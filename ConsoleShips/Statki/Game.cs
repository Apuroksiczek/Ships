using Ships.Models;
using ShipsApi.Models;
using System.Data.Common;

namespace Ships
{
    public class Game : IGame
    {
        private int BoardSize { get; set; }

        private Board playerOneBoard;
        private Board playerTwoBoard;

        private bool PlayerOneGameOver = false;
        private bool PlayerTwoGameOver = false;

        private bool PlayerOneMove = true;

        public Game()
        {
            BoardSize = 2;
            playerOneBoard = new Board(BoardSize, 1);
            playerTwoBoard = new Board(BoardSize, 1);
            PrepareGame();
        }

        public ShipsResponse GetGameStatus()
        {
            if (!PlayerOneGameOver || !PlayerTwoGameOver)
            {
                Step();
            }

            return new ShipsResponse()
            {
                PlayerOneBoard = ConvertCharsToStrings2D(playerOneBoard.Grid),
                PlayerTweBoard = ConvertCharsToStrings2D(playerTwoBoard.Grid),
                PlayerOneMove = PlayerOneMove,
                Winner = GetWinner()
            };
        }

        public void Start()
        {
            while (true)
            {
                PrintBoards();
                Step();
            }
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

        private void CheckGameOver()
        {
            if (playerOneBoard.IsGameOver())
            {
                PlayerOneGameOver = true;
            }

            if (playerTwoBoard.IsGameOver())
            {
                PlayerTwoGameOver = true;
            }
        }

        private void PrepareGame()
        {
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
            if (PlayerOneGameOver)
            {
                return "Player 2";
            }

            if (PlayerTwoGameOver)
            {
                return "Player 1";
            }

            return "";
        }

        private void PrintBoards()
        {
            Console.WriteLine("Player One Board:");
            PrintBoard(playerOneBoard);
            Console.WriteLine("Player Two Board:");
            PrintBoard(playerTwoBoard);
        }

        private void PrintBoard(Board board)
        {
            int rows = board.Grid.GetLength(0);
            int cols = board.Grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(board.Grid[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}