using TMPro;
using UnityEngine;

namespace UI
{
    public class MovesView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _movesLabel;

        public void SetMoves(int value)
        {
            _movesLabel.text = value.ToString();
        }
    }
}