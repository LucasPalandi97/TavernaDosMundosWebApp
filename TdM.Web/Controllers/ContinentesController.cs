using Microsoft.AspNetCore.Mvc;

namespace TdM.Web.Controllers
{
    public class ContinentesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
