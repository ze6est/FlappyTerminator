using Assets.FlappyTerminator.CodeBase.Enemies;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Factory;
using Assets.FlappyTerminator.CodeBase.Logic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IRestarter
{
    private const int EnemyPreloadCount = 10;

    [SerializeField] private float _offsetX;
    [SerializeField] private float _spawnTime = 2;

    private float _currentTime;
    private float _currentSpawnTime;

    private IGameFactory _gameFactory;
    private BulletSpawner _bulletSpawner;
    private GameObject _player;
    private PoolBase<Enemy> _enemyPool;

    public void Construct(IGameFactory gameFactory, BulletSpawner bulletSpawner, GameObject player)
    {
        _gameFactory = gameFactory;
        _bulletSpawner = bulletSpawner;
        _player = player;

        _enemyPool = new PoolBase<Enemy>(Preload, GetAction, ReturnAction, EnemyPreloadCount);
    }

    private void Start()
    {
        _currentSpawnTime = _spawnTime;
        _currentTime = _spawnTime;
    }

    private void Update()
    {
        _currentSpawnTime -= _currentSpawnTime / 50 * Time.deltaTime;
        _currentTime -= Time.deltaTime;

        if(_currentTime <= 0) 
        {
            Enemy enemy = _enemyPool.Get();
            enemy.transform.position = new Vector3(_player.transform.position.x + _offsetX, _player.transform.position.y, 0);
            ObjectSwitch objectswitch = enemy.GetComponent<ObjectSwitch>();
            objectswitch.CollisionDetected(OnCollisionDetected);

            if (enemy is Shooter shooter)
                shooter.Construct(_bulletSpawner);

            _currentTime = _currentSpawnTime;

            void OnCollisionDetected() => 
                _enemyPool.Return(enemy);
        }
    }

    public void Restart()
    {
        _currentSpawnTime = _spawnTime;
        _enemyPool.ReturnAll();
    }

    private Enemy Preload() =>
        _gameFactory.CreateShooter(transform.position).GetComponent<Shooter>();

    private void GetAction(Enemy enemy) =>
        enemy.gameObject.SetActive(true);

    private void ReturnAction(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}