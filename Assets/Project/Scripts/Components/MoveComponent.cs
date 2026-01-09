using UnityEngine;

namespace Project.Scripts.Components
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed = 5f; // Скорость поворота

        private float _dirX = 0;
        private float _dirZ = 0;

        private void OnEnable()
        {
            _inputController.OnMoveX += OnMoveX;
            _inputController.OnMoveZ += OnMoveZ;
        }

        private void OnDestroy()
        {
            _inputController.OnMoveX -= OnMoveX;
            _inputController.OnMoveZ += OnMoveZ;
        }

        private void FixedUpdate()
        {
            Vector3 direction = new Vector3(_dirX, 0, _dirZ).normalized;
            _rigidbody.linearVelocity = direction * _speed;
            
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                
                Quaternion newRotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, Time.fixedDeltaTime * _rotationSpeed);
                _rigidbody.rotation = newRotation;
            }
        }

        public void UpgradeMoveSpeed(float addSpeed)
        {
            _speed += addSpeed;
        }
        
        private void OnMoveX(float directionX)
        {
            _dirX = directionX;
            //Debug.Log(_dirX);
        }
        
        private void OnMoveZ(float directionZ)
        {
            _dirZ = directionZ;
            //Debug.Log(_dirZ);
        }
    }
}