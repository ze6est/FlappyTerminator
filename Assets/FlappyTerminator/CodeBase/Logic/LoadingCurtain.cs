using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private float _alphaStep = 0.03f;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide() => 
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            WaitForSeconds waitTime = new WaitForSeconds(_alphaStep);

            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= _alphaStep;
                yield return waitTime;
            }

            gameObject.SetActive(false);
        }
    }
}