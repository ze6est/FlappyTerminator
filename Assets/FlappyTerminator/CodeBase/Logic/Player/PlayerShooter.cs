using Assets.FlappyTerminator.CodeBase.Infrastructure.Services;
using Assets.FlappyTerminator.CodeBase.Services.Inputs;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Logic.Player
{
    public class PlayerShooter : MonoBehaviour, IRestarter
    {
        private IInputService _inputService;
        private BulletSpawner _bulletSpawner;
        
        private Quaternion _previousRotation;
        private Quaternion _currentRotation;

        private float _currentBulletSpeed;

        public void Construct(BulletSpawner bulletSpawner) => 
            _bulletSpawner = bulletSpawner;

        private void Awake() => 
            _inputService = AllServices.Container.Single<IInputService>();        

        private void Update()
        {
            _currentBulletSpeed += _currentBulletSpeed / 50 * Time.deltaTime;

            _currentRotation = transform.localRotation;            
            
            if (_inputService.IsFireButtonDown())            
                _bulletSpawner.Spawn(transform.position, _previousRotation, Vector3.right, gameObject);            

            if(_previousRotation != _currentRotation)
                _previousRotation = _currentRotation;
        }

        public void Restart() => 
            enabled = false;
    }
}