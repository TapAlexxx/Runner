using System;
using Scripts.Logic.PlayerControl.HealthControl;
using Scripts.Logic.PlayerControl.MovementControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.StateControl
{

    public class PlayerStateControl : MonoBehaviour
    {
        [SerializeField] private PlayerJumper playerJumper;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private PlayerHealth playerHealth;
        
        private void OnValidate()
        {
            playerJumper = GetComponentInChildren<PlayerJumper>();
            playerMover = GetComponentInChildren<PlayerMover>();
            playerHealth = GetComponentInChildren<PlayerHealth>();
        }

        public void EnterRunState()
        {
            playerJumper.Activate();
            playerMover.Activate(); 
        }

        private void Start()
        {
            playerHealth.Dead += EnterDeadState;
            playerHealth.Revived += EnterRevivedState;
        }

        private void OnDestroy()
        {
            playerHealth.Dead -= EnterDeadState;
            playerHealth.Revived -= EnterRevivedState;
        }

        private void EnterRevivedState()
        {
            playerJumper.Activate();
            playerMover.Activate();   
        }

        private void EnterDeadState()
        {
            playerJumper.Disable();
            playerMover.Disable();
        }
    }
}