using DG.Tweening;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Factories.UI;
using Scripts.Infrastructure.Services.InstantiatorService;
using Scripts.Infrastructure.Services.SceneLoader;
using Scripts.Infrastructure.Services.StateMachine;
using Scripts.Infrastructure.Services.StateMachine.States;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.Infrastructure.Services.Window;
using Scripts.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Infrastructure.Bootsrapper
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IStaticDataService _staticDataService;
        private IGameFactory _gameFactory;
        private IInstantiator _instantiator;
        private GameStateMachine _gameStateMachine;
        private IUIFactory _uiFactory;
        private ISceneLoader _sceneLoader;
        private IWindowService _windowService;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Initialize()
        {
            _gameStateMachine = SetupGameStateMachine();
            
            _instantiator = SetupInstantiator();
            _sceneLoader = SetupSceneLoader();
            _staticDataService = SetupStaticDataService();

            _windowService = SetupWindowService();
            _gameFactory = SetupGameFactory(_staticDataService, _instantiator, _gameStateMachine, _windowService);
            _uiFactory = SetupUiFactory(_staticDataService, _instantiator);
            _windowService.Initialize(_uiFactory, _gameFactory, _gameStateMachine);

            BindStates();

            Application.targetFrameRate = 60;
            DOTween.Init();
            
            LoadInitialStartScene();
        }

        private void LoadInitialStartScene()
        {
            GameConfig gameConfig = _staticDataService.GetGameConfig();

            string sceneName = gameConfig.CanRunCurrent
                ? SceneManager.GetActiveScene().name
                : gameConfig.StartScene;
            
            _gameStateMachine.Enter<LoadLevelState, string>(sceneName);
        }

        private StaticDataService SetupStaticDataService()
        {
            var staticDataService = new StaticDataService();
            staticDataService.Load();
            return staticDataService;
        }

        private WindowService SetupWindowService() =>
            new WindowService();

        private Instantiator SetupInstantiator() => 
            gameObject.AddComponent<Instantiator>();

        private ISceneLoader SetupSceneLoader() => 
            gameObject.AddComponent<SceneLoader>();

        private void BindStates()
        {
            _gameStateMachine.BindState(new LoadLevelState(
                _gameFactory, _gameStateMachine,
                _sceneLoader, _uiFactory));
            
            _gameStateMachine.BindState(new GameLoopState());
        }

        private GameStateMachine SetupGameStateMachine()
        {
            GameStateMachine gameStateMachine = gameObject.AddComponent<GameStateMachine>();
            gameStateMachine.Initialize();
            return gameStateMachine;
        }

        private IUIFactory SetupUiFactory(IStaticDataService staticDataService, IInstantiator instantiator) => 
            new UIFactory(staticDataService, instantiator);

        private IGameFactory SetupGameFactory(IStaticDataService staticDataService, IInstantiator instantiator,
            GameStateMachine gameStateMachine, IWindowService windowService) => 
            new GameFactory(staticDataService, instantiator, gameStateMachine, windowService);
    }
}