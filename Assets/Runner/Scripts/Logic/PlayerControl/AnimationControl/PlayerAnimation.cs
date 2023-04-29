using System;
using System.Collections;
using Scripts.Logic.PlayerControl.MovementControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.AnimationControl
{

    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private JumpInput jumpInput;

        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
            jumpInput = GetComponentInChildren<JumpInput>();
        }

        private void Start()
        {
            jumpInput.OnSingleJump += Jump;
        }

        private void OnDestroy()
        {
            jumpInput.OnSingleJump -= Jump;
        }

        private void Jump()
        {
            StartCoroutine(AnimateJump());
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