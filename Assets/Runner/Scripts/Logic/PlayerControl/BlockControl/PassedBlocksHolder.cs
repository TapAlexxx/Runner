using System.Collections.Generic;
using Scripts.Logic.LevelGeneration.Blocks;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.BlockControl
{

    public class PassedBlocksHolder : MonoBehaviour
    {
        [SerializeField] private DamageBlockObserver damageBlockObserver;

        private Dictionary<DamageBlockType, int> _passedBlocks;

        private void OnValidate()
        {
            damageBlockObserver = GetComponentInChildren<DamageBlockObserver>();
        }

        public Dictionary<DamageBlockType, int> GetPassedBlocks() => 
            _passedBlocks;

        private void Start()
        {
            _passedBlocks = new Dictionary<DamageBlockType, int>();
            damageBlockObserver.PassedBlock += CollectBlock;
        }

        private void OnDestroy()
        {
            damageBlockObserver.PassedBlock -= CollectBlock;
        }

        private void CollectBlock(DamageBlock block)
        {
            if (_passedBlocks.ContainsKey(block.Type))
            {
                _passedBlocks[block.Type] += 1;
            }
            else
            {
                _passedBlocks.Add(block.Type, 1);
            }

            foreach (KeyValuePair<DamageBlockType,int> keyValuePair in _passedBlocks)
            {
                Debug.Log(keyValuePair.Key);
                Debug.Log(keyValuePair.Value);
            }
        }
    }
}