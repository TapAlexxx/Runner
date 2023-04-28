using Scripts.Infrastructure.Services.Window;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StateMachine.States
{

    public class GameLoopState : State
    {
        
        public override void Enter()
        {
            Debug.Log("GameLoopState");
        }

        public override void Exit()
        {
        }
    }

}