using System;
using Scripts.Infrastructure.Services.Factories.UI;
using Scripts.Infrastructure.Services.InstantiatorService;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.StaticData.Player;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInstantiator _instantiator;

        
        public GameObject Player { get; private set; }
        

        public GameFactory(IStaticDataService staticDataService, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _staticDataService = staticDataService;
        }

        public void CreatePlayer()
        {
            PlayerStaticData staticData = _staticDataService.GetPlayerStaticData();
            var player = _instantiator.InstantiatePrefab(staticData.Prefab, null);
            Player = player;
        }

        public void Clear()
        {
            Player = null;
        }
    }
}