using UnityEngine;

namespace Scripts.StaticData.Player
{
    [CreateAssetMenu(menuName = "StaticData/Player", fileName = "PlayerConfig", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        public GameObject Prefab;
    }
}