using System.Collections;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class JumpController : MonoBehaviour
    {
        [SerializeField] private JumpInput jumpInput;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float jumpTime = 0.5f;
        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private float doubleJumpHeightMultiplier = 2f;

        private float defaultY;

        private void Start()
        {
            defaultY = transform.position.y;
            jumpInput.OnSingleJump += OnSingleJump;
            jumpInput.OnDoubleJump += OnDoubleJump;
        }

        private void OnDestroy()
        {
            jumpInput.OnSingleJump -= OnSingleJump;
            jumpInput.OnDoubleJump -= OnDoubleJump;
        }

        private void OnSingleJump()
        {
            Debug.Log("singlec");
            StartCoroutine(Jump(jumpHeight));
        }

        private void OnDoubleJump()
        {
            Debug.Log("double");
            StartCoroutine(Jump(jumpHeight * doubleJumpHeightMultiplier));
        }

        private IEnumerator Jump(float targetHeight)
        {
            float elapsedTime = 0;
            while (elapsedTime <= jumpTime)
            {
                transform.position = new Vector3(transform.position.x, defaultY + jumpCurve.Evaluate(elapsedTime / jumpTime) * targetHeight, transform.position.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

}