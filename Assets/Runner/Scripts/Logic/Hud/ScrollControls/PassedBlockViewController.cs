using System.Collections.Generic;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.Logic.LevelGeneration.Blocks;
using Scripts.Logic.PlayerControl.BlockControl;
using UnityEngine;

namespace Scripts.Logic.Hud.ScrollControls
{

    public class PassedBlockViewController : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private PassedBlockView viewPrefab;
        
        private IStaticDataService _staticDataService;
        private IGameFactory _gameFactory;
        private Dictionary<DamageBlockType, int> _passedBlocks;

        public void Initialize(IStaticDataService staticDataService, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
            _passedBlocks = _gameFactory.Player.GetComponentInChildren<PassedBlocksHolder>().GetPassedBlocks();

            FillContent(_passedBlocks);
        }

        private void FillContent(Dictionary<DamageBlockType, int> passedBlocks)
        {
            ResetContent();
            foreach (KeyValuePair<DamageBlockType,int> keyValuePair in passedBlocks)
            {
                PassedBlockView view = Instantiate(viewPrefab.gameObject, content).GetComponent<PassedBlockView>();
                PassedBlockData data = _staticDataService.GetBlockDataFor(keyValuePair.Key);
                view.RefreshView(keyValuePair.Value, data);
            }
        }

        private void ResetContent()
        {
            foreach (Transform child in content) 
                Destroy(child.gameObject);
        }
    }

}