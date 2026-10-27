using BoardSurface;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 駒の数をUIで表示する
    /// </summary>
    [System.Serializable]
    public class UIPieceValueManager
    {
        private const float SHAKE_POWER_LABEL = 13f;
        private const float MOVE_TIME_RABEL = 0.5f;

        [SerializeField]
        private BoardSurfaceSpriteDataAsset _spriteData;

        [Header("ラベル")]
        [SerializeField]
        private RectTransform[] _rectTransformP1;
        [SerializeField]
        private RectTransform[] _rectTransformP2;

        [Header("駒アイコン")]
        [SerializeField]
        private Image[] _imagePieceIconP1;
        [SerializeField]
        private Image[] _imagePieceIconP2;

        [Header("駒の数")]
        [SerializeField]
        private TextMeshProUGUI[] _textPieceValueP1;
        [SerializeField]
        private TextMeshProUGUI[] _textPieceValueP2;

        [Header("背景")]
        [SerializeField]
        private Image _imageBackImageP1;
        [SerializeField]
        private Image _imageBackImageP2;


        private Vector3[] _startPositionP1 ;
        private Vector3[] _startPositionP2;


        public void OnStart()
        {
            foreach (var icon in _imagePieceIconP1)
            {
                icon.color = _spriteData.GetColorPiece(EnumTileState.PlacedCircle);
            }
            foreach (var icon in _imagePieceIconP2)
            {
                icon.color = _spriteData.GetColorPiece(EnumTileState.PlacedCross);
            }

            _imageBackImageP1.color = _spriteData.GetColorUI(EnumTileState.PlacedCircle);
            _imageBackImageP2.color = _spriteData.GetColorUI(EnumTileState.PlacedCross);

            _startPositionP1 = new Vector3[_rectTransformP1.Length];
            _startPositionP2 = new Vector3[_rectTransformP2.Length];
            for (int i = 0; i < _rectTransformP1.Length; ++i)
            {
                _startPositionP1[i] = _rectTransformP1[i].localPosition;
            }
            for (int i = 0; i < _rectTransformP2.Length; ++i)
            {
                _startPositionP2[i] = _rectTransformP2[i].localPosition;
            }
        }

        /// <summary>
        /// 指定されたプレイヤーの駒数を変更する
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pieceValue"></param>
        public void OnChangePieceValue(EnumTileState player, int[] pieceValue)
        {
            MyLogger.Log("UIPieceValueManager OnChangePieceValue player[" + player + "]");

            TextMeshProUGUI[] targetText = null;

            // 対象のテキストを確認
            if (player == EnumTileState.PlacedCircle) targetText = _textPieceValueP1;
            else if (player == EnumTileState.PlacedCross) targetText = _textPieceValueP2;
            else
            {
                MyLogger.LogError("UIPieceValueManager OnChangePieceValue エラー：意図しない値が入りました　player[" + player + "]");
                return;
            }

            // 反映
            for (int i = 0; i < targetText.Length; ++i)
            {
                targetText[i].text = " × " + pieceValue[i];
            }
        }

        /// <summary>
        /// ラベルを揺らす
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pieceType"></param>
        public void OnShakeRabel(EnumTileState player, EnumPieceType pieceType)
        {
            MyLogger.Log("UIPieceValueManager OnShakeRabel player[" + player + "] pieceType[" + pieceType + "]");

            RectTransform[] targetRectTransform = null;
            Vector3[] targetStartPosition = null;

            // 対象のテキストを確認
            if (player == EnumTileState.PlacedCircle)
            {
                targetRectTransform = _rectTransformP1;
                targetStartPosition = _startPositionP1;
            }
            else if (player == EnumTileState.PlacedCross)
            {
                targetRectTransform = _rectTransformP2;
                targetStartPosition = _startPositionP2;
            }
            else
            {
                MyLogger.LogError("UIPieceValueManager OnShakeRabel エラー：意図しない値が入りました　player[" + player + "]");
                return;
            }

            // 反映
            targetRectTransform[(int)pieceType].DOKill();
            targetRectTransform[(int)pieceType].localPosition = targetStartPosition[(int)pieceType];
            targetRectTransform[(int)pieceType].DOShakePosition(MOVE_TIME_RABEL, SHAKE_POWER_LABEL);

        }
    }
}