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
        [SerializeField] private PlayerMover playerMover;
        
        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
            groundChecker = GetComponentInChildren<GroundChecker>();
            playerMover = GetComponentInChildren<PlayerMover>();
        }
        
        private void Update()
        {
            animator.SetFloat("Movement", playerMover.NormalizedSpeed);
            animator.SetBool("Jump", !groundChecker.Grounded);
        }
    }

}