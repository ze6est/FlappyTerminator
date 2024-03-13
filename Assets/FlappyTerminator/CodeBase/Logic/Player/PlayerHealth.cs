using Assets.FlappyTerminator.CodeBase.Data;
using Assets.FlappyTerminator.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.FlappyTerminator.CodeBase.Logic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.FlappyTerminator.CodeBase.Player
{
    public class PlayerHealth : MonoBehaviour, ISavedProgress
    {
        private State _state;
        private ScoreCounter _scoreCounter;

        public float Current
        {
            get => _state.CurrentHP;
            set
            {
                if(_state.CurrentHP != value)
                {
                    _state.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        public event UnityAction HealthChanged;

        public void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;

            _scoreCounter.ScoreUpdated += OnScoreUpdated;
        }

        private void OnDestroy() => 
            _scoreCounter.ScoreUpdated -= OnScoreUpdated;

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.PlayerState;
            _state.ResetHP();
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerState.CurrentHP = Current;
            progress.PlayerState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            HealthChanged?.Invoke();            
        }

        private void OnScoreUpdated(int currentScore) => 
            _state.MaxHP += currentScore / 10;
    }
}