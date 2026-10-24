using BoardSurface.CurrentTileData;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace BoardSurface
{
    /// <summary>
    /// 盤面の選択をするクラス
    /// </summary>
    [System.Serializable]
    public class BoardSurfaceManager_Selector
    {
        [SerializeField]
        private BoardSurfaceInputBase _inputBase;


        private BoardSurfaceStateData _boardData;

        private UpdateTileData _updateTileData;


        public event Action<Vector2Int> OnMoveTileEvent;

        public event Action<Vector2Int, EnumTileState, EnumPieceType> OnSelectTileEvent;

        public event Action<EnumPieceType> OnChangePieceTypeEvent;


        public void OnStart(BoardSurfaceStateData boardData)
        {
            _boardData = boardData;
        }

        /// <summary>
        /// 入力結果からボードの状態を変更する
        /// </summary>
        /// <returns></returns>
        public async UniTask<bool> OnChangeBoardAsync(BoardSurfacePlayerStateData playerData)
        {
            MyLogger.Log("BoardSurfaceManager_Selector OnChangeBoardAsync 引数：playerData[" + playerData + "]");

            _updateTileData = new();

            bool isSelect = false;
            while (!isSelect)
            {
                // イベント発火
                OnMoveTileEvent.Invoke(_updateTileData.Pos);

                // 入力を待つ
                EnumInputType inputType = await _inputBase.OnCheckInputAsync();

                // 入力の結果を反映する
                switch (inputType)
                {
                    case EnumInputType.Left:
                        OnCheckMoveTile(new Vector2Int(-1, 0));
                        break;
                    case EnumInputType.Right:
                        OnCheckMoveTile(new Vector2Int(1, 0));
                        break;
                    case EnumInputType.Up:
                        OnCheckMoveTile(new Vector2Int(0, -1));
                        break;
                    case EnumInputType.Down:
                        OnCheckMoveTile(new Vector2Int(0, 1));
                        break;
                    case EnumInputType.Select:
                        isSelect = OnCheckSelectTile(playerData);
                        break;
                    case EnumInputType.PieceTypeChange:
                        OnChangePieceType();
                        break;
                }
            }

            bool isWinner = _boardData.OnCheckWinner(_updateTileData.Pos, playerData.PlacingType);

            return isWinner;
        }

        /// <summary>
        /// 移動できるかを判定し、変更を反映する
        /// </summary>
        private void OnCheckMoveTile(Vector2Int moveVectol)
        {
            MyLogger.Log("BoardSurfaceManager_Selector OnCheckMoveTile 引数：moveVectol[" + moveVectol + "]");

            // 入力方向からタイルの座標情報を取得
            int nextMoveTilePointX = _updateTileData.Pos.x + moveVectol.x;
            int nextMoveTilePointY = _updateTileData.Pos.y + moveVectol.y;

            // タイルを取得（取得失敗したら範囲外）
            bool isSuccess = false;
            EnumTileState tileState;
            EnumPieceType pieceType;
            (isSuccess, tileState, pieceType) = _boardData.GetTileData(new Vector2Int(nextMoveTilePointX, nextMoveTilePointY));
            if (!isSuccess)
            {
                // 範囲外

                return;
            }

            // 変更を反映
            _updateTileData.Pos = _updateTileData.Pos + moveVectol;
        }

        /// <summary>
        /// 駒を配置できるかを判定し、変更を反映する
        /// </summary>
        private bool OnCheckSelectTile(BoardSurfacePlayerStateData playerData)
        {
            MyLogger.Log("BoardSurfaceManager_Selector OnCheckSelectTile 引数：playerData[" + playerData + "]");

            // 指定した駒を所持しているか確認
            if (!playerData.OnCheckNonePiece(_updateTileData.PieceType))
            {
                // 駒を所持してない

                return false;
            }

            // タイルを取得（取得失敗したら範囲外）
            bool isSuccess = false;
            EnumTileState tileState;
            EnumPieceType pieceType;
            (isSuccess, tileState, pieceType) = _boardData.GetTileData(_updateTileData.Pos);
            if (!isSuccess)
            {
                // 範囲外
                MyLogger.LogWarning("BoardSurfaceManager_Selector OnCheckSelectTile エラー：範囲外でした");
                return false;
            }

            // 既に駒が配置されているか確認
            if (tileState == EnumTileState.PlacedCircle || tileState == EnumTileState.PlacedCross)
            {
                // 配置済み

                return false;
            }

            // 前回配置されていた駒が相手の駒、且つ、自身が置こうと思っていた駒より配置されていたランクの方が高いか確認
            if (playerData.PlacingType == EnumTileState.PlacedCircle && tileState == EnumTileState.EmptyCross &&
                (int)_updateTileData.PieceType < (int)pieceType ||
                playerData.PlacingType == EnumTileState.PlacedCross && tileState == EnumTileState.EmptyCircle &&
                (int)_updateTileData.PieceType < (int)pieceType )
            {
                // ランクが低くて配置できない

                return false;
            }

            // 変更を反映
            _boardData.SetTileData(_updateTileData.Pos, playerData.PlacingType, _updateTileData.PieceType);
            playerData.OnSubPiece(_updateTileData.PieceType);

            // イベント発火
            OnSelectTileEvent.Invoke(_updateTileData.Pos, playerData.PlacingType, _updateTileData.PieceType);

            return true;
        }

        /// <summary>
        /// 配置する駒の種類を変更する
        /// </summary>
        private void OnChangePieceType()
        {
            MyLogger.Log("BoardSurfaceManager_Selector OnChangePieceType");
            // 次の駒を確認
            int newType = (int)_updateTileData.PieceType + 1;
            if (newType == Enum.GetNames(typeof(EnumPieceType)).Length) newType = 0;

            // 変更を反映
            _updateTileData.PieceType = (EnumPieceType)newType;

            // イベント発火
            OnChangePieceTypeEvent.Invoke(_updateTileData.PieceType);
        }
    }
}