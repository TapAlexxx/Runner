using Scripts.Infrastructure.Services.InstantiatorService;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.Infrastructure.Services.Window;
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

        public void CreatePlayer(Transform spawnPointTransform)
        {
            PlayerStaticData staticData = _staticDataService.GetPlayerStaticData();
            GameObject player = _instantiator.InstantiatePrefab(staticData.Prefab, null);

            player.transform.position = spawnPointTransform.position;
            player.transform.rotation = spawnPointTransform.rotation;
            
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