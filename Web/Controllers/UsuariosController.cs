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
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Si ya hay un usuario logueado, redirigir a la página principal usando Session
            if (HttpContext.Session.GetString("Email") != null) return RedirectToAction("Index", "Home");

            ViewBag.Exito = TempData["Exito"];
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Clave)
        {
            try
            {
                Usuario u = sistema.IniciarSesion(Email, Clave);

                HttpContext.Session.SetString("Email", u.Email);
                HttpContext.Session.SetString("Rol", u.TipoUsuario());

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Login");
            }
        }

        [HttpGet]
        public IActionResult Registro()
        {
            if (HttpContext.Session.GetString("Email") != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Registro(string Nombre, string Apellido, string Email, string Clave)
        {
            try
            {
                sistema.AltaCliente(Nombre, Apellido, Email, Clave);
                TempData["Exito"] = "¡Registro exitoso!";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Registro");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
