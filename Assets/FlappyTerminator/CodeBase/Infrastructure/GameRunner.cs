using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapperPrefab;

        private void Awake()
        {
            GameBootstrapper bootstrapper = FindFirstObjectByType<GameBootstrapper>();

            if (bootstrapper == null )
                Instantiate(_bootstrapperPrefab);
        }
    }
}