using UnityEngine;

namespace Scripts.Infrastructure.Services.StateMachine.States
{
    public class LoadLevelState : PayloadedState<string>
    {
        public override void Enter(string sceneName)
        {
            Debug.Log(sceneName);
        }

        public override void Exit()
        {
        }
    }
}