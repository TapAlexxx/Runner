using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class GroundChecker : MonoBehaviour
    {
        public bool Grounded => transform.position.y < 0.6f;
    }

}