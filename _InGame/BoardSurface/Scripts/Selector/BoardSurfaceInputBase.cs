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

        private ReactiveProperty<EnumInputType> _inputType;

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
            _inputType.Value = EnumInputType.Null;

            // 値が変わる（入力されて関数が呼ばれる）まで止まる
            await _inputType.Where(val => val != EnumInputType.Null).FirstAsync();

            return _inputType.Value;
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
            _inputType.Value = EnumInputType.Left;
        }
        protected void OnPush_Right()
        {
            _inputType.Value = EnumInputType.Right;
        }
        protected void OnPush_Up()
        {
            _inputType.Value = EnumInputType.Up;
        }
        protected void OnPush_Down()
        {
            _inputType.Value = EnumInputType.Down;
        }
        protected void OnPush_Select()
        {
            _inputType.Value = EnumInputType.Select;
        }
        protected void OnPush_PieceTypeChange()
        {
            _inputType.Value = EnumInputType.PieceTypeChange;
        }

        //==================================================================
        // ターンが変更され、操作するプレイヤーが変わるときに呼び出される関数
        abstract protected void OnChange_1P();
        abstract protected void OnChange_2P();

    }
}