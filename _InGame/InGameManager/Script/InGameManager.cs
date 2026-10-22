using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;

namespace GameManager
{
    public class InGameManager : MonoBehaviour
    {

        //===============================================================
        /// <summary>
        /// 今どちらの手番かを示す
        /// 1=1P 2=2P 0=初期値
        /// </summary>
        private readonly ReactiveProperty<int> _playerTurn = new(0);

        private Func<UniTask<bool>> _onBoardSurfaceSelectEvent;
        private Func<UniTask<bool>> _onBoardSurfaceCheckBuriedEvent;


        //===============================================================



        //===============================================================
        /// <summary>
        /// 今どちらの手番かを示す
        /// 1=1P 2=2P 0=初期値
        /// </summary>
        public ReadOnlyReactiveProperty<int> PlayerTurn => _playerTurn;


        public void RegisterBoardSurfaceSelectEvent(Func<UniTask<bool>> handler)
        {
            _onBoardSurfaceSelectEvent = handler;
        }
        public void RegisterBoardSurfaceCheckBuriedEvent(Func<UniTask<bool>> handler)
        {
            _onBoardSurfaceCheckBuriedEvent = handler;
        }


        private void Start()
        {
            OnGameProgressionAsync().Forget();
        }

        /// <summary>
        /// ゲームの進行を行う関数
        /// </summary>
        /// <returns></returns>
        private async UniTaskVoid OnGameProgressionAsync()
        {
            bool isWin = false;

            do
            {
                // 盤面が埋まっているか判定
                if (await _onBoardSurfaceCheckBuriedEvent.Invoke())
                {
                    // プレイヤーの操作を変更？
                }

                // 操作プレイヤー交代
                _playerTurn.Value = _playerTurn.Value != 1 ? 1 : 2;


                // テロップ​（〇〇の​番）


                // ​盤面に駒を配置
                if (await _onBoardSurfaceSelectEvent.Invoke())
                {
                    // 勝者が決定
                    isWin = true;
                }


            } while (isWin);


        }


    }
}