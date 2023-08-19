using Ships.Constants;
using Ships.Models;
using ShipsApi.Models;
using System.Data.Common;

namespace Ships
{
    public class Game : IGame
    {
        private readonly int BoardSize;

        private Board playerOneBoard;
        private Board playerTwoBoard;

        private bool PlayerOneGameOver = false;
        private bool PlayerTwoGameOver = false;

        private bool PlayerOneMove = true;

        public Game()
        {
            BoardSize = ShipsConstants.BoardSize;
            playerOneBoard = new Board(BoardSize, ShipsConstants.NumberOfShips);
            playerTwoBoard = new Board(BoardSize, ShipsConstants.NumberOfShips);
            PrepareGame();
        }

        public ShipsResponse GetGameStatus()
        {
            if (!CheckGameOver())
            {
                Step();
            }

            return new ShipsResponse()
            {
                PlayerOneBoard = ConvertCharsToStrings2D(playerOneBoard.Grid),
                PlayerTwoBoard = ConvertCharsToStrings2D(playerTwoBoard.Grid),
                PlayerOneMove = PlayerOneMove,
                Winner = GetWinner()
            };
        }

        private void Step()
        {
            if (PlayerOneMove)
            {
                ShotBoard(playerTwoBoard);
            }
            else
            {
                ShotBoard(playerOneBoard);
            }
        }

        private IEnumerable<IEnumerable<string>> ConvertCharsToStrings2D(char[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                List<string> rowStrings = new List<string>();
                for (int j = 0; j < cols; j++)
                {
                    rowStrings.Add(grid[i, j].ToString());
                }
                yield return rowStrings;
            }
        }

        private bool CheckGameOver()
        {
            if (PlayerTwoGameOver || PlayerTwoGameOver)
            {
                return true;
            }

            if (playerOneBoard.IsGameOver())
            {
                PlayerOneGameOver = true;
                return true;
            }

            if (playerTwoBoard.IsGameOver())
            {
                PlayerTwoGameOver = true;
                return true;
            }
            return false;
        }

        public void PrepareGame()
        {
            PlayerOneGameOver = false;
            PlayerTwoGameOver = false;
            PlayerOneMove = true;

            playerOneBoard.InitializeBoard();
            playerTwoBoard.InitializeBoard();
        }

        private (int, int) GetRandomCoordinates()
        {
            int row = new Random().Next(BoardSize);
            int column = new Random().Next(BoardSize);

            return (row, column);
        }

        private void ShotBoard(Board board)
        {
            while (true)
            {
                (int, int) corrdinates = GetRandomCoordinates();

                if (board.IsEmptySpace(corrdinates.Item1, corrdinates.Item2))
                {
                    board.ShotBoard(corrdinates.Item1, corrdinates.Item2);
                    break;
                }
            }

            PlayerOneMove = !PlayerOneMove;
            CheckGameOver();
        }

        private string GetWinner()
        {
            if (PlayerOneGameOver == true)
            {
                return PlayerNamesConstants.Player2;
            }

            if (PlayerTwoGameOver == true)
            {
                return PlayerNamesConstants.Player1;
            }

            return PlayerNamesConstants.None;
        }
    }
}