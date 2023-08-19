using Ships.Enums;
using System.Data.Common;

namespace Ships.Models
{
    public class Board
    {
        public Board(int size, int numberOfShips)
        {
            Size = size;
            Grid = new char[size, size];
            NumberOfShips = numberOfShips;
        }

        public char[,] Grid { get; set; }
        public int NumberOfShips { get; set; }
        public List<Ship> Ships { get; set; }
        public int Size { get; set; }

        public void SetGameToDeafult()
        {
            InitializeGrid();
            Ships = Enumerable.Range(1, NumberOfShips).Select(size => new Ship { Size = size }).ToList();

            PlaceShips();
            InitializeGrid();
        }

        public bool IsEmptySpace(int row, int column)
        {
            return Grid[row, column] == (char)BoardStates.Empty;
        }

        public bool IsGameOver()
        {
            foreach (var ship in Ships)
            {
                if (ship.isSinked == false)
                {
                    return false;
                }
            }
            return true;
        }

        public void ShotBoard(int row, int column)
        {
            foreach (var ship in Ships)
            {
                var partHit = ship.Parts.FirstOrDefault(p => p.X == row && p.Y == column);
                if (partHit != null)
                {
                    partHit.IsHit = true;
                    MarkBoard(row, column, BoardStates.Hit);

                    if (ship.IsShipSinked())
                    {
                        ship.isSinked = true;
                        SinkShipOnBoard(ship);
                    }

                    return;
                }
            }

            MarkBoard(row, column, BoardStates.Missed);
        }

        private void SinkShipOnBoard(Ship ship)
        {
            ship.Parts.ForEach(x =>
            {
                MarkBoard(x.X, x.Y, BoardStates.Sinked);
            });
        }

        private void InitializeGrid()
        {
            int rows = Grid.GetLength(0);
            int columns = Grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    MarkBoard(i, j, BoardStates.Empty);
                }
            }
        }

        private List<ShipPart> InitAndPlaceShipParts(Ship ship, int row, int column, bool isHorizontal)
        {
            if (!CanPlaceShip(ship, row, column, isHorizontal))
            {
                return null;
            }

            List<ShipPart> parts = new List<ShipPart>();

            for (int i = 0; i < ship.Size; i++)
            {
                int x = isHorizontal ? row + i : row;
                int y = isHorizontal ? column : column + i;

                if (!IsEmptySpace(x, y))
                {
                    return null;
                }

                parts.Add(new ShipPart { X = x, Y = y });
                MarkBoard(x, y, BoardStates.Ship);
            }

            return parts;
        }

        private bool CanPlaceShip(Ship ship, int row, int column, bool isHorizontal)
        {
            if (!isHorizontal)
            {
                if (column + ship.Size > Size)
                {
                    return false;
                }
            }
            else
            {
                if (row + ship.Size > Size)
                {
                    return false;
                }
            }
            return true;
        }

        private void PlaceShips()
        {
            foreach (var ship in Ships)
            {
                bool isPlaced = false;

                while (!isPlaced)
                {
                    int row = new Random().Next(Size);
                    int column = new Random().Next(Size);

                    bool isHorizontal = RandomBoolean();

                    ship.Parts = InitAndPlaceShipParts(ship, row, column, isHorizontal);

                    if (ship.Parts != null)
                    {
                        isPlaced = true;
                    }
                }
            }
        }

        public void MarkBoard(int row, int column, BoardStates boardState)
        {
            Grid[row, column] = (char)boardState;
        }

        private bool RandomBoolean()
        {
            return new Random().NextDouble() > 0.5;
        }
    }
}