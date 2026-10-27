using DG.Tweening;
using UnityEngine;

namespace BoardSurface
{
    /// <summary>
    /// 選択しているタイルを指すテクスチャの挙動管理
    /// </summary>
    public class BoardSurfaceViewSelectTile : MonoBehaviour
    {
        private const float MOVE_TIME = 0.3f;
        private const float MOVE_TIME_CHAGE_PIECE_TYPE = 0.2f;
        private const float SHAKE_POWER_CURSOL = 0.25f;
        private const float MOVE_TIME_CURSOL = 0.5f;


        [SerializeField]
        private BoardSurfaceSpriteDataAsset _sprite;

        [SerializeField]
        private SpriteRenderer _spriteRendererSelect;
        [SerializeField]
        private SpriteRenderer _spriteRendererPieceType;

        [SerializeField]
        private Color _color1P;
        [SerializeField]
        private Color _color2P;


        // 駒の変更用シーケンス
        private Sequence _sequenceChangePieceType;

        private Vector3 _startScalePiece;
        private Vector3 _startRoatatePiece;

        private Vector3 _startPositionSelect;


        private void Awake()
        {
            _startScalePiece = _spriteRendererPieceType.transform.localScale;
            _startRoatatePiece = _spriteRendererPieceType.transform.localEulerAngles;

            _startPositionSelect = _spriteRendererSelect.transform.localPosition;
        }

        private void OnDestroy()
        {
            _sequenceChangePieceType?.Kill();
        }


        /// <summary>
        /// 選択されているタイルへ移動
        /// </summary>
        /// <param name="pos"></param>
        public void OnMoveTile(Vector3 nextPos)
        {
            MyLogger.Log("BoardSurfaceViewSelectTile OnMoveTile 引数：nextPos[" + nextPos + "]");

            transform.DOMove(nextPos, MOVE_TIME);
        }

        /// <summary>
        /// 選択されている駒の種類を描画
        /// </summary>
        /// <param name="pieceType"></param>
        public void OnChangePieceType(EnumPieceType pieceType)
        {
            MyLogger.Log("BoardSurfaceViewSelectTile OnChangePieceType 引数：pieceType[" + pieceType + "]");

            // 動いている途中ならキル
            if (_sequenceChangePieceType != null && _sequenceChangePieceType.IsActive())
            {
                _sequenceChangePieceType.Kill();
            }

            // 画像変更
            _spriteRendererPieceType.sprite = _sprite.GetPiece(pieceType);

            // シーケンスを作って再生
            _sequenceChangePieceType = DOTween.Sequence()
                .Append(_spriteRendererPieceType.transform.DOScale(_startScalePiece, MOVE_TIME_CHAGE_PIECE_TYPE).From(Vector3.zero))
                .Join(_spriteRendererPieceType.transform.DORotate(_startRoatatePiece, MOVE_TIME_CHAGE_PIECE_TYPE).From(new Vector3(0, 0, 90)))
                .SetEase(Ease.Linear);
        }

        /// <summary>
        /// 操作プレイヤー色を変更
        /// </summary>
        /// <param name="player"></param>
        public void OnChangePlayer(EnumTileState player)
        {
            MyLogger.Log("BoardSurfaceViewSelectTile OnChangePlayer 引数：player[" + player + "]");

            _spriteRendererPieceType.color = _sprite.GetColorPiece(player);

            if (player == EnumTileState.PlacedCircle)
            {
                _spriteRendererSelect.color = _color1P;
            }
            else if (player == EnumTileState.PlacedCross)
            {
                _spriteRendererSelect.color = _color2P;
            }
        }

        /// <summary>
        /// カーソルを揺らす
        /// </summary>
        public void OnShakeCursol()
        {
            MyLogger.Log("BoardSurfaceViewSelectTile OnShakeCursol");
            _spriteRendererSelect.DOKill();
            _spriteRendererSelect.transform.localPosition = _startPositionSelect;
            _spriteRendererSelect.transform.DOShakePosition(SHAKE_POWER_CURSOL, MOVE_TIME_CURSOL);
        }
    }
}