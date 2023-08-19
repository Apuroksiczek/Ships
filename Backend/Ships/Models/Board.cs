using Ships.Enums;
using System.Data.Common;

namespace Ships.Models
{
    public class Board
    {
        public int Size { get; set; }
        public int NumberOfShips { get; set; }
        public char[,] Grid { get; set; }
        public List<Ship> Ships { get; set; }

        public Board(int size, int numberOfShips)
        {
            Size = size;
            Grid = new char[size, size];
            NumberOfShips = numberOfShips;
        }

        public void InitializeBoard()
        {
            InitializeMap();
            Ships = Enumerable.Range(1, NumberOfShips).Select(size => new Ship { Size = size }).ToList();

            PlaceShips();
            InitializeMap();
        }

        private void InitializeMap()
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

        public void ShotBoard(int row, int column)
        {
            foreach (var ship in Ships)
            {
                var part = ship.Parts.FirstOrDefault(p => p.X == row && p.Y == column);
                if (part != null)
                {
                    part.IsHit = true;
                    MarkBoard(row, column, BoardStates.Hit);

                    if (IsShipSinked(ship.Parts))
                    {
                        ship.isSinked = true;
                        SinkShip(ship);
                    }

                    return;
                }
            }

            MarkBoard(row, column, BoardStates.Missed);
        }

        private Ship SinkShip(Ship ship)
        {
            ship.isSinked = true;
            ship.Parts.ForEach(x =>
            {
                MarkBoard(x.X, x.Y, BoardStates.Sinked);
            });

            return ship;
        }

        private bool IsShipSinked(List<ShipPart> parts)
        {
            foreach (var part in parts)
            {
                if (part.IsHit == false)
                    return false;
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

                    ship.Parts = PlaceShip(ship, row, column, isHorizontal);

                    if (ship.Parts != null)
                    {
                        isPlaced = true;
                    }
                }
            }
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

        public bool IsEmptySpace(int row, int column)
        {
            return Grid[row, column] == (char)BoardStates.Empty;
        }

        private List<ShipPart> PlaceShip(Ship ship, int row, int column, bool isHorizontal)
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

        private bool RandomBoolean()
        {
            return new Random().NextDouble() > 0.5;
        }

        public void MarkBoard(int row, int column, BoardStates boardState)
        {
            Grid[row, column] = (char)boardState;
        }
    }
}