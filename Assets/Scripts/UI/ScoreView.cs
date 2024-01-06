using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _scoreLabel;
        [SerializeField] 
        private float _calculatingTime = 0.5f;
        private float _currentScore;

        private Coroutine _moneyCalculationCoroutine;

        public void SetScore(int score)
        {
            if (_moneyCalculationCoroutine != null)
            {
                StopCoroutine(_moneyCalculationCoroutine);
            }
        
            _moneyCalculationCoroutine = StartCoroutine(ShowMoneyCalculation(score, _calculatingTime));
        }
    
        private IEnumerator ShowMoneyCalculation(float newScore, float calculatingTime)
        {
            var currentTime = 0f;

            while (currentTime < calculatingTime)
            {
                var interpolatingScore = Mathf.Lerp(_currentScore, newScore, currentTime / calculatingTime);
                float incrementScorePerFrame = Mathf.CeilToInt(interpolatingScore);
                currentTime += Time.deltaTime;
                _scoreLabel.text = incrementScorePerFrame.ToString();
            
                yield return null;
            }

            _currentScore = newScore;
            _scoreLabel.text = _currentScore.ToString();
        }
    }
}