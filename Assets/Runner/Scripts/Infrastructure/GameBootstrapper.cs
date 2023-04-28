using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Factories.UI;
using Scripts.Infrastructure.Services.StateMachine;
using Scripts.Infrastructure.Services.StateMachine.States;
using Scripts.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IStaticDataService _staticDataService;
        private IGameFactory _gameFactory;
        private IInstantiator _instantiator;
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Initialize()
        {
            _instantiator = gameObject.AddComponent<Instantiator>();
            InitializeStaticDataService();
            InitializeGameFactory();
            InitializeUiFactory();

            InitializeGameStateMachine();
            AddStates();
            

            _gameStateMachine.Enter<LoadLevelState, string>("Test Scene Name");
        }

        private void AddStates()
        {
            _gameStateMachine.AddState(new LoadLevelState(_gameFactory, _gameStateMachine));
        }

        private void InitializeGameStateMachine()
        {
            _gameStateMachine = new GameStateMachine();
            _gameStateMachine.Initialize();
        }

        private void InitializeUiFactory()
        {
            IUIFactory uiFactory = new UIFactory(_staticDataService, _instantiator);
            uiFactory.CreateUiRoot();
        }

        private void InitializeGameFactory()
        {
            _gameFactory = new GameFactory();
            _gameFactory.Clear();
        }

        private void InitializeStaticDataService()
        {
            _staticDataService = new StaticDataService();
            _staticDataService.Load();
        }
    }
}