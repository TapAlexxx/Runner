using System;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.InputControl
{

    public class JumpInput : MonoBehaviour
    {
        private float _lastJumpButtonTime;

        public float doubleTapTime = 0.2f;

        public event Action OnSingleJump;
        public event Action OnDoubleJump;


        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - _lastJumpButtonTime < doubleTapTime)
                {
                    OnDoubleJump?.Invoke();
                    _lastJumpButtonTime = 0;
                }
                else
                {
                    OnSingleJump?.Invoke();
                    _lastJumpButtonTime = Time.time;
                }
            }
        }
    }

}