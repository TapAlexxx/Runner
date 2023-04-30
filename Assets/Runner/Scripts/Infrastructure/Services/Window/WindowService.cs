using System;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Factories.UI;
using Scripts.Infrastructure.Services.StateMachine;
using Scripts.Logic.Hud;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Infrastructure.Services.Window
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;
        private IGameFactory _gameFactory;
        private GameStateMachine _gameStateMachine;
        
        private GameObject _lastOpened;


        public void Open(WindowTypeId windowTypeId)
        {
            _lastOpened = _uiFactory.CrateWindow(windowTypeId);

            InitWindow(windowTypeId);
        }

        private void InitWindow(WindowTypeId windowTypeId)
        {
            _lastOpened.GetComponentInChildren<LoadNewLevelButton>().Initialize(_gameStateMachine);
            switch (windowTypeId)
            {
                case WindowTypeId.Finish:
                    break;
                case WindowTypeId.Lose:
                    _lastOpened.GetComponentInChildren<ReviveButton>().Initialize(_gameFactory.Player);
                    break;
            }
        }

        public void TryCloseLastOpened()
        {
            if(_lastOpened == null)
                return;
            Object.Destroy(_lastOpened);
        }

        public void Initialize(IUIFactory uiFactory, IGameFactory gameFactory, GameStateMachine gameStateMachine)
        {
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }
    }
}