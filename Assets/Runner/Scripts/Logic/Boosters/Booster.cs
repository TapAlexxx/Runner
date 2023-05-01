using UnityEngine;

namespace Scripts.Logic.Boosters
{

    public class Booster : MonoBehaviour
    {
        [field:SerializeField] public BoosterType BoosterType { get; private set; }
        [field: SerializeField] public int BoostValue { get; private set; }
        [field: SerializeField] public float ChanceToAppear { get; private set; }
    }

}