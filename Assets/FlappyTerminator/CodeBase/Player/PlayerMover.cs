using Assets.FlappyTerminator.CodeBase.Infrastructure.AssetManagement;
using Assets.FlappyTerminator.CodeBase.Services.Inputs;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _tapForce;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxRotationZ;
        [SerializeField] private float _minRotationZ;

        private IInputService _inputService;

        private Quaternion _maxRotation;
        private Quaternion _minRotation;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
            _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
        }

        private void Update()
        {
            if (_inputService.IsJumpButtonDown())
            {
                _rigidbody2D.velocity = new Vector2(_speed, _tapForce);
                transform.rotation = _maxRotation;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}