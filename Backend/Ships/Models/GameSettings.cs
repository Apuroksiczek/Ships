namespace Ships.Models
{
    public class GameSettings
    {
        public const string SectionName = "GameSettings";
        public int BoardSize { get; init; }
        public int NumberOfShips { get; init; }
    }
}