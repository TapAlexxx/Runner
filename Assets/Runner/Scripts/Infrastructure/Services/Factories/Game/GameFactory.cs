using Scripts.Infrastructure.Services.InstantiatorService;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.Infrastructure.Services.Window;
using Scripts.Logic;
using Scripts.Logic.CameraControl;
using Scripts.Logic.PlayerControl.InputControl;
using Scripts.Logic.PlayerControl.MovementControl;
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
        public CameraStateChanger CameraStateChanger { get; private set; }


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

            player.GetComponent<PlayerMover>().Initialize(staticData);
            player.GetComponent<PlayerJumper>().Initialize(staticData);
            player.GetComponent<JumpInput>().Initialize(staticData);
            
            Player = player;
        }

        public CameraStateChanger CreateCamera()
        {
            GameObject cineMachineCamera = _instantiator.InstantiateFromPath("CameraResources/Camera_container");
            CameraStateChanger = cineMachineCamera.GetComponent<CameraStateChanger>();
            return CameraStateChanger;
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