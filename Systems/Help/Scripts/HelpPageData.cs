using UnityEngine;

namespace Help
{
    /// <summary>
    /// ヘルプ画面の１ページ分のデータ
    /// </summary>
    [System.Serializable]
    public class HelpPageData
    {
        [SerializeField]
        private RectTransform _rectTransformImage;

        private string _stringExplanation;


        public RectTransform RestTransformImage => _rectTransformImage;

        public string StringExplanation => _stringExplanation;


        /// <summary>
        /// 説明テキストを初期化する（上書き不可）
        /// </summary>
        /// <param name="text"></param>
        public void OnSetExplanationText(string text)
        {
            // テキストが設定されていたら終了
            if (_stringExplanation != null) return;

            // 引数のテキストが設定されていなかったら終了
            if (text.Length == 0) return;
            
            // 反映
            _stringExplanation = text;
        }
    }
}