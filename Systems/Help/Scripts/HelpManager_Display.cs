using DG.Tweening;
using UnityEngine;

namespace Help
{
    /// <summary>
    /// ヘルプ画面の表示、非表示挙動
    /// </summary>
    [System.Serializable]
    public class HelpManager_Display
    {
        public const float MOVE_TIME = 0.3f;

        [SerializeField]
        private RectTransform _rectTransformImage;

        [SerializeField]
        private RectTransform _rectTransformPointIn;
        [SerializeField]
        private RectTransform _rectTransformPointOut;

        private GameObject _gameObject;


        public void OnStart(GameObject obj)
        {
            _gameObject = obj;
        }

        public void OnDisplay()
        {
            _rectTransformImage.DOLocalMove(_rectTransformPointIn.localPosition, MOVE_TIME);
        }
        public void OnHide()
        {
            _rectTransformImage.DOLocalMove(_rectTransformPointOut.localPosition, MOVE_TIME);
        }
    }
}