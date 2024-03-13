using System.Collections.Generic;
using Assets.FlappyTerminator.CodeBase.Infrastructure.AssetManagement;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.FlappyTerminator.CodeBase.Logic;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public List<IRestarter> Restarters { get; } = new List<IRestarter>();

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreatePlayer(GameObject at) => 
            InstantiateRegistered(AssetPath.PlayerPath, at.transform.position);

        public GameObject CreateScoreCounter() =>
            InstantiateRegistered(AssetPath.ScoreCounterPath);

        public GameObject CreateHUD() => 
            InstantiateRegistered(AssetPath.HUDPath);

        public GameObject CreateEnemySpawner() =>
            InstantiateRegistered(AssetPath.EnemySpawnerPath);

        public GameObject CreateBulletSpawner() =>
            InstantiateRegistered(AssetPath.BulletSpawnerPath);

        public GameObject CreateShooter(Vector3 at) => 
            InstantiateRegistered(AssetPath.ShooterPath, at);

        public GameObject CreateBullet() =>
            InstantiateRegistered(AssetPath.BulletPath);

        public GameObject CreateDestroyer() =>
            InstantiateRegistered(AssetPath.EnemyDestroyerPath);

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath, at: at);
            RegisterProgressWatchers(gameObject);
            RegisterRestarters(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            RegisterRestarters(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private void RegisterRestarters(GameObject gameObject)
        {
            foreach (IRestarter restarter in gameObject.GetComponentsInChildren<IRestarter>())
                Restarters.Add(restarter);
        }
    }
}