using Cysharp.Threading.Tasks;
using GameManager;
using R3;
using UnityEngine;

namespace BoardSurface
{
    /// <summary>
    /// 盤面の処理を統括するクラス
    /// </summary>
    public class BoardSurfaceManager : MonoBehaviour
    {

        [SerializeField]
        private InGameManager _gameManager;
        [SerializeField]
        private BoardSurfaceManager_Selector _selector;

        private BoardSurfaceStateData _boardData = new();
        private BoardSurfacePlayerStateData[] _playerData = new BoardSurfacePlayerStateData[2];
        private BoardSurfacePlayerStateData _currentPlayerData;




        private void Awake()
        {
            _playerData[0] = new BoardSurfacePlayerStateData(EnumTileState.PlacedCircle);
            _playerData[1] = new BoardSurfacePlayerStateData(EnumTileState.PlacedCross);
            _selector = new(_boardData);

            OnSubscribe();
        }

        private void OnSubscribe()
        {
            _gameManager.RegisterBoardSurfaceSelectEvent(OnSelect);
            _gameManager.RegisterBoardSurfaceCheckBuriedEvent(OnCheckBuried);

            _gameManager.PlayerTurn.Subscribe(val => {
                if (val == 1)
                {
                    _currentPlayerData = _playerData[0];
                }
                if (val == 2)
                {
                    _currentPlayerData = _playerData[1];
                }
            }).AddTo(this);
        }

        //===============================================================================
        /// <summary>
        /// 盤面に駒を配置
        /// </summary>
        /// <returns></returns>
        private async UniTask<bool> OnSelect()
        {
            bool isWinner = false;

            isWinner = await _selector.OnChangeBoardAsync(_currentPlayerData);

            return isWinner;
        }
        //===============================================================================
        /// <summary>
        /// 盤面が埋まっているか判定
        /// </summary>
        /// <returns></returns>
        private async UniTask<bool> OnCheckBuried()
        {
            if (!_boardData.OnCheckBuried()) return false;

            // 盤面をリセット
            _boardData.OnResetBoard();
            return true;
        }


    }
}