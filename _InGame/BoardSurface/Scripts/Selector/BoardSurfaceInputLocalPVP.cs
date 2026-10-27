using Help;
using InputSystems;
using UnityEngine;

namespace BoardSurface
{
    public class BoardSurfaceInputLocalPVP : BoardSurfaceInputBase
    {
        [Header("BoardSurfaceInputLocalPVP関連")]
        [SerializeField]
        private InputManager _inputManager;

        [SerializeField]
        private HelpManager _helpManager;


        private int _currentPlayer = 0;


        protected override void OnSubscribe()
        {
            base.OnSubscribe();

            OnSubscribeHelp();
        }
        protected override void OnDispose()
        {
            base.OnDispose();

            OnDisposeInput();

            OnDisposeHelp();
        }

        protected override void OnChange_1P()
        {
            MyLogger.Log("BoardSurfaceInputLocalPVP OnChange_1P");

            _currentPlayer = 1;
            OnChangeControllerP1();

        }
        protected override void OnChange_2P()
        {
            MyLogger.Log("BoardSurfaceInputLocalPVP OnChange_2P");

            _currentPlayer = 2;
            OnChangeControllerP2();

        }


        /// <summary>
        /// コントロールを1Pに変更
        /// </summary>
        private void OnChangeControllerP1()
        {
            OnSubscribeP1();
            OnDisposeP2();
        }

        /// <summary>
        /// コントロールを2Pに変更
        /// </summary>
        private void OnChangeControllerP2()
        {
            OnSubscribeP2();
            OnDisposeP1();
        }


        /// <summary>
        /// 入力を受け付けないようにする
        /// </summary>
        private void OnDisposeInput()
        {
            MyLogger.Log("BoardSurfaceInputLocalPVP OnDisposeInput");
            OnDisposeP1();
            OnDisposeP2();
        }


        //==========================================================================
        private void OnPush_Escape_OpenHelp()
        {
            // ボタン入力イベント解除
            OnDisposeInput();

            _helpManager.OnOpenHelp();

            _inputManager.OnPushEscapeEvent -= OnPush_Escape_OpenHelp;
        }

        private void OnPush_Escape_CloseHelp()
        {
            if (_currentPlayer == 1)
                OnChangeControllerP1();
            else if (_currentPlayer == 2)
                OnChangeControllerP2();

            _inputManager.OnPushEscapeEvent += OnPush_Escape_OpenHelp;
        }

        //==========================================================================
        private void OnSubscribeP1()
        {
            _inputManager.OnPush1PLeftEvent += OnPush_Left;
            _inputManager.OnPush1PRightEvent += OnPush_Right;
            _inputManager.OnPush1PUpEvent += OnPush_Up;
            _inputManager.OnPush1PDownEvent += OnPush_Down;
            _inputManager.OnPush1PSelectEvent += OnPush_Select;
            _inputManager.OnPush1PPieceTypeChangeEvent += OnPush_PieceTypeChange;
        }
        private void OnDisposeP1()
        {
            _inputManager.OnPush1PLeftEvent -= OnPush_Left;
            _inputManager.OnPush1PRightEvent -= OnPush_Right;
            _inputManager.OnPush1PUpEvent -= OnPush_Up;
            _inputManager.OnPush1PDownEvent -= OnPush_Down;
            _inputManager.OnPush1PSelectEvent -= OnPush_Select;
            _inputManager.OnPush1PPieceTypeChangeEvent -= OnPush_PieceTypeChange;
        }
        private void OnSubscribeP2()
        {
            _inputManager.OnPush2PLeftEvent += OnPush_Left;
            _inputManager.OnPush2PRightEvent += OnPush_Right;
            _inputManager.OnPush2PUpEvent += OnPush_Up;
            _inputManager.OnPush2PDownEvent += OnPush_Down;
            _inputManager.OnPush2PSelectEvent += OnPush_Select;
            _inputManager.OnPush2PPieceTypeChangeEvent += OnPush_PieceTypeChange;
        }
        private void OnDisposeP2()
        {
            _inputManager.OnPush2PLeftEvent -= OnPush_Left;
            _inputManager.OnPush2PRightEvent -= OnPush_Right;
            _inputManager.OnPush2PUpEvent -= OnPush_Up;
            _inputManager.OnPush2PDownEvent -= OnPush_Down;
            _inputManager.OnPush2PSelectEvent -= OnPush_Select;
            _inputManager.OnPush2PPieceTypeChangeEvent -= OnPush_PieceTypeChange;
        }
        private void OnSubscribeHelp()
        {
            _inputManager.OnPushEscapeEvent += OnPush_Escape_OpenHelp;
            _helpManager.OnCloseEvent += OnPush_Escape_CloseHelp;
        }
        private void OnDisposeHelp()
        {
            _inputManager.OnPushEscapeEvent -= OnPush_Escape_OpenHelp;
            _helpManager.OnCloseEvent -= OnPush_Escape_CloseHelp;
        }
    }
}