using BoardSurface;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameManager;
using InputSystems;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Result
{
    public class ResultUIManager : MonoBehaviour
    {
        private const string TEXT_WINNER_P1 = "1P\nWinner";
        private const string TEXT_WINNER_P2 = "2P\nWinner";

        private const float MOVE_TIME_FLASH = 0.2f;
        private const float MOVE_TIME_FLASH_AFTER = 0.4f;
        private const float MOVE_TIME_DISPLAY = 0.5f;


        [SerializeField]
        private InGameManager _gameManager;

        [SerializeField]
        private InputManager _inputManager;
        [SerializeField]
        private BoardSurfaceSpriteDataAsset _spriteDataAsset;

        [Header("UIの要素関連")]
        [SerializeField]
        private RectTransform _traParent;
        [SerializeField]
        private RectTransform _traButton;
        [SerializeField]
        private TMP_Text _textPlayer;

        [Header("演出用UIの要素関連")]
        [SerializeField]
        private Image _imageFlash;

        [Header("移動ポイント")]
        [SerializeField]
        private RectTransform _movePosIn;


        private void Awake()
        {
            _gameManager.OnResultEvent += OnResult;
        }
        private void Start()
        {
            _traButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _gameManager.OnResultEvent -= OnResult;
        }


        private void OnResult(int player)
        {
            MyLogger.Log("ResultUIManager OnResult 引数：player[" + player + "]");

            // 判定を確認
            if (player == 1)
            {
                // P1の勝ち
                _textPlayer.text = TEXT_WINNER_P1;
                _textPlayer.color = _spriteDataAsset.GetColorUI(EnumTileState.PlacedCircle);
                _imageFlash.color = _spriteDataAsset.GetColorTile(EnumTileState.PlacedCircle);
            }
            else if (player == 2)
            {
                // P2の勝ち
                _textPlayer.text = TEXT_WINNER_P2;
                _textPlayer.color = _spriteDataAsset.GetColorUI(EnumTileState.PlacedCross);
                _imageFlash.color = _spriteDataAsset.GetColorTile(EnumTileState.PlacedCross);
            }
            else
            {
                MyLogger.LogError("ResultUIManager OnResult エラー：意図しない値でした player[" + player + "]");
                return;
            }

            OnResultAsync().Forget();
        }

        private async UniTaskVoid OnResultAsync()
        {
            // 演出
            await _imageFlash.DOFade(1, MOVE_TIME_FLASH).AsyncWaitForCompletion();
            await _imageFlash.DOFade(0, MOVE_TIME_FLASH).AsyncWaitForCompletion();
            await _imageFlash.DOFade(1, MOVE_TIME_FLASH).AsyncWaitForCompletion();
            await _imageFlash.DOFade(0, MOVE_TIME_FLASH).AsyncWaitForCompletion();

            await UniTask.WaitForSeconds(MOVE_TIME_FLASH_AFTER);

            // 表示
            await _traParent.DOLocalMove(_movePosIn.localPosition, MOVE_TIME_DISPLAY).AsyncWaitForCompletion();

            _traButton.gameObject.SetActive(true);

            // 入力を受け付ける
            _inputManager.OnPushAnyKeyEvent += OnPushAnyKey;
        }

        private void OnPushAnyKey()
        {
            _inputManager.OnPushAnyKeyEvent -= OnPushAnyKey;
            SceneManager.LoadScene("Title");
        }
    }
}