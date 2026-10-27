using DG.Tweening;
using InputSystems;
using System;
using UnityEngine;

namespace Help
{
    /// <summary>
    /// ヘルプ画面の処理を統括
    /// </summary>
    public class HelpManager : MonoBehaviour
    {
        [SerializeField]
        private InputManager _input;

        [SerializeField]
        private HelpManager_Display _display;

        [SerializeField]
        private HelpManager_PageSelector _pageSelector;


        public event Action OnCloseEvent;


        private void Start()
        {
            _pageSelector.OnStart();
            _display.OnStart(gameObject);
        }

        private void OnDestroy()
        {
            OnDispose();
        }
        private void OnDispose()
        {
            _input.OnPush1PLeftEvent -= _pageSelector.OnPrevPage;
            _input.OnPush2PLeftEvent -= _pageSelector.OnPrevPage;
            _input.OnPush1PRightEvent -= _pageSelector.OnNextPage;
            _input.OnPush2PRightEvent -= _pageSelector.OnNextPage;
            _input.OnPush1PSelectEvent -= OnCloseHelp;
            _input.OnPush2PSelectEvent -= OnCloseHelp;
            _input.OnPushEscapeEvent -= OnCloseHelp;
        }


        /// <summary>
        /// ヘルプ画面を表示する
        /// </summary>
        public void OnOpenHelp()
        {
            // 入力イベント登録
            _input.OnPush1PLeftEvent += _pageSelector.OnPrevPage;
            _input.OnPush2PLeftEvent += _pageSelector.OnPrevPage;
            _input.OnPush1PRightEvent += _pageSelector.OnNextPage;
            _input.OnPush2PRightEvent += _pageSelector.OnNextPage;
            _input.OnPush1PSelectEvent += OnCloseHelp;
            _input.OnPush2PSelectEvent += OnCloseHelp;
            _input.OnPushEscapeEvent += OnCloseHelp;

            // 画面表示
            _display.OnDisplay();
        }

        /// <summary>
        /// ヘルプ画面を非表示にする
        /// </summary>
        private void OnCloseHelp()
        {
            // 入力イベントを解除
            _input.OnPush1PLeftEvent -= _pageSelector.OnPrevPage;
            _input.OnPush2PLeftEvent -= _pageSelector.OnPrevPage;
            _input.OnPush1PRightEvent -= _pageSelector.OnNextPage;
            _input.OnPush2PRightEvent -= _pageSelector.OnNextPage;
            _input.OnPush1PSelectEvent -= OnCloseHelp;
            _input.OnPush2PSelectEvent -= OnCloseHelp;
            _input.OnPushEscapeEvent -= OnCloseHelp;

            // 画面非表示
            _display.OnHide();

            // イベント発火
            DOVirtual.DelayedCall(HelpManager_Display.MOVE_TIME, () => {
                OnCloseEvent.Invoke();
            }, true).SetLink(gameObject);

        }
    }
}