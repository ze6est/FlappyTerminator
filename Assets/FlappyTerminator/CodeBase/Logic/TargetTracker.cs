﻿using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase
{
    public class TargetTracker : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _offsetX;

        private void LateUpdate()
        {
            if (_target != null)
            {
                Vector3 position = transform.position;
                position.x = _target.position.x + _offsetX;
                transform.position = position;
            }            
        }

        public void Follow(GameObject target) => 
            _target = target.transform;
    }
}