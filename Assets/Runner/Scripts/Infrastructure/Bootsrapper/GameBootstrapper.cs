﻿using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Factories.UI;
using Scripts.Infrastructure.Services.InstantiatorService;
using Scripts.Infrastructure.Services.SceneLoader;
using Scripts.Infrastructure.Services.StateMachine;
using Scripts.Infrastructure.Services.StateMachine.States;
using Scripts.Infrastructure.Services.StaticData;
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

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Initialize()
        {
            _instantiator = SetupInstantiator();
            _sceneLoader = SetupSceneLoader();
            _staticDataService = SetupStaticDataService();
            _gameFactory = SetupGameFactory();
            _uiFactory = SetupUiFactory();
            
            _gameStateMachine = SetupGameStateMachine();
            BindStates();
            
            _gameStateMachine.Enter<LoadLevelState, string>("SampleScene");
        }

        private StaticDataService SetupStaticDataService()
        {
            var staticDataService = new StaticDataService();
            staticDataService.Load();
            return staticDataService;
        }

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

        private IUIFactory SetupUiFactory() => 
            new UIFactory(_staticDataService, _instantiator);

        private IGameFactory SetupGameFactory() => 
            new GameFactory(_staticDataService, _instantiator);
    }
}