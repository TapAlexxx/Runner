using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class PlayerMover : MonoBehaviour
    {
        private const float DefaultY = 0.5f;
        
        [SerializeField] private float speed;
        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private float jumpTime = 0.6f;
        [SerializeField] private float jumpHeight = 3f;


        private void Update()
        {
            float axis = Input.GetAxis("Vertical");

            transform.position += Vector3.forward * (axis * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Jump(jumpHeight));
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                StartCoroutine(Jump(jumpHeight * 2));
            }
        }

        private IEnumerator Jump(float height)
        {
            float elapsedTime = 0;
            while (elapsedTime <= jumpTime)
            {
                transform.DOMoveY(DefaultY + jumpCurve.Evaluate(elapsedTime / jumpTime) * height, Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}