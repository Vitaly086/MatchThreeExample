using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverPopup : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _finalScoreLabel;

        public void SetFinalScore(int value)
        {
            _finalScoreLabel.text = value.ToString();
        }
    }
}