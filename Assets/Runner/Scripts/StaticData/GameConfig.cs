using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(menuName  = "StaticData/GameConfig", fileName= "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public string StartScene;
        public bool CanRunCurrent;
    }
}