using InputSystems;
using UnityEngine;

namespace BoardSurface
{
    public class BoardSurfaceInputLocalPVP : BoardSurfaceInputBase
    {
        [Header("BoardSurfaceInputLocalPVP関連")]
        [SerializeField]
        private InputManager _inputManager;


        protected override void OnChange_1P()
        {
            MyLogger.Log("BoardSurfaceInputLocalPVP OnChange_1P");

            _inputManager.OnPush1PLeftEvent += OnPush_Left;
            _inputManager.OnPush1PRightEvent += OnPush_Right;
            _inputManager.OnPush1PUpEvent += OnPush_Up;
            _inputManager.OnPush1PDownEvent += OnPush_Down;
            _inputManager.OnPush1PSelectEvent += OnPush_Select;
            _inputManager.OnPush1PPieceTypeChangeEvent += OnPush_PieceTypeChange;

            _inputManager.OnPush2PLeftEvent -= OnPush_Left;
            _inputManager.OnPush2PRightEvent -= OnPush_Right;
            _inputManager.OnPush2PUpEvent -= OnPush_Up;
            _inputManager.OnPush2PDownEvent -= OnPush_Down;
            _inputManager.OnPush2PSelectEvent -= OnPush_Select;
            _inputManager.OnPush2PPieceTypeChangeEvent -= OnPush_PieceTypeChange;
        }
        protected override void OnChange_2P()
        {
            MyLogger.Log("BoardSurfaceInputLocalPVP OnChange_2P");

            _inputManager.OnPush1PLeftEvent -= OnPush_Left;
            _inputManager.OnPush1PRightEvent -= OnPush_Right;
            _inputManager.OnPush1PUpEvent -= OnPush_Up;
            _inputManager.OnPush1PDownEvent -= OnPush_Down;
            _inputManager.OnPush1PSelectEvent -= OnPush_Select;
            _inputManager.OnPush1PPieceTypeChangeEvent -= OnPush_PieceTypeChange;

            _inputManager.OnPush2PLeftEvent += OnPush_Left;
            _inputManager.OnPush2PRightEvent += OnPush_Right;
            _inputManager.OnPush2PUpEvent += OnPush_Up;
            _inputManager.OnPush2PDownEvent += OnPush_Down;
            _inputManager.OnPush2PSelectEvent += OnPush_Select;
            _inputManager.OnPush2PPieceTypeChangeEvent += OnPush_PieceTypeChange;
        }
    }
}