using UnityEngine;

namespace BoardSurface
{
    /// <summary>
    /// 盤面上の状態についてのデータ
    /// </summary>
    public class BoardSurfaceStateData
    {
        private const int TILE_SIZE_X = 3;
        private const int TILE_SIZE_Y = 3;
        private const int CONNECT_VALUE = 3;

        private struct TileData
        {
            public EnumTileState TileState { get; set; }
            public EnumPieceType PieceType { get; set; }
        }


        private TileData[,] _tileDatas = new TileData[TILE_SIZE_X, TILE_SIZE_Y];


        /// <summary>
        /// タイルの配置状態を変更する
        /// </summary>
        /// <param name="tileState"></param>
        /// <param name="pieceType"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public bool SetTileData(int x, int y, EnumTileState tileState, EnumPieceType pieceType)
        {
            // 範囲内チェック
            if (!OnCheckWthinTheRange(x, y)) return false;

            _tileDatas[x, y].TileState = tileState;
            _tileDatas[x, y].PieceType = pieceType;

            return true;
        }

        /// <summary>
        /// タイルの配置状況を取得する
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public (bool, EnumTileState, EnumPieceType) GetTileData(int x, int y)
        {
            // 範囲内チェック
            if (!OnCheckWthinTheRange(x, y)) return (false, EnumTileState.Empty, EnumPieceType.S);

            EnumTileState tileState = _tileDatas[x, y].TileState;
            EnumPieceType pieceType = _tileDatas[x, y].PieceType;

            return (true, tileState, pieceType);
        }


        /// <summary>
        /// 勝敗がついているかチェック
        /// </summary>
        /// <returns></returns>
        public bool OnCheckWinner(Vector2Int checkPos, EnumTileState tileState)
        {
            return OnConnectionExploration(checkPos, tileState);
        }

        /// <summary>
        /// 盤面が埋まってるかチェック
        /// </summary>
        /// <returns></returns>
        public bool OnCheckBuried()
        {
            for (int x = 0; x < TILE_SIZE_X; ++x)
            {
                for (int y = 0; y < TILE_SIZE_Y; ++y)
                {
                    // 空いていないか確認
                    if (_tileDatas[x,y].TileState == EnumTileState.PlacedCircle ||
                        _tileDatas[x, y].TileState == EnumTileState.PlacedCircle)
                    {
                        // 駒が配置されていて空いてない
                        continue;
                    }

                    // 空きがある
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 盤面の駒を取り除く
        /// </summary>
        /// <returns></returns>
        public void OnResetBoard()
        {
            for (int x = 0; x < TILE_SIZE_X; ++x)
            {
                for (int y = 0; y < TILE_SIZE_Y; ++y)
                {
                    if (_tileDatas[x, y].TileState == EnumTileState.PlacedCircle)
                    {
                        _tileDatas[x, y].TileState = EnumTileState.EmptyCircle;
                    }
                    if (_tileDatas[x, y].TileState == EnumTileState.PlacedCross)
                    {
                        _tileDatas[x, y].TileState = EnumTileState.EmptyCross;
                    }
                }
            }
        }



        /// <summary>
        /// 範囲内チェック
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool OnCheckWthinTheRange(int x, int y)
        {
            if (0 > x || x <= TILE_SIZE_X || 0 > y || y <= TILE_SIZE_Y)
            {
                Debug.LogWarning("範囲外でした");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 連結探査
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tileState"></param>
        /// <returns></returns>
        private bool OnConnectionExploration(Vector2Int checkPos, EnumTileState tileState)
        {
            int connectPoint = 1;
            Vector2Int addPos = new();

            for (int i = 0; i < 3; ++i)
            {
                connectPoint = 1;

                switch (i)
                {
                    case 0:
                        addPos = new(1, 0);
                        break;
                        case 1:
                        addPos = new(1, 1);
                        break;
                        case 2:
                        addPos = new(0, 1);
                        break;
                }

                // 探査
                OnConnectionExploration_AdjacentTile(checkPos, addPos, tileState, ref connectPoint);

                // 方向を逆にして探査
                addPos *= -Vector2Int.one;
                OnConnectionExploration_AdjacentTile(checkPos, addPos, tileState, ref connectPoint);

                // 連結している数が指定数以上か
                if (connectPoint >= CONNECT_VALUE)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 連結探査_指定された場所のタイルが指定された値と同じかを調べる
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tileState"></param>
        /// <param name="connectPoint"></param>
        private void OnConnectionExploration_AdjacentTile(Vector2Int checkPos, Vector2Int addPos, EnumTileState tileState, ref int connectPoint)
        {
            Vector2Int explorationPos = checkPos + addPos;
            if (tileState == _tileDatas[explorationPos.x, explorationPos.y].TileState)
            {
                // 連結している
                ++connectPoint;

                // 次の探査へ
                OnConnectionExploration_AdjacentTile(explorationPos, addPos, tileState, ref connectPoint);
            }
        }
    }
}