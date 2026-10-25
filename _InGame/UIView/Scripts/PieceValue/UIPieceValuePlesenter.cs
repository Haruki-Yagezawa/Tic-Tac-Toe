using BoardSurface;
using UnityEngine;

namespace UI
{
    public class UIPieceValuePlesenter : MonoBehaviour
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
        }

        private void OnDispose()
        {
            _boardSurfaceManager.OnChangePieceValueEvent -= _manager.OnChangePieceValue;
        }
    }
}