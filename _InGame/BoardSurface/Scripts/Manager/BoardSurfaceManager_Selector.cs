using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoardSurface
{
    /// <summary>
    /// 盤面の選択をするクラス
    /// </summary>
    [System.Serializable]
    public class BoardSurfaceManager_Selector
    {
        [SerializeField]
        private BoardSurfaceInputBase _inputBase;


        /// <summary>
        /// 入力結果からボードの状態を変更する
        /// </summary>
        /// <returns></returns>
        private async UniTask OnChangeBoardAsync()
        {
            // 入力を待つ
            EnumInputType inputType = await _inputBase.OnCheckInputAsync();

            // 入力の結果を反映する

        }

    }
}