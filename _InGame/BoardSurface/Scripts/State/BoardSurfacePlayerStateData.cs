namespace BoardSurface
{
    /// <summary>
    /// プレイヤーの持ち駒のデータ
    /// </summary>
    public class BoardSurfacePlayerStateData
    {
        private const int PROVIDE_PIECE_S_VALUE = 2;
        private const int PROVIDE_PIECE_M_VALUE = 2;
        private const int PROVIDE_PIECE_L_VALUE = 1;

        private int _pieceSValue = 0;
        private int _pieceMValue = 0;
        private int _pieceLValue = 0;


        /// <summary>
        /// プレイヤーが置く駒の種類（◯✕のどちらか）
        /// </summary>
        public readonly EnumTileState PlacingType;

        public int GetPieceSValue => _pieceSValue;
        public int GetPieceMValue => _pieceMValue;
        public int GetPieceLValue => _pieceLValue;


        public BoardSurfacePlayerStateData(EnumTileState placingType)
        {
            OnProvidePiece();
            PlacingType = placingType;
        }



        /// <summary>
        /// 駒を追加
        /// </summary>
        public void OnProvidePiece()
        {
            MyLogger.Log("BoardSurfacePlayerStateData OnProvidePiece");

            //_pieceSValue += PROVIDE_PIECE_S_VALUE;
            //_pieceMValue += PROVIDE_PIECE_M_VALUE;
            //_pieceLValue += PROVIDE_PIECE_L_VALUE;
            _pieceSValue = PROVIDE_PIECE_S_VALUE;
            _pieceMValue = PROVIDE_PIECE_M_VALUE;
            _pieceLValue = PROVIDE_PIECE_L_VALUE;
        }

        /// <summary>
        /// 特定の駒を減算
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool OnSubPiece(EnumPieceType type)
        {
            MyLogger.Log("BoardSurfacePlayerStateData OnSubPiece 引数：type[" + type + "]");

            switch (type)
            {
                case EnumPieceType.S:
                    if (_pieceSValue == 0) return false;
                    --_pieceSValue;
                    break;
                case EnumPieceType.M:
                    if (_pieceMValue == 0) return false;
                    --_pieceMValue;
                    break;
                case EnumPieceType.L:
                    if (_pieceLValue == 0) return false;
                    --_pieceLValue;
                    break;
            }

            return true;
        }

        /// <summary>
        /// 指定した駒を所持しているか確認
        /// </summary>
        /// <returns></returns>
        public bool OnCheckNonePiece(EnumPieceType pieceType)
        {
            MyLogger.Log("BoardSurfacePlayerStateData OnCheckNonePiece 引数：pieceType[" + pieceType + "]");

            switch (pieceType)
            {
                case EnumPieceType.S:
                    return _pieceSValue != 0;
                case EnumPieceType.M:
                    return _pieceMValue != 0;
                case EnumPieceType.L:
                    return _pieceLValue != 0;
                default:
                    MyLogger.LogError("BoardSurfacePlayerStateData OnCheckNonePiece エラー：存在しない駒が指定されました");
                    return false;
            }
        }
    }
}