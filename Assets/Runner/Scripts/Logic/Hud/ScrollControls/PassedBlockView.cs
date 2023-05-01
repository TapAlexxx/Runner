using System;
using Scripts.Infrastructure.Services.InstantiatorService;
using Scripts.Logic.LevelGeneration.Blocks;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Logic.Hud.ScrollControls
{

    public class PassedBlockView : MonoBehaviour
    {
        [SerializeField] private Text blockName;
        [SerializeField] private Text countText;
        [SerializeField] private Image icon;
        
        public void RefreshView(DamageBlockType passedBlockType, int count, PassedBlockData data)
        {

            blockName.text = passedBlockType.ToString();
            countText.text = count.ToString();
            icon.sprite = data.Icon;
        }
    }

}