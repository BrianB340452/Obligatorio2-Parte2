using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ArticulosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
