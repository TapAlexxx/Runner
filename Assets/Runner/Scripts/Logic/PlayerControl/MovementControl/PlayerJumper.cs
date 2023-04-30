using System.Collections;
using DG.Tweening;
using Scripts.Logic.PlayerControl.InputControl;
using Scripts.StaticData.Player;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class PlayerJumper : MonoBehaviour
    {
        private const float DefaultY = 0.5f;

        [SerializeField] private JumpInput playerInput;
        [SerializeField] private GroundChecker groundChecker;
        
        private float _jumpHeight;
        private float _defaultJumpDuration;
        private float _doubleJumpDuration;
        private float _targetHeight;
        private float _elapsedTime;
        private float _delay;
        private bool _jumped;
        private bool _canJump;

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
            _targetHeight = _jumpHeight;

            Jump(_targetHeight, _defaultJumpDuration);
            _delay = _defaultJumpDuration + _defaultJumpDuration * 1.5f;
            
            StartCoroutine(DelayNextJump());
        }

        private void DoubleJump()
        {
            if(!_jumped)
                return;
            _jumped = false;
            _targetHeight = _jumpHeight * 2;
            
            Jump(_targetHeight, _doubleJumpDuration);
            _delay = _doubleJumpDuration + _doubleJumpDuration * 1.5f;
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

        public void Initialize(PlayerStaticData staticData)
        {
            _jumpHeight = staticData.JumpHeight;
            _defaultJumpDuration = staticData.DefaultJumpDuration;
            _doubleJumpDuration = staticData.DoubleJumpDuration;
        }
    }
}