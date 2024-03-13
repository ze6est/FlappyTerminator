using Assets.FlappyTerminator.CodeBase.Enemies;
using Assets.FlappyTerminator.CodeBase.Logic.Bullets;
using Assets.FlappyTerminator.CodeBase.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.FlappyTerminator.CodeBase.Logic
{
    public class CollisionTracker : MonoBehaviour
    {
        public event UnityAction<int> EnemyKilled;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(gameObject.layer == 6)
            {
                if(collision.gameObject.TryGetComponent(out Bullet bullet))
                {
                    DisableObject(bullet.transform);
                    DisableObject(gameObject.transform);
                }
                else if(collision.gameObject.TryGetComponent(out Enemy enemy))
                {
                    DisableObject(enemy.transform);
                    DisableObject(gameObject.transform);

                    EnemyKilled?.Invoke(enemy.GetPoint);
                }
            }

            if(gameObject.layer == 11)
            {
                if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
                {
                    DisableObject(gameObject.transform);
                    playerHealth.TakeDamage(1f);
                }
            }            
        }

        private void DisableObject(Transform obj)
        {
            if (obj.TryGetComponent(out ObjectSwitch objectSwitch))
                objectSwitch.DisableObject();
        }
    }
}