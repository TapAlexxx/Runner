using System.Collections;
using Scripts.StaticData.Player;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.MovementControl
{

    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private PlayerTurnControl playerTurnControl;
        
        private float _moveSpeed;
        private float _rotationSpeed;
        private float _currentSpeed;
        private Vector3 _currentDirection;
        private Vector3 _targetDirection;
        private Vector3 _targetRotation;
        private bool _active;
        private Coroutine _stopMoveCoroutine;

        public float NormalizedSpeed => _currentSpeed / _moveSpeed;

        private void OnValidate()
        {
            playerTurnControl = GetComponentInChildren<PlayerTurnControl>();
        }

        public void Initialize(PlayerStaticData staticData)
        {
            _moveSpeed = staticData.MoveSpeed;
            _rotationSpeed = staticData.RotationSpeed;
            InitializeDefault();
        }

        private void InitializeDefault()
        {
            _targetDirection = Vector3.forward;
            _currentDirection = _targetDirection;
            _targetRotation = Vector3.zero;
        }

        public void Activate()
        {
            if (_stopMoveCoroutine != null)
            {
                StopCoroutine(_stopMoveCoroutine);
                _stopMoveCoroutine = null;
            }
            
            _active = true;
        }

        public void Disable()
        {
            _active = false;
            _stopMoveCoroutine = StartCoroutine(StopMove());
        }

        private IEnumerator StopMove()
        {
            while (_currentSpeed != 0)
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, _moveSpeed, Time.deltaTime);
                yield return null;
            }
        }

        private void Start()
        {
            playerTurnControl.TurnedLeft += TurnLeft;
            playerTurnControl.TurnedRight += TurnRight;
        }

        private void OnDestroy()
        {
            playerTurnControl.TurnedLeft -= TurnLeft;
            playerTurnControl.TurnedRight -= TurnRight;
        }

        private void Update()
        {
            if(!_active)
                return;
            
            Move();
            UpdateSpeed();
            UpdateDirection();
            
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_targetRotation), _rotationSpeed * Time.deltaTime);
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
            _currentDirection = Vector3.Lerp(_currentDirection, _targetDirection, _rotationSpeed * Time.deltaTime);

        private void Move() => 
            transform.position += _currentDirection * (_currentSpeed * Time.deltaTime);

        private void UpdateSpeed() =>
            _currentSpeed = Mathf.Lerp(_currentSpeed, _moveSpeed, Time.deltaTime);
    }
}