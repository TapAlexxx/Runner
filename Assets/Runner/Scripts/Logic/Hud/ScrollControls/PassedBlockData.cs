using Scripts.Logic.LevelGeneration.Blocks;
using UnityEngine;

namespace Scripts.Logic.Hud.ScrollControls
{

    [CreateAssetMenu(menuName = "StaticData/PassedBlockData", fileName = "PassedBlockData", order = 0)]
    public class PassedBlockData : ScriptableObject
    {
        public string Name;
        public DamageBlockType DamageBlockType;
        public Sprite Icon;
    }

}