using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IGameService GameService { get; }

        public HomeController(ILogger<HomeController> logger,IGameService gameService)
        {
            _logger = logger;
            GameService = gameService;
        }

        public IActionResult Index()
        {
            var games = GameService.GetAll();
            return View(games);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
