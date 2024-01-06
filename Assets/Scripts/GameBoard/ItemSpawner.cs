using UnityEngine;
using Items;
using Random = UnityEngine.Random;

namespace GameBoard
{
    public class ItemSpawner
    {
        private readonly Item[] _itemPrefabs;
        private readonly MatchFinder _matchFinder;
        private readonly Transform _parentTransform;
        private float _tweenDuration;

        public ItemSpawner(Item[] itemPrefabs, MatchFinder matchFinder, Transform parentTransform, float tweenDuration)
        {
            _itemPrefabs = itemPrefabs;
            _matchFinder = matchFinder;
            _parentTransform = parentTransform;
            _tweenDuration = tweenDuration;
        }

        public Item SpawnItem(Item[,] items, Vector2 position)
        {
            var index = Random.Range(0, _itemPrefabs.Length);
            var item = _itemPrefabs[index];

            while (_matchFinder.HasMatchesWithAdjacentItems(items, position, item))
            {
                index = Random.Range(0, _itemPrefabs.Length);
                item = _itemPrefabs[index];
            }

            item = Object.Instantiate(_itemPrefabs[index], _parentTransform);
            item.transform.position = position;

            return item;
        }
    }
}