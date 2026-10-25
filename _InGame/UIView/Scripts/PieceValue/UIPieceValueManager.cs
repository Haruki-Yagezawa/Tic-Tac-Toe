using BoardSurface;
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
        [SerializeField]
        private BoardSurfaceSpriteDataAsset _spriteData;

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
    }
}