using BoardSurface.CurrentTileData;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

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



        public BoardSurfaceManager_Selector(BoardSurfaceStateData boardData)
        {
            _boardData = boardData;
        }

        /// <summary>
        /// 入力結果からボードの状態を変更する
        /// </summary>
        /// <returns></returns>
        public async UniTask<bool> OnChangeBoardAsync(BoardSurfacePlayerStateData playerData)
        {
            _updateTileData = new();

            bool isSelect = false;
            while (!isSelect)
            {
                // 入力を待つ
                EnumInputType inputType = await _inputBase.OnCheckInputAsync();

                // 入力の結果を反映する
                switch (inputType)
                {
                    case EnumInputType.Left:
                        OnCheckMoveTile(-1, 0);
                        break;
                    case EnumInputType.Right:
                        OnCheckMoveTile(1, 0);
                        break;
                    case EnumInputType.Up:
                        OnCheckMoveTile(0, 1);
                        break;
                    case EnumInputType.Down:
                        OnCheckMoveTile(0, -1);
                        break;
                    case EnumInputType.Select:
                        OnCheckSelectTile(playerData);
                        isSelect = true;
                        break;
                    case EnumInputType.PieceTypeChange:
                        OnChangePieceType();
                        break;
                }
            }

            bool isWinner = _boardData.OnCheckWinner(_updateTileData.pos, playerData.PlacingType);

            return isWinner;
        }

        /// <summary>
        /// 移動できるかを判定し、変更を反映する
        /// </summary>
        private void OnCheckMoveTile(int x, int y)
        {
            // 入力方向からタイルの座標情報を取得
            int nextMoveTilePointX = _updateTileData.pos.x + x;
            int nextMoveTilePointY = _updateTileData.pos.y + y;

            // タイルを取得（取得失敗したら範囲外）
            bool isSuccess = false;
            EnumTileState tileState;
            EnumPieceType pieceType;
            (isSuccess, tileState, pieceType) = _boardData.GetTileData(nextMoveTilePointX, nextMoveTilePointY);
            if (!isSuccess)
            {
                // 範囲外
                Debug.LogError("範囲外でした");
                return;
            }

            // 変更を反映
            _updateTileData.pos.x = x;
            _updateTileData.pos.y = y;
        }

        /// <summary>
        /// 駒を配置できるかを判定し、変更を反映する
        /// </summary>
        private void OnCheckSelectTile(BoardSurfacePlayerStateData playerData)
        {
            // 指定した駒を所持しているか確認
            if(!playerData.OnCheckNonePiece(_updateTileData.PieceType))
            {
                // 駒を所持してない

                return;
            }

            // タイルを取得（取得失敗したら範囲外）
            bool isSuccess = false;
            EnumTileState tileState;
            EnumPieceType pieceType;
            (isSuccess, tileState, pieceType) = _boardData.GetTileData(_updateTileData.pos.x, _updateTileData.pos.y);
            if (!isSuccess)
            {
                // 範囲外
                Debug.LogError("範囲外でした");
                return;
            }

            // 既に駒が配置されているか確認
            if (tileState == EnumTileState.PlacedCircle || tileState == EnumTileState.PlacedCross)
            {
                // 配置済み

                return;
            }

            // 前回配置されていた駒が相手の駒、且つ、自身が置こうと思っていた駒より配置されていたランクの方が高いか確認
            if (playerData.PlacingType == EnumTileState.PlacedCircle && tileState == EnumTileState.EmptyCross &&
                (int)_updateTileData.PieceType < (int)pieceType ||
                playerData.PlacingType == EnumTileState.PlacedCross && tileState == EnumTileState.EmptyCircle &&
                (int)_updateTileData.PieceType < (int)pieceType )
            {
                // ランクが低くて配置できない

                return;
            }

            // 変更を反映
            _boardData.SetTileData(_updateTileData.pos.x, _updateTileData.pos.y, playerData.PlacingType, _updateTileData.PieceType);
        }

        /// <summary>
        /// 配置する駒の種類を変更する
        /// </summary>
        private void OnChangePieceType()
        {
            int newType = (int)_updateTileData.PieceType + 1;

            if (newType == Enum.GetNames(typeof(EnumPieceType)).Length) newType = 0;

            _updateTileData.PieceType = (EnumPieceType)newType;
        }
    }
}