using Assets.FlappyTerminator.CodeBase.Logic;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Enemies
{
    public class Shooter : Enemy
    {
        [SerializeField] private float _maxDelayBetweenShots = 2f;
        [SerializeField] private float _minDelayBetweenShots = 0.5f;
        [SerializeField] private int _point;        

        private BulletSpawner _bulletSpawner;
        private float _currentTime;

        public void Construct(BulletSpawner bulletSpawner) => 
            _bulletSpawner = bulletSpawner;

        private void Start()
        {
            Point = _point;
            _currentTime = Random.Range(_minDelayBetweenShots, _maxDelayBetweenShots);            
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                _bulletSpawner.Spawn(transform.position, transform.rotation, Vector3.left, gameObject);

                _currentTime = Random.Range(_minDelayBetweenShots, _maxDelayBetweenShots);
            }
        }
    }
}