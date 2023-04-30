using System;
using Scripts.Logic.LevelGeneration.Blocks;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.BlockControl
{

    public class DamageBlockObserver : MonoBehaviour
    {
        private DamageBlock _lastDamageBlock;

        public event Action<DamageBlock> PassedBlock;
        public event Action HitDamageBlock;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DamageBlock damageBlock))
            {
                _lastDamageBlock = damageBlock;
                HitDamageBlock?.Invoke();
            }

            if (other.TryGetComponent(out DamageBlockExit damageBlockExit))
            {
                if (!damageBlockExit.AccordingTo(_lastDamageBlock))
                {
                    PassedBlock?.Invoke(damageBlockExit.DamageBlock);
                }
            }
        }
    }

}