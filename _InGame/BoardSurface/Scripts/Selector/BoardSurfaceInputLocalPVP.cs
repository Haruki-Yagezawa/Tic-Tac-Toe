using InputSystems;
using UnityEngine;

namespace BoardSurface
{
    public class BoardSurfaceInputLocalPVP : BoardSurfaceInputBase
    {
        [Header("BoardSurfaceInputLocalPVP関連")]
        [SerializeField]
        private InputManager _inputManager;


        protected override void OnChange_1P() { }
        protected override void OnChange_2P() { }


        protected override void OnSubscribe()
        {
            _inputManager.OnPushLeftEvent += OnPush_Left;
            _inputManager.OnPushRightEvent += OnPush_Right;
            _inputManager.OnPushUpEvent += OnPush_Up;
            _inputManager.OnPushDownEvent += OnPush_Down;
            _inputManager.OnPushSelectEvent += OnPush_Select;
            _inputManager.OnPushPieceTypeChangeEvent += OnPush_PieceTypeChange;

        }

        protected override void OnDispose()
        {
            _inputManager.OnPushLeftEvent -= OnPush_Left;
            _inputManager.OnPushRightEvent -= OnPush_Right;
            _inputManager.OnPushUpEvent -= OnPush_Up;
            _inputManager.OnPushDownEvent -= OnPush_Down;
            _inputManager.OnPushSelectEvent -= OnPush_Select;
            _inputManager.OnPushPieceTypeChangeEvent -= OnPush_PieceTypeChange;

        }
    }
}