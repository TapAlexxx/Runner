using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Extensions;
using Scripts.Logic.Boosters;
using Scripts.StaticData.Level;
using UnityEngine;
using Random = UnityEngine.Random;

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

        [SerializeField] private Booster speedBooster;
        [SerializeField] private Booster shieldBooster;
        [SerializeField] private Booster healBooster;

        
        private List<GameObject> _defaultPool;
        private List<GameObject> _leftTurnPool;
        private List<GameObject> _rightTurnPool;
        private List<GameObject> _damagePool;
        private List<GameObject> _finishPool;
        private List<GameObject> _speedBoosterPool;
        private List<GameObject> _shieldBoosterPool;
        private List<GameObject> _healBoosterPool;

        public void Initialize()
        {
            _defaultPool = new List<GameObject>();
            _leftTurnPool = new List<GameObject>();
            _rightTurnPool = new List<GameObject>();
            _damagePool = new List<GameObject>();
            _finishPool = new List<GameObject>();
            
            _speedBoosterPool = new List<GameObject>();
            _shieldBoosterPool = new List<GameObject>();
            _healBoosterPool = new List<GameObject>();
            
            FillPool(_defaultPool, defaultBlocks, defaultCopyCount);
            FillPool(_leftTurnPool, leftTurnBlocks, leftTurnCopyCount);
            FillPool(_rightTurnPool, rightTurnBlocks, rightTurnCopyCount);
            FillPool(_damagePool, damageBlocks, damageCopyCount);
            FillPool(_finishPool, finishBlocks, finishCopyCount);
            
            FillPool(_speedBoosterPool, speedBooster, 10);
            FillPool(_shieldBoosterPool, shieldBooster, 10);
            FillPool(_healBoosterPool, healBooster, 10);
        }

        private void FillPool(List<GameObject> pool, Booster booster, int copyCount)
        {
            for (int i = 0; i < copyCount; i++)
            {
                GameObject block = Instantiate(booster.gameObject, transform);
                block.SetActive(false);
                pool.Add(block);
            }
        }

        public bool TryGetBooster(out Booster booster)
        {
            BoosterType boosterType = ChooseRandomBooster();
            booster = null;
            Booster targetBooster;
            switch (boosterType)
            {
                case BoosterType.Speed: 
                    targetBooster = GetSpeedBooster();
                    if (Random.Range(0, 1f) <= targetBooster.ChanceToAppear)
                        booster = targetBooster;
                    break;
                case BoosterType.Shield: 
                    targetBooster = GetShieldBooster();
                    if (Random.Range(0, 1f) <= targetBooster.ChanceToAppear)
                        booster = targetBooster;
                    break;
                case BoosterType.Heal:
                    targetBooster = GetHealBooster();
                    if (Random.Range(0, 1f) <= targetBooster.ChanceToAppear)
                        booster = targetBooster;
                    break;
                default:
                    booster = null;
                    break;
            }

            return booster != null;
        }

        private Booster GetHealBooster() =>
            _healBoosterPool.FirstOrDefault(
                x => x.activeSelf == false)?.GetComponent<Booster>();

        private Booster GetShieldBooster() =>
            _shieldBoosterPool.FirstOrDefault(
                x => x.activeSelf == false)?.GetComponent<Booster>();

        private Booster GetSpeedBooster() =>
            _speedBoosterPool.FirstOrDefault(
                x => x.activeSelf == false)?.GetComponent<Booster>();

        private static BoosterType ChooseRandomBooster()
        {
            BoosterType boosterType;
            float randomValue = Random.Range(0, 3f);

            if (randomValue < 1f)
                boosterType = BoosterType.Heal;
            else if (randomValue > 1f && randomValue < 2f)
                boosterType = BoosterType.Speed;
            else
                boosterType = BoosterType.Shield;
            return boosterType;
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