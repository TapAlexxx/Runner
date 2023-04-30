using System.Collections.Generic;
using System.Linq;
using Scripts.Extensions;
using UnityEngine;

namespace Scripts.Logic.LevelGeneration.Blocks
{

    public class BlockPool : MonoBehaviour
    {
        [SerializeField] private List<GameObject> defaultBlocks;
        [SerializeField] private int defaultCopyCount = 5;
        [SerializeField] private List<GameObject> leftTurnBlocks;
        [SerializeField] private int leftTurnCopyCount = 5;
        [SerializeField] private List<GameObject> rightTurnBlocks;
        [SerializeField] private int rightTurnCopyCount = 5;
        [SerializeField] private List<GameObject> damageBlocks;
        [SerializeField] private int damageCopyCount = 5;
        [SerializeField] private List<GameObject> finishBlocks;
        [SerializeField] private int finishCopyCount = 5;

        private List<GameObject> _defaultPool;
        private List<GameObject> _leftTurnPool;
        private List<GameObject> _rightTurnPool;
        private List<GameObject> _damagePool;
        private List<GameObject> _finishPool;

        public void Initialize()
        {
            _defaultPool = new List<GameObject>();
            _leftTurnPool = new List<GameObject>();
            _rightTurnPool = new List<GameObject>();
            _damagePool = new List<GameObject>();
            _finishPool = new List<GameObject>();
            
            FillPool(_defaultPool, defaultBlocks, defaultCopyCount);
            FillPool(_leftTurnPool, leftTurnBlocks, leftTurnCopyCount);
            FillPool(_rightTurnPool, rightTurnBlocks, rightTurnCopyCount);
            FillPool(_damagePool, damageBlocks, damageCopyCount);
            FillPool(_finishPool, finishBlocks, finishCopyCount);
        }

        public bool TryGetDefault(out GameObject block)
        {
            _defaultPool.Shuffle();
            GetBlockFrom(_defaultPool, out block);
            return block != null;
        }

        public bool TryGetLeftTurn(out GameObject block)
        {
            _leftTurnPool.Shuffle();
            GetBlockFrom(_leftTurnPool, out block);
            return block != null;
        }

        public bool TryGetRightTurn(out GameObject block)
        {
            _rightTurnPool.Shuffle();
            GetBlockFrom(_rightTurnPool, out block);
            return block != null;
        }

        public bool TryGetDamage(out GameObject block)
        {
            _damagePool.Shuffle();
            GetBlockFrom(_damagePool, out block);
            return block != null;
        }

        public bool TryGetFinish(out GameObject finish)
        {
            _finishPool.Shuffle();
            GetBlockFrom(_finishPool, out finish);
            return finish != null;
        }

        public void Reset()
        {
            foreach (GameObject block in _defaultPool) 
                block.SetActive(false);
            foreach (GameObject block in _leftTurnPool) 
                block.SetActive(false);
            foreach (GameObject block in _rightTurnPool) 
                block.SetActive(false);
            foreach (GameObject block in _damagePool) 
                block.SetActive(false);
        }

        private void FillPool(List<GameObject> pool, List<GameObject> blocks, int copyCount)
        {
            foreach (GameObject defaultBlock in blocks)
            {
                for (int i = 0; i < copyCount; i++)
                {
                    GameObject block = Instantiate(defaultBlock,transform);
                    block.SetActive(false);
                    pool.Add(block);
                }
            }
        }

        private void GetBlockFrom(List<GameObject> pool, out GameObject block) => 
            block = pool.FirstOrDefault(x => x.activeSelf == false);
    }

}