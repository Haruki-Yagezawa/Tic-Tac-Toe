using Cysharp.Threading.Tasks;
using GameManager;
using R3;
using UnityEngine;

namespace BoardSurface
{
    /// <summary>
    /// 入力方法に沿って入力内容を確定させるクラス
    /// </summary>
    abstract public class BoardSurfaceInputBase : MonoBehaviour
    {
        [Header("BoardSurfaceInputBase関連")]
        [SerializeField]
        private InGameManager _gameManager;

        private EnumInputType _inputType;

        private void Awake()
        {
            OnSubscribe();
        }
        private void OnDestroy()
        {
            OnDispose();
        }

        /// <summary>
        /// 入力を受け付ける
        /// </summary>
        /// <returns></returns>
        public async UniTask<EnumInputType> OnCheckInputAsync()
        {
            MyLogger.Log("BoardSurfaceInputBase OnCheckInputAsync");

            _inputType = EnumInputType.Null;

            // 値が変わる（入力されて関数が呼ばれる）まで止まる
            await UniTask.WaitUntil(() => _inputType != EnumInputType.Null);

            return _inputType;
        }


        //==================================================================
        // イベント登録用関数
        virtual protected void OnSubscribe()
        {
            _gameManager.PlayerTurn.Subscribe(val => {
                if (val == 1)
                {
                    OnChange_1P();
                }
                if (val == 2)
                {
                    OnChange_2P();
                }
            }).AddTo(this);
        }
        virtual protected void OnDispose()
        {

        }

        //==================================================================
        // 押した内容（継承先で呼び出す）
        protected void OnPush_Left()
        {
            MyLogger.Log("BoardSurfaceInputBase OnPush_Left");

            _inputType = EnumInputType.Left;
        }
        protected void OnPush_Right()
        {
            MyLogger.Log("BoardSurfaceInputBase OnPush_Right");

            _inputType = EnumInputType.Right;
        }
        protected void OnPush_Up()
        {
            MyLogger.Log("BoardSurfaceInputBase OnPush_Up");

            _inputType = EnumInputType.Up;
        }
        protected void OnPush_Down()
        {
            MyLogger.Log("BoardSurfaceInputBase OnPush_Down");

            _inputType = EnumInputType.Down;
        }
        protected void OnPush_Select()
        {
            MyLogger.Log("BoardSurfaceInputBase OnPush_Select");

            _inputType = EnumInputType.Select;
        }
        protected void OnPush_PieceTypeChange()
        {
            MyLogger.Log("BoardSurfaceInputBase OnPush_PieceTypeChange");

            _inputType = EnumInputType.PieceTypeChange;
        }

        //==================================================================
        // ターンが変更され、操作するプレイヤーが変わるときに呼び出される関数
        abstract protected void OnChange_1P();
        abstract protected void OnChange_2P();

    }
}