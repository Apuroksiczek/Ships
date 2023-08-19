namespace Ships.Models
{
    public class Ship
    {
        public int Size { get; set; }

        public bool IsHorizontal { get; set; }

        public bool isSinked { get; set; } = false;
        public List<ShipPart> Parts { get; set; }
    }
}