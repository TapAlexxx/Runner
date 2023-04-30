using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private PlayerTurnControl playerTurnControl;
        
        private float _currentSpeed;
        private Vector3 _currentDirection;
        private Vector3 _targetDirection;
        private Vector3 _targetRotation;

        public float NormalizedSpeed => _currentSpeed / speed;

        private void OnValidate()
        {
            playerTurnControl = GetComponentInChildren<PlayerTurnControl>();
        }

        private void Start()
        {
            playerTurnControl.TurnedLeft += TurnLeft;
            playerTurnControl.TurnedRight += TurnRight;
            InitializeDefault();
        }

        private void OnDestroy()
        {
            playerTurnControl.TurnedLeft -= TurnLeft;
            playerTurnControl.TurnedRight -= TurnRight;
        }

        private void InitializeDefault()
        {
            _targetDirection = Vector3.forward;
            _currentDirection = _targetDirection;
            _targetRotation = Vector3.zero;
        }

        private void Update()
        {
            Move();
            UpdateSpeed();
            UpdateDirection();
            
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_targetRotation), rotationSpeed * Time.deltaTime);
        }

        private void TurnLeft()
        {
            _targetRotation += new Vector3(0, -90, 0);
            UpdateTargetDirection();
        }

        private void TurnRight()
        {
            _targetRotation += new Vector3(0, 90, 0);
            UpdateTargetDirection();
        }

        private void UpdateTargetDirection()
        {
            if(_targetRotation.y < -1f)
                _targetDirection = Vector3.left;
            else if (_targetRotation.y > 1f) 
                _targetDirection = Vector3.right;
            else
                _targetDirection = Vector3.forward;
        }

        private void UpdateDirection() => 
            _currentDirection = Vector3.Lerp(_currentDirection, _targetDirection, rotationSpeed * Time.deltaTime);

        private void Move() => 
            transform.position += _currentDirection * (_currentSpeed * Time.deltaTime);

        private void UpdateSpeed() =>
            _currentSpeed = Mathf.Lerp(_currentSpeed, speed, Time.fixedDeltaTime);
    }
}