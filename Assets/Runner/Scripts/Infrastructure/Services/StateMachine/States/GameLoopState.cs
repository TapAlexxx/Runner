using Scripts.Infrastructure.Services.Curtain;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StateMachine.States
{

    public class GameLoopState : State
    {
        private ILoadingCurtain _loadingCurtain;

        public GameLoopState(ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
        }
        
        public override void Enter()
        {
            Debug.Log("GameLoopState");
            _loadingCurtain.Hide();
        }

        public override void Exit()
        {
        }
    }

}