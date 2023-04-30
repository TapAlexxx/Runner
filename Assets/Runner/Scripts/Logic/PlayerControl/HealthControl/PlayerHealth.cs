using System;
using Scripts.Logic.PlayerControl.BlockControl;
using Scripts.StaticData.Player;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.HealthControl
{

    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private DamageBlockObserver damageBlockObserver;
        
        private int _startHealth;
        private int _currentHealth;

        public event Action Dead;
        public event Action Revived;

        private void OnValidate()
        {
            damageBlockObserver = GetComponentInChildren<DamageBlockObserver>();
        }

        public void Initialize(PlayerStaticData staticData)
        {
            _startHealth = staticData.StartHealth;
            ResetHealth();
        }

        private void Start()
        {
            damageBlockObserver.HitDamageBlock += TakeDamage;
        }

        private void OnDestroy()
        {
            damageBlockObserver.HitDamageBlock -= TakeDamage;
        }

        private void Heal()
        {
            _currentHealth++;
        }

        private void ResetHealth()
        {
            _currentHealth = _startHealth;
            Revived?.Invoke();
        }

        private void TakeDamage()
        {
            _currentHealth--;
            ValidateHealth();
        }

        private void ValidateHealth()
        {
            if (_currentHealth <= 0)
            {
                Dead?.Invoke();
            }
        }
    }
}