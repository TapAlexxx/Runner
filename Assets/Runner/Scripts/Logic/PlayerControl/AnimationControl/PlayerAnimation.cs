using Scripts.Logic.PlayerControl.MovementControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.AnimationControl
{

    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private PlayerMover playerMover;

        private readonly int _movement = Animator.StringToHash("Movement");
        private readonly int _jump = Animator.StringToHash("Jump");

        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
            groundChecker = GetComponentInChildren<GroundChecker>();
            playerMover = GetComponentInChildren<PlayerMover>();
        }

        private void Update()
        {
            animator.SetFloat(_movement, playerMover.NormalizedSpeed);
            animator.SetBool(_jump, !groundChecker.Grounded);
        }
    }

}