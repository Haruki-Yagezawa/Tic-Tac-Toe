using DG.Tweening;
using UnityEngine;

namespace BoardSurface
{
    /// <summary>
    /// タイルのテクスチャの挙動管理
    /// </summary>
    public class BoardSurfaceViewTile : MonoBehaviour
    {
        private const float SHAKE_POWER_PIECE = 0.2f;
        private const float MOVE_TIME_PIECE = 0.5f;


        [SerializeField]
        private BoardSurfaceSpriteDataAsset _sprite;

        [Header("タイル関連")]
        [SerializeField]
        private SpriteRenderer _spriteRendererTile;


        [Header("駒の種類関連")]
        [SerializeField]
        private SpriteRenderer _spriteRendererPiece;


        private Vector3 _startPositionPiece;


        private void Start()
        {
            _startPositionPiece = _spriteRendererPiece.transform.localPosition;
        }

        /// <summary>
        /// タイルと駒のテクスチャを変更する
        /// </summary>
        /// <param name="tileState"></param>
        /// <param name="pieceType"></param>
        public void OnChangeTileAndPiece(EnumTileState tileState, EnumPieceType pieceType)
        {
            MyLogger.Log("BoardSurfaceViewTile OnChangeTileAndPiece 引数：tileState[" + tileState + "]　pieceType[" + pieceType + "]");

            // 盤面の状態テクスチャと駒の色を変更
            switch (tileState)
            {
                case EnumTileState.PlacedCircle:
                case EnumTileState.PlacedCross:
                    _spriteRendererTile.sprite = _sprite.TilePlaced;
                    _spriteRendererTile.color = _sprite.GetColorTile(tileState);
                    _spriteRendererPiece.color = _sprite.GetColorPiece(tileState);
                    break;

                case EnumTileState.EmptyCircle:
                case EnumTileState.EmptyCross:
                    _spriteRendererTile.sprite = _sprite.TileEmpty;
                    _spriteRendererTile.color = _sprite.GetColorTile(tileState);
                    _spriteRendererPiece.color = _sprite.GetColorPiece(tileState);
                    break;

                case EnumTileState.Empty:
                    // 空だったら変更した後終了
                    _spriteRendererTile.sprite = _sprite.TileEmpty;
                    _spriteRendererTile.color = Color.white;
                    _spriteRendererPiece.color = Color.clear;
                    return;
            }

            // 駒のテクスチャ変更
            _spriteRendererPiece.sprite = _sprite.GetPiece(pieceType);
        }

        /// <summary>
        /// 空の状態にする
        /// </summary>
        public void OnReset()
        {
            MyLogger.Log("BoardSurfaceViewTile OnReset");

            _spriteRendererTile.sprite = _sprite.TileEmpty;
            _spriteRendererTile.color = Color.white;
        }

        /// <summary>
        /// 駒の部分を揺らす
        /// </summary>
        public void OnShakePiece()
        {
            MyLogger.Log("BoardSurfaceViewTile OnShakePiece");

            _spriteRendererPiece.DOKill();
            _spriteRendererPiece.transform.localPosition = _startPositionPiece;
            _spriteRendererPiece.transform.DOShakePosition(MOVE_TIME_PIECE, SHAKE_POWER_PIECE);
        }
    }
}