using Microsoft.AspNetCore.Mvc;
using Ships;

namespace ShipsApi.Controllers
{
    [ApiController]
    [Route("api/ships")]
    public class ShipsController : Controller
    {
        private readonly IGame _game;

        public ShipsController(IGame game)
        {
            _game = game;
        }

        [HttpPost("next-step")]
        public IActionResult NextStep()
        {
            return Ok(_game.GetGameStatus());
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _game.PrepareGame();
            return Ok();
        }
    }
}