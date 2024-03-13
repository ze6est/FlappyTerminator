using Assets.FlappyTerminator.CodeBase.Infrastructure.Factory;
using Assets.FlappyTerminator.CodeBase.Logic.Bullets;
using Assets.FlappyTerminator.CodeBase.Logic.Player;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Logic
{
    public class BulletSpawner : MonoBehaviour, IRestarter
    {
        [SerializeField] private float _speedBullet;

        private IGameFactory _gameFactory;        
        private BulletsPool _pool;

        private float _currentSpeedBullet;

        public void Construct(IGameFactory gameFactory, ScoreCounter scoreCounter)
        {
            _gameFactory = gameFactory;                      

            _pool = new BulletsPool(_gameFactory, scoreCounter);
        }

        private void Start() => 
            _currentSpeedBullet = _speedBullet;

        private void Update() => 
            _currentSpeedBullet += _currentSpeedBullet / 50 * Time.deltaTime;

        public void Spawn(Vector3 at, Quaternion rotation, Vector3 direction, GameObject origin)
        {
            Bullet bullet = _pool.GetBullet();

            bullet.transform.position = at;
            bullet.transform.rotation = rotation;

            if (origin.TryGetComponent(out PlayerShooter _))
            {
                bullet.gameObject.layer = 6;
                bullet.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            }
            else
            {
                bullet.gameObject.layer = 11;
                bullet.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }

            Mover mover = bullet.GetComponent<Mover>();
            mover.SetSpeed(direction * _currentSpeedBullet);
        }

        public void Restart()
        {
            _currentSpeedBullet = _speedBullet;
            _pool.ReturnAll();
        }
    }
}