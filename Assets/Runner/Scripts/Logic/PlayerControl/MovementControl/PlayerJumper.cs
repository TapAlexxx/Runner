using System.Collections;
using DG.Tweening;
using Scripts.Logic.PlayerControl.InputControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class PlayerJumper : MonoBehaviour
    {
        private const float DefaultY = 0.5f;
        
        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private float jumpTime = 0.6f;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private JumpInput playerInput;
        [SerializeField] private GroundChecker groundChecker;
        
        private float _targetHeight;

        private void OnValidate()
        {
            playerInput = GetComponentInChildren<JumpInput>();
            groundChecker = GetComponentInChildren<GroundChecker>();
        }

        private void Start()
        {
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
            if(!groundChecker.Grounded)
                return;
            
            _targetHeight = jumpHeight;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveY(DefaultY + _targetHeight, 0.3f).SetEase(Ease.OutQuad));
            sequence.Append(transform.DOMoveY(DefaultY, 0.3f).SetEase(Ease.InQuint));
            //StartCoroutine(Jump());
        }

        private void DoubleJump()
        {
            _targetHeight = jumpHeight * 2;
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveY(DefaultY + _targetHeight, 0.3f - playerInput.Delay).SetEase(Ease.InOutQuad));
            sequence.Append(transform.DOMoveY(DefaultY, 0.3f).SetEase(Ease.InOutQuad));
        }

        /*private IEnumerator Jump()
        {
            float elapsedTime = 0;
            while (elapsedTime <= jumpTime)
            {
                transform.DOMoveY(DefaultY + jumpCurve.Evaluate(elapsedTime / jumpTime) * _targetHeight, Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }*/
    }

}