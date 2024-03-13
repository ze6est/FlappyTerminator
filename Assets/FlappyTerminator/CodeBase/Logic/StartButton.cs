using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.FlappyTerminator.CodeBase.Logic
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event UnityAction GameStarted;

        private void Awake() => 
            _button.onClick.AddListener(StartGame);

        private void OnDestroy() => 
            _button.onClick.RemoveListener(StartGame);

        private void StartGame() => 
            GameStarted?.Invoke();
    }
}