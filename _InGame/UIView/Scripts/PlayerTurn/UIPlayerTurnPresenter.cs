using BoardSurface;
using GameManager;
using R3;
using UnityEngine;

namespace UI
{
    public class UIPlayerTurnPresenter : MonoBehaviour
    {
        [SerializeField]
        private UIPlayerTurnManager _manager;

        [SerializeField]
        private InGameManager _gameManager;

        private void Awake()
        {
            OnSubscribe();
        }
        private void OnDestroy()
        {
            OnDispose();
        }

        private void Start()
        {
            _manager.OnStart();
        }

        private void OnSubscribe()
        {
            _gameManager.PlayerTurn.Subscribe(val =>
            {
                if (val == 1)
                {
                    _manager.OnChangePlayer(EnumTileState.PlacedCircle);
                }
                if (val == 2)
                {
                    _manager.OnChangePlayer(EnumTileState.PlacedCross);
                }
            }).AddTo(this);
        }

        private void OnDispose()
        {

        }
    }
}