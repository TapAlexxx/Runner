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
            _instantiator = SetupInstantiator();
            _sceneLoader = SetupSceneLoader();
            _staticDataService = SetupStaticDataService();

            _uiFactory = SetupUiFactory(_staticDataService, _instantiator);
            _windowService = SetupWindowService(_uiFactory);
            _gameFactory = SetupGameFactory(_staticDataService, _instantiator, _windowService);


            _gameStateMachine = SetupGameStateMachine();
            
            BindStates();

            LoadInitialStartScene();
        }

        private void LoadInitialStartScene()
        {
            GameConfig gameConfig = _staticDataService.GetGameConfig();
            _gameStateMachine.Enter<LoadLevelState, string>(gameConfig.StartScene);
        }

        private StaticDataService SetupStaticDataService()
        {
            var staticDataService = new StaticDataService();
            staticDataService.Load();
            return staticDataService;
        }

        private WindowService SetupWindowService(IUIFactory uiFactory) => 
            new WindowService(uiFactory);

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
            IWindowService windowService) => 
            new GameFactory(staticDataService, instantiator, windowService);
    }
}