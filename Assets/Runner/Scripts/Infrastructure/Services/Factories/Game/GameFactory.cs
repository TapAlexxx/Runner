using Scripts.Infrastructure.Services.InstantiatorService;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.Infrastructure.Services.Window;
using Scripts.Logic;
using Scripts.StaticData.Player;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInstantiator _instantiator;
        private readonly IWindowService _windowService;
        
        public GameObject Player { get; private set; }
        public GameObject Hud { get; private set; }


        public GameFactory(IStaticDataService staticDataService, IInstantiator instantiator,
            IWindowService windowService)
        {
            _windowService = windowService;
            _instantiator = instantiator;
            _staticDataService = staticDataService;
        }

        public void CreatePlayer()
        {
            PlayerStaticData staticData = _staticDataService.GetPlayerStaticData();
            var player = _instantiator.InstantiatePrefab(staticData.Prefab, null);

            Player = player;
        }

        public GameObject CreateHud()
        {
            Hud = _instantiator.InstantiateFromPath("Hud/Hud");
            return Hud;
        }

        public void Clear()
        {
            Player = null;
            Hud = null;
        }
    }
}