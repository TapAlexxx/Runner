﻿using System;
using Scripts.Logic.PlayerControl.BlockControl;
using Scripts.StaticData.Player;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.HealthControl
{

    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private DamageBlockObserver damageBlockObserver;
        
        private int _startHealth;
        public int CurrentHealth { get; private set; }
        public event Action DamageApplied;
        public event Action Dead;
        public event Action Revived;
        public event Action HealthChanged;

        private void OnValidate()
        {
            damageBlockObserver = GetComponentInChildren<DamageBlockObserver>();
        }

        public void Initialize(PlayerStaticData staticData)
        {
            _startHealth = staticData.StartHealth;
            Revive();
        }

        private void Start()
        {
            damageBlockObserver.HitDamageBlock += TakeDamage;
        }

        private void OnDestroy()
        {
            damageBlockObserver.HitDamageBlock -= TakeDamage;
        }

        public void Heal(int healCount)
        {
            if (healCount <= 0)
                throw new InvalidOperationException("heal must be grater zero");
            CurrentHealth += healCount;
            HealthChanged?.Invoke();
        }

        public void Revive()
        {
            CurrentHealth = _startHealth;
            HealthChanged?.Invoke();
            Revived?.Invoke();
        }

        private void TakeDamage()
        {
            CurrentHealth--;
            HealthChanged?.Invoke();
            DamageApplied?.Invoke();
            ValidateHealth();
        }

        private void ValidateHealth()
        {
            if (CurrentHealth <= 0)
            {
                Dead?.Invoke();
            }
        }
    }
}