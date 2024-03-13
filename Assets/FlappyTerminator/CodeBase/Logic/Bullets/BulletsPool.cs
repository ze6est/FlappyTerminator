using Assets.FlappyTerminator.CodeBase.Infrastructure.Factory;

namespace Assets.FlappyTerminator.CodeBase.Logic.Bullets
{
    public class BulletsPool
    {
        private const int BulletPreloadCount = 30;

        private readonly IGameFactory _gameFactory;
        private readonly ScoreCounter _scoreCounter;

        private PoolBase<Bullet> _bulletPool;        

        public BulletsPool(IGameFactory gameFactory, ScoreCounter scoreCounter)
        {
            _gameFactory = gameFactory;
            _scoreCounter = scoreCounter;

            _bulletPool = new PoolBase<Bullet>(Preload, GetAction, ReturnAction, BulletPreloadCount);
        }        

        public Bullet GetBullet()
        {
            Bullet bullet = _bulletPool.Get();            
            ObjectSwitch objectswitch = bullet.GetComponent<ObjectSwitch>();

            objectswitch.CollisionDetected(OnCollisionDetected);

            void OnCollisionDetected() =>
                _bulletPool.Return(bullet);

            return bullet;
        }

        public void ReturnAll() => 
            _bulletPool.ReturnAll();

        private Bullet Preload()
        {
            Bullet bullet = _gameFactory.CreateBullet().GetComponent<Bullet>();

            CollisionTracker collisionTracker = bullet.GetComponent<CollisionTracker>();
            collisionTracker.EnemyKilled += OnEnemyKilled;

            return bullet;
        }

        private void GetAction(Bullet bullet) =>
            bullet.gameObject.SetActive(true);

        private void ReturnAction(Bullet bullet) =>
            bullet.gameObject.SetActive(false);

        private void OnEnemyKilled(int point) => 
            _scoreCounter.ChangeScore(point);
    }
}