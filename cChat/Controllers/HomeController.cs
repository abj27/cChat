using System.Diagnostics;
using cChat.BusinessLogic.Services;
using cChat.Data.Repositories;
using cChat.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cChat.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly  IChatRoomService _chatRoomService;

        public HomeController(ILogger<HomeController> logger, IChatRoomService chatRoomService)
        {
            _logger = logger;
            _chatRoomService= chatRoomService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
