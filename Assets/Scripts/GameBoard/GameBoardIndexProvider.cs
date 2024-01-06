using UnityEngine;

namespace GameBoard
{
    public class GameBoardIndexProvider
    {
        private readonly GameBoardController _gameBoardController;

        public GameBoardIndexProvider(GameBoardController gameBoardController)
        {
            _gameBoardController = gameBoardController;
        }
    
        public Vector2Int GetIndex(Vector3 worldPosition)
        {
            var tilePositionInMap = _gameBoardController.transform.InverseTransformPoint(worldPosition);

            var halfCellSize = _gameBoardController.CellSize / 2;

            var x = Mathf.FloorToInt(tilePositionInMap.x + halfCellSize);
            var y = Mathf.FloorToInt(tilePositionInMap.y + halfCellSize);

            return new Vector2Int(x, y);
        }
    }
}