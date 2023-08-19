using Ships.Models;
using System.Data.Common;

namespace Ships
{
    public class Game
    {
        private int BoardSize { get; set; }

        private Board playerOneBoard;
        private Board playerTwoBoard;

        private bool IsGameRunning = true;
        private bool PlayerOneMove = true;

        public Game(int boardSize, int numberOfShips)
        {
            BoardSize = boardSize;
            playerOneBoard = new Board(BoardSize, numberOfShips);
            playerTwoBoard = new Board(BoardSize, numberOfShips);
        }

        public void Start()
        {
            PrepareGame();

            while (IsGameRunning)
            {
                PrintBoards();

                if (PlayerOneMove)
                {
                    ShotBoard(playerTwoBoard);
                }
                else
                {
                    ShotBoard(playerOneBoard);
                }

                CheckGameOver();
            }
        }

        private void CheckGameOver()
        {
            if (playerOneBoard.IsGameOver())
            {
                Console.WriteLine("GAME OVER");
                Console.WriteLine("PLAYER ONE WINS");
                Console.ReadLine();
            }

            if (playerTwoBoard.IsGameOver())
            {
                Console.WriteLine("GAME OVER");
                Console.WriteLine("PLAYER TWO WINS");
                Console.ReadLine();
            }
        }

        private void PrintBoards()
        {
            Console.WriteLine("PLAYER 1 BOARD:");
            playerOneBoard.Display();
            Console.WriteLine("PLAYER 2 BOARD:");
            playerTwoBoard.Display();
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
        }
    }
}