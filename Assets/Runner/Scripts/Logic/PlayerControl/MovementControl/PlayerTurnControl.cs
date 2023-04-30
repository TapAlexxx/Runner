using System;
using Scripts.Logic.LevelGeneration;
using Scripts.Logic.LevelGeneration.Blocks;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class PlayerTurnControl : MonoBehaviour
    {
        public event Action TurnedLeft;
        public event Action TurnedRight;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TurnBlock block))
            {
                switch (block.Turn)
                {
                    case Turn.Left:
                        TurnedLeft?.Invoke();
                        break;
                    case Turn.Right:
                        TurnedRight?.Invoke();
                        break;
                }
            }
        }
    }

}