using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Logic
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;        

        private Vector3 _speed;

        public void SetSpeed(Vector3 speed) =>
             _speed = speed;

        private void Update() => 
            transform.Translate(_speed * Time.deltaTime, Space.Self);
    }
}