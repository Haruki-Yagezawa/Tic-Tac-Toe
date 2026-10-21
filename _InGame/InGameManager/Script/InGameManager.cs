using BoardSurface;
using Cysharp.Threading.Tasks;
using R3;
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

        //===============================================================
        [SerializeField]
        private BoardSurfaceManager _boardSurfaceManager;

        //===============================================================
        /// <summary>
        /// 今どちらの手番かを示す
        /// 1=1P 2=2P 0=初期値
        /// </summary>
        public ReadOnlyReactiveProperty<int> PlayerTurn => _playerTurn;


        private void Start()
        {
            OnGameProgressionAsync().Forget();
        }

        private async UniTaskVoid OnGameProgressionAsync()
        {
            bool isWin = false;

            do
            {
                // 埋まり​判定


                // プレイヤー変更
                _playerTurn.Value = _playerTurn.Value != 1 ? 1 : 2;


                // テロップ​（〇〇の​番）


                // ​盤面を​選択

                
                // 勝敗判定


            } while (isWin);
        }


    }
}