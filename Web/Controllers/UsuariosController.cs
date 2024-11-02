using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        private Sistema sistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Error = TempData["Error"];
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (sistema.UsuarioActual != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Clave)
        {
            try
            {
                sistema.IniciarSesion(Email, Clave);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View("Login");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            sistema.UsuarioActual = null;
            return RedirectToAction("Login");
        }
    }
}
