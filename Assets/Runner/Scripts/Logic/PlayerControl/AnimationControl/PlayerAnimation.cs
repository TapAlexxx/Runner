using System.Collections;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.AnimationControl
{

    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
        }
        
        private void Update()
        {
            float axis = Input.GetAxis("Vertical");
         
            animator.SetFloat("Movement", axis);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.V)) 
                StartCoroutine(AnimateJump());
        }
        
        private IEnumerator AnimateJump()
        {
            animator.SetBool("Jump", true);
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("Jump", false);
        }
    }

}