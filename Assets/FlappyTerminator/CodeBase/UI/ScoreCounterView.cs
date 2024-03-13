using TMPro;
using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.UI
{
    public class ScoreCounterView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _score;

        public void SetScore(int score) =>
            _score.text = score.ToString();
    }
}