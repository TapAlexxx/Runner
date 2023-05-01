using Scripts.Logic.PlayerControl.BlockControl;
using Scripts.Logic.PlayerControl.HealthControl;
using Scripts.Logic.PlayerControl.MovementControl;
using UnityEngine;

namespace Scripts.Logic.Boosters
{

    public class BoosterApplier : MonoBehaviour
    {
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private DamageBlockObserver damageBlockObserver;
        [SerializeField] private BoosterCollector boosterCollector;

        private void OnValidate()
        {
            playerMover = GetComponentInChildren<PlayerMover>();
            playerHealth = GetComponentInChildren<PlayerHealth>();
            damageBlockObserver = GetComponentInChildren<DamageBlockObserver>();
            boosterCollector = GetComponentInChildren<BoosterCollector>();
        }

        private void Start()
        {
            boosterCollector.SpeedCollected += BoostSpeed;
            boosterCollector.ShieldCollected += ActivateShield;
            boosterCollector.HealCollected += Heal;
        }

        private void OnDestroy()
        {
            boosterCollector.SpeedCollected -= BoostSpeed;
            boosterCollector.ShieldCollected -= ActivateShield;
            boosterCollector.HealCollected -= Heal;
        }

        private void BoostSpeed(int boostValue)
        {
            playerMover.BoostSpeed(boostValue);
        }

        private void ActivateShield(int boostValue)
        {
            damageBlockObserver.ActivateShield(boostValue);
        }

        private void Heal(int boostValue)
        {
            playerHealth.Heal(boostValue);
        }
    }

}