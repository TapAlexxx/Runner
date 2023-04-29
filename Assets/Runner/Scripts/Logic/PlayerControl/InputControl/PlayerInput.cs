using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.InputControl
{

    public class PlayerInput : MonoBehaviour
    {
        private const int Tap = 0;

        [SerializeField] private float _delayBetweenTap;
        
        private bool _active;

        public event Action Double;
        public event Action Single;

        private void Start() => 
            Activate();

        private void Update()
        {
            if (!_active)
                return;

            if (IsTap()) 
                StartCoroutine(WaitForSecondTap());
        }

        private void Activate() =>
            _active = true;

        private void Disable() => 
            _active = false;

        private IEnumerator WaitForSecondTap()
        {
            yield return null;
            Single?.Invoke();
            float firstTapTime = Time.time;

            yield return new WaitUntil(IsTap);
            
            if (IsTapEnoughFast(firstTapTime))
                Double?.Invoke();
        }

        private bool IsTapEnoughFast(float firstTapTime) => 
            Time.time - firstTapTime < _delayBetweenTap;

        private bool IsTap() =>
            Input.GetMouseButtonDown(Tap);
    }

}