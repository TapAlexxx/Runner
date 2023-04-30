using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Window;
using Scripts.Logic.PlayerControl.StateControl;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StateMachine.States
{

    public class GameLoopState : State
    {
        private readonly IGameFactory _gameFactory;

        public GameLoopState(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        public override void Enter()
        {
            Debug.Log("GameLoopState");

            _gameFactory.Player.GetComponent<PlayerStateControl>().EnterRunState();
        }

        public override void Exit()
        {
        }
    }

}