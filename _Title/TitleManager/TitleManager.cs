using Help;
using InputSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleSystem
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField]
        private InputManager _inputManager;

        [SerializeField]
        private HelpManager _helpManager;


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
            _helpManager.OnCloseEvent += OnStartGame;
        }

        private void OnDispose()
        {
            _inputManager.OnPushAnyKeyEvent -= OnPushAnyKey;
            _helpManager.OnCloseEvent -= OnStartGame;
        }

        private void OnPushAnyKey()
        {
            _inputManager.OnPushAnyKeyEvent -= OnPushAnyKey;
            _helpManager.OnOpenHelp();
        }

        private void OnStartGame()
        {
            SceneManager.LoadScene("InGame");
        }
    }
}