using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Enemies
{
    public class Enemy : MonoBehaviour
    {
        protected int Point;

        public int GetPoint =>
            Point;
    }
}