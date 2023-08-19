using ShipsApi.Models;

namespace Ships
{
    public interface IGame
    {
        ShipsResponse GetGameStatus();

        void Start();

        public void PrepareGame();
    }
}