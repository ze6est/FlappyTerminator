using UnityEngine;
using UnityEngine.UI;

namespace Assets.FlappyTerminator.CodeBase.Logic
{
    public class Window : MonoBehaviour, IRestarter
    {
        [SerializeField] private Image _image;

        public void SetColor(Color color) =>
            _image.color = color;

        public void SetAlpha(float alpha)
        {
            Color currentColor = _image.color;
            currentColor.a = alpha;
            _image.color = currentColor;
        }

        public void Restart()
        {
            gameObject.SetActive(true);
        }
    }
}