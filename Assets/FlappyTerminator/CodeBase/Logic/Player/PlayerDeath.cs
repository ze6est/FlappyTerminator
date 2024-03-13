using Assets.FlappyTerminator.CodeBase.Logic.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.FlappyTerminator.CodeBase.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] PlayerHealth _health;        
        [SerializeField] PlayerCollisionTracker _collisionTracker;        

        public event UnityAction Died;

        private void Start() => 
            _health.HealthChanged += OnHealthChanged;

        private void OnEnable() => 
            _collisionTracker.WithDestroyerFaced += OnWithDestroyerFaced;

        private void OnDisable() => 
            _collisionTracker.WithDestroyerFaced -= OnWithDestroyerFaced;

        private void OnDestroy() => 
            _health.HealthChanged -= OnHealthChanged;

        private void OnWithDestroyerFaced() => 
            Die();

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die() => 
            Died?.Invoke();
    }
}