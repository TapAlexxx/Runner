using UnityEngine;

namespace Scripts.Logic.LevelGeneration.Blocks
{

    public class DamageBlockExit : MonoBehaviour
    {
        [SerializeField] private DamageBlock damageBlock;
        
        public DamageBlock DamageBlock => damageBlock;

        private void OnValidate()
        {
            damageBlock = GetComponentInChildren<DamageBlock>();
        }
        
        public bool AccordingTo(DamageBlock lastDamageBlock) => 
            lastDamageBlock == damageBlock;
    }

}