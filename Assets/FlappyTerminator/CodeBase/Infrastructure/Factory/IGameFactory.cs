using System.Collections.Generic;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.FlappyTerminator.CodeBase.Logic;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }
        public List<IRestarter> Restarters { get; }

        void Cleanup();
        GameObject CreateBullet();
        GameObject CreateBulletSpawner();
        GameObject CreateDestroyer();
        GameObject CreateEnemySpawner();
        GameObject CreateHUD();
        GameObject CreatePlayer(GameObject at);
        GameObject CreateScoreCounter();
        GameObject CreateShooter(Vector3 at);
    }
}