namespace Ships.Models
{
    public class ShipPart
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsHit { get; set; } = false;
    }
}