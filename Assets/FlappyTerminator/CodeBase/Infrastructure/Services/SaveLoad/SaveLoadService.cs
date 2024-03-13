using Assets.FlappyTerminator.CodeBase.Data;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Factory;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress1";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService ,IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)            
                progressWriter.UpdateProgress(_progressService.PlayerProgress);

            PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
        }
    }
}
