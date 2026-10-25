using InputSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleSystem
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField]
        private InputManager _inputManager;


        private void Awake()
        {
            OnSubscribe();
        }

        private void OnDestroy()
        {
            OnDispose();
        }

        private void OnSubscribe()
        {
            _inputManager.OnPushAnyKeyEvent += OnPushAnyKey;
        }

        private void OnDispose()
        {
            _inputManager.OnPushAnyKeyEvent -= OnPushAnyKey;
        }

        private void OnPushAnyKey()
        {
            SceneManager.LoadScene("InGame");
        }
    }
}