using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float speed;
        private float _elapsedTime;

        private void Update()
        {
            float axis = Input.GetAxis("Vertical");

            transform.position += Vector3.forward * (axis * speed * Time.deltaTime);
        }
    }
}