using BoardSurface;
using UnityEngine;

namespace UI
{
    public class UIPieceValuePresenter : MonoBehaviour
    {
        [SerializeField]
        private UIPieceValueManager _manager;

        [SerializeField]
        private BoardSurfaceManager _boardSurfaceManager;

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
            _boardSurfaceManager.OnChangePieceValueEvent += _manager.OnChangePieceValue;
            _boardSurfaceManager.OnMissChoice_NotHaveEvent += _manager.OnShakeRabel;
        }

        private void OnDispose()
        {
            _boardSurfaceManager.OnChangePieceValueEvent -= _manager.OnChangePieceValue;
        }
    }
}