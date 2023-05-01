using UnityEngine;

namespace Scripts.StaticData.Level
{

    [CreateAssetMenu(menuName = "StaticData/LevelStaticData", fileName = "LevelStaticData", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        [Header("Level Generation")]
        public int LevelLenght = 20;
        public int MinBlockToTurn = 10;
        public int MaxBlockToTurn = 15;
    }

}