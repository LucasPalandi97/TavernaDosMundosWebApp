using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TdM.Web.Models;
using TdM.Web.Repositories;
namespace TdM.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IMundoRepository MundoRepository { get; }

        public HomeController(ILogger<HomeController> logger, IMundoRepository mundoRepository)
        {
            _logger = logger;
            MundoRepository = mundoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var mundo = await MundoRepository.GetAllAsync(1, 10);

            return View(mundo);
        }

        public IActionResult testcode()
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