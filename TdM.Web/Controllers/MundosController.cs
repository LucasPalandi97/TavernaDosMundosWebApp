using Microsoft.AspNetCore.Mvc;

namespace TdM.Web.Controllers
{
    public class MundosController : Controller
    {
        [HttpGet]
        public IActionResult Index(string urlHandle)
        {

            return View();
        }
    }
}
