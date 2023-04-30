using UnityEngine;

namespace Scripts.Logic.LevelGeneration.Blocks
{

    public class DamageBlockExit : MonoBehaviour
    {
        [SerializeField] private DamageBlock damageBlock;
        
        public DamageBlock DamageBlock => damageBlock;

        public bool AccordingTo(DamageBlock lastDamageBlock) => 
            lastDamageBlock == damageBlock;
    }

}