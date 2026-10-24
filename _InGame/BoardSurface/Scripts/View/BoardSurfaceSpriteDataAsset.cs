using UnityEngine;

namespace BoardSurface
{
    /// <summary>
    /// BoardSurfaceで使われるテクスチャデータ
    /// </summary>
    [CreateAssetMenu(fileName = "BoardSurfaceSpriteData", menuName = "ScriptableObjects/BoardSurfaceSpriteDataAsset")]
    public class BoardSurfaceSpriteDataAsset : ScriptableObject
    {
        [Header("駒関連")]
        [SerializeField]
        private Sprite _pieceS;
        [SerializeField]
        private Sprite _pieceM;
        [SerializeField]
        private Sprite _pieceL;

        [SerializeField]
        private Color _colorPiece1P;
        [SerializeField]
        private Color _colorPiece2P;


        [Header("タイル関連")]
        [SerializeField]
        private Sprite _tilePlaced;
        [SerializeField]
        private Sprite _tileEmpty;


        [Header("選択項目関連")]
        [SerializeField]
        private Color _colorSelect1P;
        [SerializeField]
        private Color _colorSelect2P;


        public Sprite PieceS => _pieceS;
        public Sprite PieceM => _pieceM;
        public Sprite PieceL => _pieceL;

        public Sprite TilePlaced => _tilePlaced;
        public Sprite TileEmpty => _tileEmpty;


        public Sprite GetPiece(EnumPieceType pieceType)
        {
            MyLogger.Log("BoardSurfaceSpriteDataAsset GetPiece 引数：player[" + pieceType + "]");

            switch (pieceType)
            {
                case EnumPieceType.S:
                    return _pieceS;
                case EnumPieceType.M:
                    return _pieceM;
                case EnumPieceType.L:
                    return _pieceL;
                default:
                    MyLogger.LogError("BoardSurfaceSpriteDataAsset GetPiece エラー：設定されてないです");
                    return null;
            }
        }

        public Color GetColorPiece(EnumTileState player)
        {
            MyLogger.Log("BoardSurfaceSpriteDataAsset GetColorPiece 引数：player[" + player + "]");

            if (player == EnumTileState.PlacedCircle) return _colorPiece1P;
            if (player == EnumTileState.PlacedCross) return _colorPiece2P;
            MyLogger.LogError("BoardSurfaceSpriteDataAsset GetColorPiece エラー：1P、2P以外の値が指定されました");
            return Color.clear;
        }

        public Color GetColorSelect(EnumTileState player)
        {
            MyLogger.Log("BoardSurfaceSpriteDataAsset GetColorSelect 引数：player[" + player + "]");

            if (player == EnumTileState.PlacedCircle) return _colorSelect1P;
            if (player == EnumTileState.PlacedCross) return _colorSelect2P;
            MyLogger.LogError("BoardSurfaceSpriteDataAsset GetColorSelect エラー：1P、2P以外の値が指定されました");
            return Color.clear;
        }
    }
}