using CarTonki.Scripts.Logic.CameraControl;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Factories.UI;
using Scripts.Infrastructure.Services.SceneLoader;
using Scripts.Logic.CameraControl;
using Scripts.Logic.PlayerControl.SpawnControl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Infrastructure.Services.StateMachine.States
{
    public class LoadLevelState : PayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(IGameFactory gameFactory, GameStateMachine gameStateMachine,
            ISceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _gameStateMachine = gameStateMachine;
        }

        public override void Enter(string sceneName)
        {
            Debug.Log("LoadLevelState");
            _sceneLoader.Load(sceneName, OnLevelLoaded);
        }

        private void InitializeGameWorld()
        {
            _gameFactory.Clear();
            _uiFactory.CreateUiRoot();

            InitPlayer();
            InitCamera();
            _gameFactory.CreateHud();
        }

        private void InitCamera()
        {
            CameraStateChanger cameraStateChanger = _gameFactory.CreateCamera();
            cameraStateChanger.Initialize();
            cameraStateChanger.SwitchTo(CameraViewState.Default, _gameFactory.Player.transform);
        }

        private void InitPlayer()
        {
            PlayerSpawnPoint spawnPoint = Object.FindObjectOfType<PlayerSpawnPoint>();
            _gameFactory.CreatePlayer(spawnPoint.transform);
        }

        private void OnLevelLoaded()
        {
            InitializeGameWorld();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        public override void Exit()
        {
        }
    }

}