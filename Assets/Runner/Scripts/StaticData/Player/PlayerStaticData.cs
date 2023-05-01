using UnityEngine;

namespace Scripts.StaticData.Player
{
    [CreateAssetMenu(menuName = "StaticData/Player", fileName = "PlayerConfig", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        public GameObject Prefab;
        
        [Header("Move Settings"), Space(10)]
        public float MoveSpeed = 15;
        public float RotationSpeed = 5;
        public float SpeedBoostTime = 3f;


        [Header("Jump Settings"), Space(10)]
        public float JumpHeight;

        public float DefaultJumpDuration = 0.3f;
        public float DoubleJumpDuration = 0.4f;


        [Header("Health Settings"), Space(10)]
        public int StartHealth;

        [Header("Input Settings"), Space(10)]
        public float DoubleTapTime = 0.21f;
    }
}