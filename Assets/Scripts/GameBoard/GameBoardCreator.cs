using UnityEngine;

namespace GameBoard
{
    public class GameBoardCreator
    {
        private readonly GameObject _tilePrefab;
        private readonly Transform _parentTransform;
        private readonly int _width;
        private readonly int _height;

        public GameBoardCreator(GameObject tilePrefab, Transform parentTransform, int width, int height)
        {
            _tilePrefab = tilePrefab;
            _parentTransform = parentTransform;
            _width = width;
            _height = height;
        }

        public void CreateBoard()
        {
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    var tile = Object.Instantiate(_tilePrefab, _parentTransform);
                    var tilePosition = new Vector2(x, y);
                    tile.transform.position = tilePosition;
                }
            }
        }
    }
}