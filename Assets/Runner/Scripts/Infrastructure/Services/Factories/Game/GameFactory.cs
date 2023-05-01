using System;
using Scripts.Infrastructure.Services.InstantiatorService;
using Scripts.Infrastructure.Services.StateMachine;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.Infrastructure.Services.Window;
using Scripts.Logic.CameraControl;
using Scripts.Logic.Hud;
using Scripts.Logic.LevelGeneration;
using Scripts.Logic.PlayerControl;
using Scripts.Logic.PlayerControl.HealthControl;
using Scripts.Logic.PlayerControl.InputControl;
using Scripts.Logic.PlayerControl.MovementControl;
using Scripts.StaticData.Level;
using Scripts.StaticData.Player;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInstantiator _instantiator;
        private GameStateMachine _gameStateMachine;
        private IWindowService _windowService;

        public GameObject Player { get; private set; }
        public GameObject Hud { get; private set; }
        public CameraStateChanger CameraStateChanger { get; private set; }
        public LevelGenerator LevelGenerator { get; private set; }


        public GameFactory(IStaticDataService staticDataService, IInstantiator instantiator,
            GameStateMachine gameStateMachine, IWindowService windowService)
        {
            _windowService = windowService;
            _gameStateMachine = gameStateMachine;
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
            player.GetComponent<PlayerHealth>().Initialize(staticData);
            player.GetComponent<WinLoseControl>().Initialize(_windowService);
            
            Player = player;
        }

        public CameraStateChanger CreateCamera()
        {
            GameObject cineMachineCamera = _instantiator.InstantiateFromPath("CameraResources/Camera_container");
            CameraStateChanger = cineMachineCamera.GetComponent<CameraStateChanger>();
            return CameraStateChanger;
        }

        public LevelGenerator CreateLevelGenerator()
        {
            GameObject levelGenerator = _instantiator.InstantiateFromPath("LevelProps/LevelGenerator");
            LevelGenerator = levelGenerator.GetComponentInChildren<LevelGenerator>();
            if (Player == null)
                throw new NullReferenceException("create player first");
            LevelStaticData data = _staticDataService.GetLevelStaticData();
            LevelGenerator.Initialize(data);
            return LevelGenerator;
        }

        public GameObject CreateHud()
        {
            Hud = _instantiator.InstantiateFromPath("Hud/Hud");
            if (Player == null)
                throw new NullReferenceException("create player first");
            
            Hud.GetComponentInChildren<TapToPlayButton>().Initialize(Player);
            Hud.GetComponentInChildren<PlayerHealthView>().Initialize(Player);
            Hud.GetComponentInChildren<PlayerShieldView>().Initialize(Player);
            return Hud;
        }

        public void Clear()
        {
            Player = null;
            Hud = null;
            CameraStateChanger = null;
            LevelGenerator = null;
        }
    }
}