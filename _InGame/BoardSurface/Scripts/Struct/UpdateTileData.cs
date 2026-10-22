using UnityEngine;

namespace BoardSurface.CurrentTileData
{
    /// <summary>
    /// タイルのデータを新しいものに差し替えるデータ
    /// </summary>
    public struct UpdateTileData
    {
        public Vector2Int pos;
        public EnumPieceType PieceType;
    }
}