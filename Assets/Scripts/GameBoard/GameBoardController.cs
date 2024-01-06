using System;
using System.Collections.Generic;
using DG.Tweening;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameBoard
{
    public class GameBoardController : MonoBehaviour
    {
        public Action<Item[,]> ReFill;
        public Action<int> ItemsDestroyed;

        public float CellSize => _tilePrefab.transform.localScale.x;

        [SerializeField]
        private int _width;
        [SerializeField]
        private int _height;
        [SerializeField]
        private GameObject _tilePrefab;

        [SerializeField]
        private float _tweenDuration = 0.4f;

        private Item[] _itemPrefabs;
        private Item[,] _items;
        private ItemSpawner _itemSpawner;
        private GameBoardCreator _boardCreator;


        public void Initialize(Item[] itemsPrefabs, int itemTypesNumber, MatchFinder matchFinder)
        {
            _itemPrefabs = itemsPrefabs;
            Array.Resize(ref _itemPrefabs, itemTypesNumber);
            _itemSpawner = new ItemSpawner(_itemPrefabs, matchFinder, transform, _tweenDuration);
            _boardCreator = new GameBoardCreator(_tilePrefab, transform, _width, _height);
        }

        public Item[,] CreateGameBoard()
        {
            _items = new Item[_width, _height];
            _boardCreator.CreateBoard();
            FillGameBoard();
            return _items;
        }


        public void DestroyMatches(IReadOnlyList<Item> items)
        {
            var sequence = DOTween.Sequence();

            foreach (var item in items)
            {
                sequence.Join(item.Hide(_tweenDuration));
                ItemsDestroyed?.Invoke(item.Value);
            }

            sequence.AppendInterval(_tweenDuration / 3);
            sequence.OnComplete(() =>
            {
                var movingSequence = MoveItemsDown();

                movingSequence.OnComplete(() =>
                {
                    var sequence = FillGameBoard();
                    sequence.OnComplete(() => ReFill?.Invoke(_items));
                });
            });
        }

      
        private Sequence FillGameBoard()
        {
            var sequence = DOTween.Sequence();

            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    if (_items[x, y] == null)
                    {
                        var item = _itemSpawner.SpawnItem(_items, new Vector2(x, y));
                        _items[x, y] = item;
                        sequence.Join(item.Show(_tweenDuration));
                    }
                }
            }

            return sequence;
        }

        private Sequence MoveItemsDown()
        {
            var emptyRowsCounter = 0;

            var sequence = DOTween.Sequence();

            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    if (_items[x, y] == null)
                    {
                        emptyRowsCounter++;
                    }
                    else if (emptyRowsCounter > 0)
                    {
                        var item = _items[x, y];
                        var itemPosition = item.transform.position;

                        itemPosition.y -= emptyRowsCounter;

                        sequence.Join(item.transform.DOMove(itemPosition, _tweenDuration));

                        _items[x, y - emptyRowsCounter] = _items[x, y];
                        _items[x, y] = null;
                    }
                }

                emptyRowsCounter = 0;
            }

            return sequence;
        }
    }
}