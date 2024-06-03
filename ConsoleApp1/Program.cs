using System;

namespace ex0425_OXGame
{
    public class OXGameEngine
    {
        private char[,] gameMarkers; // 二維陣列，用於儲存遊戲狀態

        public OXGameEngine()
        {
            gameMarkers = new char[3, 3]; // 初始化遊戲狀態陣列
            ResetGame(); // 重置遊戲
        }

        public void SetMarker(int x, int y, char player)
        {
            if (IsValidMove(x, y))
            {
                gameMarkers[x, y] = player; // 設置玩家標記到指定位置
            }
            else
            {
                throw new ArgumentException("Invalid move!"); // 若移動無效，拋出ArgumentException
            }
        }

        public void ResetGame()
        {
            gameMarkers = new char[3, 3]; // 重置遊戲狀態為空白
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameMarkers[i, j] = ' ';
                }
            }
        }

        public char IsWinner()
        {
            // 檢查橫向是否有連線
            for (int i = 0; i < 3; i++)
            {
                if (gameMarkers[i, 0] != ' ' && gameMarkers[i, 0] == gameMarkers[i, 1] && gameMarkers[i, 1] == gameMarkers[i, 2])
                {
                    return gameMarkers[i, 0]; // 若有連線，返回贏家標記
                }
            }

            // 檢查縱向是否有連線
            for (int j = 0; j < 3; j++)
            {
                if (gameMarkers[0, j] != ' ' && gameMarkers[0, j] == gameMarkers[1, j] && gameMarkers[1, j] == gameMarkers[2, j])
                {
                    return gameMarkers[0, j]; // 若有連線，返回贏家標記
                }
            }

            // 檢查對角線是否有連線
            if (gameMarkers[0, 0] != ' ' && gameMarkers[0, 0] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 2])
            {
                return gameMarkers[0, 0]; // 若有連線，返回贏家標記
            }

            if (gameMarkers[0, 2] != ' ' && gameMarkers[0, 2] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 0])
            {
                return gameMarkers[0, 2]; // 若有連線，返回贏家標記
            }

            return ' '; // 沒有贏家，返回空白
        }

        private bool IsValidMove(int x, int y)
        {
            if (x < 0 || x >= 3 || y < 0 || y >= 3)
            {
                return false; // 檢查位置是否在合法範圍內
            }

            if (gameMarkers[x, y] != ' ')
            {
                return false; // 檢查位置是否已經被佔據
            }

            return true;
        }

        public char GetMarker(int x, int y)
        {
            return gameMarkers[x, y]; // 取得指定位置的標記
        }
    }

    class Program
    {
        static void Main()
        {
            OXGameEngine gameEngine = new OXGameEngine(); // 創建遊戲引擎實例
            char currentPlayer = 'X'; // 設置當前玩家為 X

            Console.WriteLine("遊戲開始！");

            while (true)
            {
                // 顯示遊戲盤面
                Console.WriteLine("遊戲中的畫面：");
                Console.WriteLine("╔═════╦═════╦═════╗"); // 修改盤面邊框大小
                for (int i = 0; i < 3; i++)
                {
                    Console.Write("║ ");
                    for (int j = 0; j < 3; j++)
                    {
                        // 設置文字顏色
                        Console.ForegroundColor = (gameEngine.GetMarker(i, j) == 'X') ? ConsoleColor.Red : ConsoleColor.Blue;
                        // 顯示指定位置的標記
                        Console.Write(gameEngine.GetMarker(i, j));
                        // 恢復文字顏色為預設值
                        Console.ResetColor();
                        Console.Write(" ║ ");
                    }
                    Console.WriteLine();
                    if (i != 2)
                        Console.WriteLine("╠═════╬═════╬═════╣"); // 修改盤面邊框大小
                }
                Console.WriteLine("╚═════╩═════╩═════╝"); // 修改盤面邊框大小

                // 檢查是否有贏家
                char winner = gameEngine.IsWinner();
                if (winner != ' ')
                {
                    Console.WriteLine($"贏家為: {winner}");
                    break;
                }

                // 獲取玩家的移動
                Console.WriteLine($"輪到 {currentPlayer} 下棋。");
                Console.WriteLine("請輸入橫向和縱向位置(0, 1, 2)，以空格分隔：");
                string[] input = Console.ReadLine().Split(' ');
                if (input.Length != 2)
                {
                    Console.WriteLine("輸入格式錯誤，請重新輸入。");
                    continue;
                }
                int x, y;
                if (!int.TryParse(input[0], out x) || !int.TryParse(input[1], out y))
                {
                    Console.WriteLine("輸入格式錯誤，請重新輸入。");
                    continue;
                }

                // 設置玩家的標記
                try
                {
                    gameEngine.SetMarker(x, y, currentPlayer);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // 切換玩家
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }

            Console.WriteLine("遊戲結束。");
        }
    }
}
