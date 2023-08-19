using Microsoft.AspNetCore.Mvc;
using Ships;
using ShipsApi.Models;

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

        [HttpGet("game-status")]
        public IActionResult Get()
        {
            return Ok(_game.GetGameStatus());
        }

        [HttpPut("reset")]
        public IActionResult Reset()
        {
            _game.PrepareGame();
            return Ok();
        }
    }
}