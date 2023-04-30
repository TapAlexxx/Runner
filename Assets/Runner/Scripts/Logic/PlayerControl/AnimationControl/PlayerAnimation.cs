using Scripts.Logic.PlayerControl.HealthControl;
using Scripts.Logic.PlayerControl.MovementControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.AnimationControl
{

    public class PlayerAnimation : MonoBehaviour
    {
        private const string Dead = "Dead";
        
        [SerializeField] private Animator animator;
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private PlayerHealth playerHealth;

        private readonly int _dead = Animator.StringToHash("Dead");
        private readonly int _movement = Animator.StringToHash("Movement");
        private readonly int _jump = Animator.StringToHash("Jump");

        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
            groundChecker = GetComponentInChildren<GroundChecker>();
            playerMover = GetComponentInChildren<PlayerMover>();
            playerHealth = GetComponentInChildren<PlayerHealth>();
        }

        private void Start()
        {
            playerHealth.Dead += AnimateDeath;
            playerHealth.Revived += Revive;
        }

        private void OnDestroy()
        {
            playerHealth.Dead -= AnimateDeath;
            playerHealth.Revived -= Revive;
        }

        private void Update()
        {
            animator.SetFloat(_movement, playerMover.NormalizedSpeed);
            animator.SetBool(_jump, !groundChecker.Grounded);
        }

        private void Revive()
        {
            animator.SetBool(_dead, false);
        }

        private void AnimateDeath()
        {
            animator.SetBool(_dead, true);
            animator.Play(Dead);
        }
    }

}