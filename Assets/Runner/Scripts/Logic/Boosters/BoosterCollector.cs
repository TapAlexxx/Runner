using System;
using UnityEngine;

namespace Scripts.Logic.Boosters
{

    public class BoosterCollector : MonoBehaviour
    {
        public event Action<int> SpeedCollected;
        public event Action<int> ShieldCollected;
        public event Action<int> HealCollected;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Booster booster))
            {
                switch (booster.BoosterType)
                {
                    case BoosterType.Speed:
                        SpeedCollected?.Invoke(booster.BoostValue);
                        break;
                    case BoosterType.Shield:
                        ShieldCollected?.Invoke(booster.BoostValue);
                        break;
                    case BoosterType.Heal:
                        HealCollected?.Invoke(booster.BoostValue);
                        break;
                }

                other.gameObject.SetActive(false);
            }
        }
    }

}