using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace BoardSurface
{
    /// <summary>
    /// テクスチャなどの挙動を管理する
    /// </summary>
    [System.Serializable]
    public class BoardSurfaceManager_View
    {
        [SerializeField]
        private BoardSurfaceViewTileArray[] _viewTiles;
        [SerializeField]
        private BoardSurfaceViewSelectTile _selectTile;


        /// <summary>
        /// タイル選択カーソルを移動
        /// </summary>
        /// <param name="pos"></param>
        public void OnMoveTile(Vector2Int pos)
        {
            MyLogger.Log("BoardSurfaceManager_View OnMoveTile 引数：pos[" + pos + "]");

            Vector3 nextPos = _viewTiles[pos.y].ArrayX[pos.x].transform.localPosition;
            _selectTile.OnMoveTile(nextPos);
        }

        /// <summary>
        /// タイルを選択された状態に変更
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tileState"></param>
        /// <param name="pieceType"></param>
        public void OnSelectTile(Vector2Int pos, EnumTileState tileState, EnumPieceType pieceType)
        {
            MyLogger.Log("BoardSurfaceManager_View OnSelectTile 引数：pos[" + pos + "] tileState[" + tileState + "] pieceType[" + pieceType + "]");

            _viewTiles[pos.y].ArrayX[pos.x].OnChangeTileAndPiece(tileState, pieceType);
        }

        /// <summary>
        /// 配置対象の駒を変更する
        /// </summary>
        /// <param name="pieceType"></param>
        public void OnChangePieceType(EnumPieceType pieceType)
        {
            MyLogger.Log("BoardSurfaceManager_View OnChangePieceType 引数：pieceType[" + pieceType + "]");

            _selectTile.OnChangePieceType(pieceType);
        }

        /// <summary>
        /// 操作されているプレイヤーを変更する
        /// </summary>
        /// <param name="tileState"></param>
        public void OnChangePlayer(EnumTileState tileState)
        {
            MyLogger.Log("BoardSurfaceManager_View OnChangePlayer 引数：tileState[" + tileState + "]");

            _selectTile.OnChangePlayer(tileState);
        }

        /// <summary>
        /// タイルを空の状態にする
        /// </summary>
        public void OnResetBoard()
        {
            MyLogger.Log("BoardSurfaceManager_View OnResetBoard");

            foreach (BoardSurfaceViewTileArray tileArray in _viewTiles)
            {
                foreach (BoardSurfaceViewTile tile in tileArray.ArrayX)
                    tile.OnReset();
            }
        }
    }

    [System.Serializable]
    public class BoardSurfaceViewTileArray
    {
        public BoardSurfaceViewTile[] ArrayX;
    }
}