using Assets.FlappyTerminator.CodeBase.Enemies;
using Assets.FlappyTerminator.CodeBase.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.FlappyTerminator.CodeBase.Logic.Player
{
    public class PlayerCollisionTracker : MonoBehaviour
    {
        public event UnityAction WithDestroyerFaced;        

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                DisableObject(enemy.transform);

                if (gameObject.TryGetComponent(out PlayerHealth playerHealth))
                {
                    playerHealth.TakeDamage(1f);
                }
            }
            else if(collision.gameObject.TryGetComponent(out Destroyer destroyer))
            {
                WithDestroyerFaced?.Invoke();
            }
        }

        private void DisableObject(Transform obj)
        {
            if (obj.TryGetComponent(out ObjectSwitch objectSwitch))
                objectSwitch.DisableObject();
        }
    }
}