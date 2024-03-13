using Assets.FlappyTerminator.CodeBase.Logic;
using Assets.FlappyTerminator.CodeBase.Player;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HPBar _hpBar;
        [SerializeField] private ScoreCounterView _scoreCounterView;

        private PlayerHealth _playerHealth;
        private ScoreCounter _scoreCounter;

        public void Construct(PlayerHealth playerHealth, ScoreCounter scoreCounter)
        {
            _playerHealth = playerHealth;
            _scoreCounter = scoreCounter;

            _playerHealth.HealthChanged += OnHealthChanged;
            _scoreCounter.ScoreChanged += OnScoreChanged;
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= OnHealthChanged;
            _scoreCounter.ScoreChanged -= OnScoreChanged;
        }        

        private void OnHealthChanged() => 
            UpdateHPBar();

        private void OnScoreChanged(int score) => 
            _scoreCounterView.SetScore(score);

        private void UpdateHPBar() => 
            _hpBar.SetValue(_playerHealth.Current, _playerHealth.Max);
    }
}