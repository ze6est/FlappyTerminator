using System;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Logic
{
    public class ObjectSwitch : MonoBehaviour
    {
        private Action _collisionDetected;

        public void CollisionDetected(Action CollisionDetected) => 
            _collisionDetected = CollisionDetected;

        public void DisableObject() => 
            _collisionDetected.Invoke();
    }
}
