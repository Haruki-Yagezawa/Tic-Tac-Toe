namespace BoardSurface
{
    /// <summary>
    /// 盤面上一つ一つのタイルの状態
    /// </summary>
    public enum EnumTileState
    {
        /// <summary>
        /// 空白
        /// </summary>
        Empty, 
        /// <summary>
        /// ◯が配置されている
        /// </summary>
        PlacedCircle, 
        /// <summary>
        /// ✕が配置されている
        /// </summary>
        PlacedCross, 
        /// <summary>
        /// 前回◯が配置されていた
        /// </summary>
        EmptyCircle,
        /// <summary>
        /// 前回✕が配置されていた
        /// </summary>
        EmptyCross,

    }
}