using BoardSurface;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// プレイヤーのターンがどっちかを表示する
    /// </summary>
    [System.Serializable]
    public class UIPlayerTurnManager
    {
        private const float MOVE_TIME = 0.5f;

        [SerializeField]
        private BoardSurfaceSpriteDataAsset _spriteData;

        [Header("上のUI表示")]
        [SerializeField]
        private Image _imageHeaderP1;
        [SerializeField]
        private Image _imageHeaderP2;
        [SerializeField]
        private Transform _transformHeaderInP1;
        [SerializeField]
        private Transform _transformHeaderOutP1;
        [SerializeField]
        private Transform _transformHeaderInP2;
        [SerializeField]
        private Transform _transformHeaderOutP2;

        private EnumTileState _displayTurn;


        public void OnStart()
        {
            _imageHeaderP1.color = _spriteData.GetColorUI(EnumTileState.PlacedCircle);
            _imageHeaderP2.color = _spriteData.GetColorUI(EnumTileState.PlacedCross);

            _displayTurn = EnumTileState.PlacedCircle;
            
            OnMoveUI();
        }

        public void OnChangePlayer(EnumTileState player)
        {
            MyLogger.Log("UIPlayerTurnManager OnChangePlayer player[" + player + "]");

            // 変わっていない場合は終了
            if (_displayTurn == player)
            {
                MyLogger.Log("UIPlayerTurnManager OnChangePlayer チェック：変更がありませんでした　player[" + player + "]");
                return;
            }

            // プレイヤーを確認
            if (player != EnumTileState.PlacedCircle &&
                player != EnumTileState.PlacedCross)
            {
                MyLogger.LogError("UIPlayerTurnManager OnChangePlayer エラー：意図しない値が入りました　player[" + player + "]");
                return;
            }

            _displayTurn = player;
            OnMoveUI();
        }

        private void OnMoveUI()
        {
            switch (_displayTurn)
            {
                case EnumTileState.PlacedCircle:
                    _imageHeaderP1.transform.DOMove(_transformHeaderInP1.position, MOVE_TIME);
                    _imageHeaderP2.transform.DOMove(_transformHeaderOutP2.position, MOVE_TIME);
                    break;
                case EnumTileState.PlacedCross:
                    _imageHeaderP1.transform.DOMove(_transformHeaderOutP1.position, MOVE_TIME);
                    _imageHeaderP2.transform.DOMove(_transformHeaderInP2.position, MOVE_TIME);
                    break;
            }
        }
    }
}