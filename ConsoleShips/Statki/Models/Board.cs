using Ships.Enums;

namespace Ships.Models
{
    public class Board
    {
        public int Size { get; set; }
        public char[,] Grid { get; set; }
        public List<Ship> Ships { get; set; }

        public Board(int size, int numberOfShips)
        {
            Size = size;
            Grid = new char[size, size];

            Ships = Enumerable.Range(1, numberOfShips).Select(size => new Ship { Size = size }).ToList();
        }

        public void InitializeBoard()
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

            PlaceShips();
        }

        public void Display()
        {
            int rows = Grid.GetLength(0);
            int columns = Grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(Grid[i, j] + " ");
                }
                Console.WriteLine();
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

                    bool isHorizontal = NewRandomBoolean();

                    if (CanPlaceShip(ship, row, column, isHorizontal))
                    {
                        PlaceShip(ship, row, column, isHorizontal);
                        isPlaced = true;
                    }
                }
            }
        }

        private bool CanPlaceShip(Ship ship, int row, int column, bool isHorizontal)
        {
            bool isSpaceEmpty;

            if (!isHorizontal)
            {
                if (column + ship.Size > Size)
                {
                    return false;
                }

                isSpaceEmpty = Enumerable.Range(0, ship.Size).All(i => IsEmptySpace(row, column + i));

                if (!isSpaceEmpty)
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

                isSpaceEmpty = Enumerable.Range(0, ship.Size).All(i => IsEmptySpace(row + i, column));

                if (!isSpaceEmpty)
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

        private Ship PlaceShip(Ship ship, int row, int column, bool isHorizontal)
        {
            ship.Parts = Enumerable.Range(0, ship.Size)
            .Select(i => new ShipPart
            {
                X = isHorizontal ? row + i : row,
                Y = isHorizontal ? column : column + i,
            })
            .ToList();

            return ship;
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

        private bool NewRandomBoolean()
        {
            return new Random().NextDouble() > 0.5;
        }

        public void MarkBoard(int row, int column, BoardStates boardState)
        {
            Grid[row, column] = (char)boardState;
        }
    }
}