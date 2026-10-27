using TMPro;
using UnityEngine;

namespace Help
{
    /// <summary>
    /// ヘルプ画面のページ処理関連
    /// </summary>
    [System.Serializable]
    public class HelpManager_PageSelector
    {
        private const int PAGE_MAX = 4;

        [Header("UI要素")]
        [SerializeField]
        private TMP_Text _textExplanation;


        [Header("ページデータ")]
        [SerializeField]
        private HelpPageData _pageData1;

        [SerializeField]
        private HelpPageData _pageData2;

        [SerializeField]
        private HelpPageData _pageData3;

        [SerializeField]
        private HelpPageData _pageData4;


        private int _pageIndex = 1;

        private RectTransform _rectTransformCurrentImage;


        public void OnStart()
        {
            _pageData1.OnSetExplanationText("<color=#FF8773>タテ・ヨコ・ナナメのいずれか一列に自分の電池を3つならべ、" +
                                            "\r\n回路を完成させて「通電」させれば勝利</color>となります。" +
                                            "\r\n空いているソケットに電池を取り付けていきましょう。");
            _pageData2.OnSetExplanationText("電池には<color=#FF8773>3種類のランク</color>があり、それぞれ<color=#FF8773>プレイヤーが持っている電池は計5つ。</color>" +
                                            "\r\nそのうち最高ランクの電池は1つしか持たないため、" +
                                            "\r\nどのタイミングで、どのソケットに取り付けるかよく考えましょう。");
            _pageData3.OnSetExplanationText("ソケットがすべてうまってしまった場合は、" +
                                            "\r\n一度すべての電池を「取り外して」最初からやり直しとなります。" +
                                            "\r\nただし...");
            _pageData4.OnSetExplanationText("前回取り付けられたソケットは「バージョンアップ」されます。" +
                                            "\r\nそのため、 <color=#FF8773>直前に取り付けられた電池</color>に対して、" +
                                            "\r\n<color=#FF8773>ランクが「同等」もしくは「上位」の電池</color>でなければ取り付けできません。<line-height=150%>" +
                                            "\r\n※前回自分の電池を取り付けたソケットであればどこでも取り付けできます！");

            OnChangePage(_pageIndex);
        }

        /// <summary>
        /// 次のページへ
        /// </summary>
        public void OnNextPage()
        {
            int nextPage = _pageIndex + 1;

            // 範囲外チェック
            if (nextPage > PAGE_MAX) return;

            // ページ変更
            OnChangePage(nextPage);
        }

        /// <summary>
        /// 前のページへ
        /// </summary>
        public void OnPrevPage()
        {
            int nextPage = _pageIndex - 1;

            // 範囲外チェック
            if (nextPage < 1) return;

            // ページ変更
            OnChangePage(nextPage);
        }

        /// <summary>
        /// ページ変更
        /// </summary>
        /// <param name="nextPage"></param>
        private void OnChangePage(int nextPage)
        {
            if (_pageIndex != nextPage)
            {
                _pageIndex = nextPage;
                _rectTransformCurrentImage.gameObject.SetActive(false);
            }

            HelpPageData pageData = null;

            switch (nextPage)
            {
                case 1:
                    pageData = _pageData1;
                    break;
                case 2:
                    pageData = _pageData2;
                    break;
                case 3:
                    pageData = _pageData3;
                    break;
                case 4:
                    pageData = _pageData4;
                    break;
                default:
                    MyLogger.LogError("HelpManager_PageSelector OnChangePage エラー：不定値 nextPage["+ nextPage +"]");
                    break;
            }

            _rectTransformCurrentImage = pageData.RestTransformImage;

            _rectTransformCurrentImage.gameObject.SetActive(true);
            _textExplanation.text = pageData.StringExplanation;
        }
    }
}