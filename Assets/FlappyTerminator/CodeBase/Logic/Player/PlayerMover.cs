using Assets.FlappyTerminator.CodeBase.Data;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.FlappyTerminator.CodeBase.Logic;
using Assets.FlappyTerminator.CodeBase.Services.Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.FlappyTerminator.CodeBase.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour, ISavedProgress, IRestarter
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
        private Vector3 _startPoint;
        private float _currentSpeed;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _startPoint = transform.position;
            _currentSpeed = _speed;

            _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
            _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
        }

        private void Update()
        {            
            if (_inputService.IsJumpButtonDown())
            {
                _rigidbody2D.velocity = new Vector2(_currentSpeed, _tapForce);
                transform.rotation = _maxRotation;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);

            _currentSpeed += _currentSpeed / 50 * Time.deltaTime;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if(GetCurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

                if(savedPosition != null)
                    transform.position = savedPosition.AsUnityVector3();
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(GetCurrentLevel(), _startPoint.AsVector3Data());
        }

        public void Restart()
        {
            _currentSpeed = _speed;
            transform.position = _startPoint;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            enabled = false;
        }

        private static string GetCurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}