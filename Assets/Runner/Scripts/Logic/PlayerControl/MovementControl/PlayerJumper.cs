using System.Collections;
using DG.Tweening;
using Scripts.Logic.PlayerControl.InputControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class PlayerJumper : MonoBehaviour
    {
        private const float DefaultY = 0.5f;
        
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private JumpInput playerInput;
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private float defaultJumpDuration = 0.3f;
        [SerializeField] private float doubleJumpDuration = 0.3f;
        
        private float _targetHeight;
        private bool _jumped;
        private bool _canJump;
        private float _elapsedTime;
        private float _delay;

        private void OnValidate()
        {
            playerInput = GetComponentInChildren<JumpInput>();
            groundChecker = GetComponentInChildren<GroundChecker>();
        }

        private void Start()
        {
            _canJump = true;
            playerInput.OnSingleJump += DefaultJump;
            playerInput.OnDoubleJump += DoubleJump;
        }

        private void OnDestroy()
        {
            playerInput.OnSingleJump -= DefaultJump;
            playerInput.OnDoubleJump -= DoubleJump;
        }

        private void DefaultJump()
        {
            if(!groundChecker.Grounded || !_canJump)
                return;
            _jumped = true;
            _targetHeight = jumpHeight;

            Jump(_targetHeight, defaultJumpDuration);
            _delay = defaultJumpDuration + defaultJumpDuration * 1.5f;
            
            StartCoroutine(DelayNextJump());
        }

        private void DoubleJump()
        {
            if(!_jumped)
                return;
            _jumped = false;
            _targetHeight = jumpHeight * 2;
            
            Jump(_targetHeight, doubleJumpDuration);
            _delay = doubleJumpDuration + doubleJumpDuration * 1.5f;
        }

        private void Jump(float targetHeight, float jumpDuration)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveY(DefaultY + targetHeight, jumpDuration).SetEase(Ease.OutCirc));
            sequence.Append(transform.DOMoveY(DefaultY, jumpDuration * 1.5f).SetEase(Ease.InCubic));
        }

        private IEnumerator DelayNextJump()
        {
            _canJump = false;
            _elapsedTime = 0;
            while (_elapsedTime <= _delay)
            {
                _elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            _canJump = true;
        }
    }
}