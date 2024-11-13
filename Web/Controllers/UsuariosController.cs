using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        private Sistema sistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Login()
        {
            // Si ya hay un usuario logueado, redirigir a la página principal
            if (HttpContext.Session.GetString("Email") != null) return RedirectToAction("Index", "Home");

            //Viewbag en caso de éxito al registrarse
            ViewBag.Exito = TempData["Exito"];

            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Clave)
        {
            // Si ya hay un usuario logueado, redirigir a la página principal
            if (HttpContext.Session.GetString("Email") != null) return RedirectToAction("Index", "Home");

            try
            {
                Usuario u = sistema.IniciarSesion(Email, Clave);

                HttpContext.Session.SetInt32("Id", u.Id);
                HttpContext.Session.SetString("Email", u.Email);
                HttpContext.Session.SetString("Rol", u.TipoUsuario());

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Registro()
        {
            // Si ya hay un usuario logueado, redirigir a la página principal
            if (HttpContext.Session.GetString("Email") != null) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Registro(string nombre, string apellido, string email, string clave)
        {
            // Si ya hay un usuario logueado, redirigir a la página principal
            if (HttpContext.Session.GetString("Email") != null) return RedirectToAction("Index", "Home");

            try
            {
                Cliente c = new Cliente(nombre, apellido, email, clave, 2000);
                sistema.AltaUsuario(c);
                TempData["Exito"] = "¡Registro exitoso!";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult RecargarSaldo()
        {
            // Autorizaciones
            if (HttpContext.Session.GetString("Rol") == null) return RedirectToAction("Login", "Usuarios");
            if (HttpContext.Session.GetString("Rol") != "Cliente") return View("Unauthorized");

            string? email = HttpContext.Session.GetString("Email");
            ViewBag.Saldo = sistema.BuscarClientePorEmail(email).Saldo;

            return View();
        }

        [HttpPost]
        public IActionResult RecargarSaldo(int saldo)
        {
            // Autorizaciones
            if (HttpContext.Session.GetString("Email") == null) return RedirectToAction("Login", "Usuarios");
            if (HttpContext.Session.GetString("Rol") != "Cliente") return View("Unauthorized");

            try
            {
                string? email = HttpContext.Session.GetString("Email");
                ViewBag.Saldo = sistema.RecargarSaldo(email, saldo);
                ViewBag.Exito = $"Su recarga de ${saldo} se ha procesado exitosamente.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Se borran los datos de la sesión actual y se redirige al login
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
