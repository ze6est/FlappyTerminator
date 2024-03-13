using Assets.FlappyTerminator.CodeBase.Data;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.FlappyTerminator.CodeBase.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.FlappyTerminator.CodeBase.Logic
{
    public class ScoreCounter : MonoBehaviour, ISavedProgress
    {
        private PlayerDeath _playerDeath;

        private int _score;
        private int _currentScore;

        public event UnityAction<int> ScoreChanged;
        public event UnityAction<int> ScoreUpdated;

        public void Construct(PlayerDeath playerDeath)
        {
            _playerDeath = playerDeath;

            _playerDeath.Died += OnDied;
        }

        public void ChangeScore(int point)
        {
            _currentScore += point;
            ScoreChanged?.Invoke(_currentScore);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _score = progress.Score;
            _currentScore = progress.CurrentScore;
            ScoreChanged?.Invoke(_currentScore);
        }

        public void UpdateProgress(PlayerProgress progress)
        {            
            progress.Score = _score;
        }

        private void OnDied()
        {
            ScoreUpdated?.Invoke(_currentScore);
            _score += _currentScore;            
        }
    }
}