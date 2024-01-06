using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    /// <summary>
    /// Менеджер счета игрока
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        public UnityEvent<int> ScoreIncreased;
        public int Score => _score;
        private int _score;

        public void IncreaseScore(int value)
        {
            _score += value;
            ScoreIncreased?.Invoke(_score);
        } 
    }
}