using System;
using Scripts.StaticData.Player;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.InputControl
{

    public class JumpInput : MonoBehaviour
    {
        private float _lastJumpButtonTime;
        private float _doubleTapTime;

        public event Action OnSingleJump;
        public event Action OnDoubleJump;


        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - _lastJumpButtonTime < _doubleTapTime)
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

        public void Initialize(PlayerStaticData staticData)
        {
            _doubleTapTime = staticData.DoubleTapTime;
        }
    }

}