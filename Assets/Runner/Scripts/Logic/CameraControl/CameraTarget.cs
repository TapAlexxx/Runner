using UnityEngine;

namespace Scripts.Logic.CameraControl
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private Transform _lookAtTransform;
        [SerializeField] private Rigidbody _ownRigidbody;
        [SerializeField] private float _xDamping;
        [SerializeField] private float _yDamping;
        [SerializeField] private float _zDamping;

        private void Start() =>
            transform.parent = null;

        private void FixedUpdate()
        {
            Vector3 rbPos = _ownRigidbody.position;
            rbPos.x = Mathf.Lerp(rbPos.x, _lookAtTransform.position.x, _xDamping * Time.fixedDeltaTime);
            rbPos.y = Mathf.Lerp(rbPos.y, _lookAtTransform.position.y, _yDamping * Time.fixedDeltaTime);
            rbPos.z = Mathf.Lerp(rbPos.z, _lookAtTransform.position.z, _zDamping * Time.fixedDeltaTime);
            _ownRigidbody.MoveRotation(_lookAtTransform.rotation);
            _ownRigidbody.MovePosition(rbPos);
        }
    }
}