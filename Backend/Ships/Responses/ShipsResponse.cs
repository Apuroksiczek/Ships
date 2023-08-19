namespace ShipsApi.Models
{
    public class ShipsResponse
    {
        public IEnumerable<IEnumerable<string>> PlayerOneBoard { get; set; }
        public IEnumerable<IEnumerable<string>> PlayerTwoBoard { get; set; }
        public bool PlayerOneMove { get; set; }
        public string Winner { get; set; }
    }
}