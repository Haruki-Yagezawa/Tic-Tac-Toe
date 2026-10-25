using Cysharp.Threading.Tasks;
using GameManager;
using R3;
using System;
using Unity.VisualScripting;
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
        [SerializeField]
        private BoardSurfaceManager_View _view;

        private BoardSurfaceStateData _boardData = new();
        private BoardSurfacePlayerStateData[] _playerData = new BoardSurfacePlayerStateData[2];
        private BoardSurfacePlayerStateData _currentPlayerData;

        public event Action<EnumTileState, int[]> OnChangePieceValueEvent;
        public event Action<EnumTileState, EnumPieceType> OnMissChoice_NotHaveEvent;


        private void Awake()
        {
            _playerData[0] = new BoardSurfacePlayerStateData(EnumTileState.PlacedCircle);
            _playerData[1] = new BoardSurfacePlayerStateData(EnumTileState.PlacedCross);
            _selector.OnStart(_boardData);

            OnSubscribe();
        }

        private void Start()
        {

            // 今の駒の数をイベント経由で渡す
            foreach (var pd in _playerData)
            {
                int[] pieveValues = new int[] { pd.GetPieceSValue,
                                            pd.GetPieceMValue,
                                            pd.GetPieceLValue};
                OnChangePieceValueEvent.Invoke(pd.PlacingType, pieveValues);
            }
        }

        private void OnDestroy()
        {
            OnDispose();
        }

        private void OnSubscribe()
        {
            _gameManager.RegisterBoardSurfaceSelectEvent(OnSelectAsync);
            _gameManager.RegisterBoardSurfaceCheckBuriedEvent(OnCheckBuried);

            _gameManager.PlayerTurn.Subscribe(val =>
            {
                if (val == 1)
                {
                    _currentPlayerData = _playerData[0];
                    _view.OnChangePlayer(_currentPlayerData.PlacingType);
                }
                if (val == 2)
                {
                    _currentPlayerData = _playerData[1];
                    _view.OnChangePlayer(_currentPlayerData.PlacingType);
                }
            }).AddTo(this);

            _selector.OnMoveTileEvent += _view.OnMoveTile;
            _selector.OnSelectTileEvent += _view.OnSelectTile;
            _selector.OnChangePieceTypeEvent += _view.OnChangePieceType;
            _selector.OnMissChoice_NotHaveEvent += OnMissChoice_NotHave;
            _selector.OnMissChoice_AllocatedEvent += _view.OnMissChoice_Allocated;
            _selector.OnMissChoice_LowRankEvent += _view.OnMissChoice_LowRank;

        }

        private void OnDispose()
        {
            _selector.OnMoveTileEvent -= _view.OnMoveTile;
            _selector.OnSelectTileEvent -= _view.OnSelectTile;
            _selector.OnChangePieceTypeEvent -= _view.OnChangePieceType;
        }

        //===============================================================================
        /// <summary>
        /// 盤面に駒を配置
        /// </summary>
        /// <returns></returns>
        private async UniTask<bool> OnSelectAsync()
        {
            MyLogger.Log("BoardSurfaceManager OnSelectAsync");
            bool isWinner = false;

            isWinner = await _selector.OnChangeBoardAsync(_currentPlayerData);

            // 今の駒の数をイベント経由で渡す
            int[] pieveValues = new int[] { _currentPlayerData.GetPieceSValue,
                                            _currentPlayerData.GetPieceMValue,
                                            _currentPlayerData.GetPieceLValue};
            OnChangePieceValueEvent.Invoke(_currentPlayerData.PlacingType, pieveValues);

            return isWinner;
        }
        //===============================================================================
        /// <summary>
        /// 盤面が埋まっているか判定
        /// </summary>
        /// <returns></returns>
        private bool OnCheckBuried()
        {
            MyLogger.Log("BoardSurfaceManager OnCheckBuried");

            if (!_boardData.OnCheckBuried()) return false;

            // 盤面をリセット
            _boardData.OnResetBoard();
            _view.OnResetBoard();

            // 駒を加算
            _playerData[0].OnProvidePiece();
            _playerData[1].OnProvidePiece();

            // 今の駒の数を表示
            foreach (var pd in _playerData)
            {
                int[] pieveValues = new int[] { pd.GetPieceSValue,
                                            pd.GetPieceMValue,
                                            pd.GetPieceLValue};
                OnChangePieceValueEvent.Invoke(pd.PlacingType, pieveValues);
            }

            return true;
        }



        private void OnMissChoice_NotHave(EnumTileState player, EnumPieceType pieceType)
        {
            MyLogger.Log("BoardSurfaceManager OnMissChoice_NotHave");
            OnMissChoice_NotHaveEvent.Invoke(player, pieceType);
        }
    }
}