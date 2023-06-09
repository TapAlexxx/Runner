﻿using System;
using Scripts.Logic.LevelGeneration.Blocks;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.BlockControl
{

    public class DamageBlockObserver : MonoBehaviour
    {
        private DamageBlock _lastDamageBlock;
        private int _shieldCount;

        public int ShieldCount => _shieldCount;
        
        public event Action<DamageBlock> PassedBlock;
        public event Action HitDamageBlock;
        public event Action ShieldCountChanged;

        private void Start()
        {
            _shieldCount = 0;
            ShieldCountChanged?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DamageBlock damageBlock))
            {
                _lastDamageBlock = damageBlock;
                if (_shieldCount > 0)
                {
                    _shieldCount--;
                    ShieldCountChanged?.Invoke();
                }
                else
                {
                    HitDamageBlock?.Invoke();
                }
            }

            if (other.TryGetComponent(out DamageBlockExit damageBlockExit))
            {
                if (!damageBlockExit.AccordingTo(_lastDamageBlock))
                {
                    PassedBlock?.Invoke(damageBlockExit.DamageBlock);
                }
            }
        }

        public void ActivateShield(int boostValue)
        {
            _shieldCount += boostValue;
            ShieldCountChanged?.Invoke();
        }
    }

}