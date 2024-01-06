using DG.Tweening;
using UnityEngine;

namespace Items
{
   
    public class Item : MonoBehaviour
    {
        [field: SerializeField]
        public ItemType Type { get; private set; }
        [field: SerializeField] 
        public int Value { get; private set; } = 1;

        public Tween Show(float tweenDuration)
        {
            var itemScale = transform.localScale;
            transform.localScale = Vector3.zero;
            var tween = transform.DOScale(itemScale, tweenDuration);
            return tween;
        }
    
        public Tween Hide(float tweenDuration)
        {
            var tween = transform.DOScale(0, tweenDuration)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    Destroy(gameObject);
                });

            return tween;
        }
    
        public Tween Move(Vector2Int position, float tweenDuration)
        {
            var tween = transform.DOMove(new Vector3(position.x, position.y), tweenDuration);
            return tween;
        }
    }
}