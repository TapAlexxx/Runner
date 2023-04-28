using Scripts.Infrastructure.Services.Factories.Game;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StateMachine.States
{
    public class LoadLevelState : PayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState() { }

        public LoadLevelState(IGameFactory gameFactory, GameStateMachine gameStateMachine)
        {
            _gameFactory = gameFactory;
            _gameStateMachine = gameStateMachine;
        }

        public override void Enter(string sceneName)
        {
            Debug.Log(sceneName);
            _gameFactory.CreatePlayer();
        }

        public override void Exit()
        {
        }
    }

    public class GameLoopState : State
    {
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
    }
}