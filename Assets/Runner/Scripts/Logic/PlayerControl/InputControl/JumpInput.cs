using System;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class JumpInput : MonoBehaviour
    {
        public event Action OnSingleJump;
        public event Action OnDoubleJump;

        public float doubleTapTime = 0.2f;

        private float lastJumpButtonTime;
        public float Delay {get; private set; }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - lastJumpButtonTime < doubleTapTime)
                {
                    Delay = Time.time - lastJumpButtonTime;
                    OnDoubleJump?.Invoke();
                    lastJumpButtonTime = 0;
                    Debug.Log("double");
                }
                else
                {
                    Delay = 0;
                    OnSingleJump?.Invoke();
                    lastJumpButtonTime = Time.time;
                }
            }
        }
    }

}