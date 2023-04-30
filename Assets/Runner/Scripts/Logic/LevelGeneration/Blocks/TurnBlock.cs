using UnityEngine;

namespace Scripts.Logic.LevelGeneration.Blocks
{
    public class TurnBlock : MonoBehaviour
    {
        [field:SerializeField] public Turn Turn { get; private set; }
    }

}