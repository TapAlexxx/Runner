using System;
using System.Collections;
using Scripts.Logic.PlayerControl.MovementControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.AnimationControl
{

    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GroundChecker groundChecker;
        
        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
            groundChecker = GetComponentInChildren<GroundChecker>();
        }
        
        private void Update()
        {
            float axis = Input.GetAxis("Vertical");
         
            animator.SetFloat("Movement", axis);
            animator.SetBool("Jump", !groundChecker.Grounded);
        }
    }

}