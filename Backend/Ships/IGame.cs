using ShipsApi.Models;

namespace Ships
{
    public interface IGame
    {
        ShipsResponse GetGameStatus();

        public void PrepareGame();
    }
}