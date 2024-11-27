using Dominio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class PublicacionesController : Controller
{
    private Sistema sistema = Sistema.Instancia;

    [HttpGet]
    public IActionResult ListadoPublicaciones(string nombre, int estado)
    {
        // Autorizaciones
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToAction("Login","Usuarios");
        if (HttpContext.Session.GetString("Rol") != "Cliente") return View("Unauthorized");

        ViewBag.publicaciones = sistema.ListarPublicacionesFiltradas(nombre, estado);
        ViewBag.nombre = nombre;
        ViewBag.estado = estado;

        return View();
    }

    [HttpGet]
    public IActionResult ListadoSubastas(string nombre, int estado)
    {
        // Autorizaciones
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToAction("Login", "Usuarios");
        if (HttpContext.Session.GetString("Rol") != "Administrador") return View("Unauthorized");

        if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];
        if (TempData["Exito"] != null) ViewBag.Exito = TempData["Exito"];

        ViewBag.subastas = sistema.ListarSubastasFiltradas(nombre, estado);

        ViewBag.nombre = nombre;
        ViewBag.estado = estado;

        return View();
    }

    [HttpPost]
    public IActionResult CerrarSubasta(int id)
    {
        // Autorizaciones
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToAction("Login", "Usuarios");
        if (HttpContext.Session.GetString("Rol") != "Administrador") return View("Unauthorized");

        try
        {
            //Para cerrar una subasta, necesitamos la subasta y el administrador, quien se obtendrá de la sesión, ya que está logueado.
            string email = HttpContext.Session.GetString("Email");
            sistema.CerrarSubasta(email, id);
            //usamos TempData para los mensajes que se mostraran en otra accion de este controlador.
            TempData["Exito"] = $"El cierre de la subasta N.º{id} se procesó correctamente.";
            return RedirectToAction("ListadoSubastas");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("ListadoSubastas");
        }
    }

    [HttpGet]
    public IActionResult Venta(int id)
    {
        // Autorizaciones
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToAction("Login", "Usuarios");
        if (HttpContext.Session.GetString("Rol") != "Cliente") return View("Unauthorized");

        ViewBag.venta = sistema.BuscarVentaPorId(id);
        return View();
    }

    [HttpPost]
    public IActionResult Venta(int idVenta, int idCliente)
    {
        // Autorizaciones
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToAction("Login", "Usuarios");
        if (HttpContext.Session.GetString("Rol") != "Cliente") return View("Unauthorized");

        try
        {
            ViewBag.venta = sistema.BuscarVentaPorId(idVenta);
            //Procesamos en sistema.
            sistema.ProcesarCompra(idCliente, idVenta, DateTime.Now);
            ViewBag.Exito = "Su compra se ha procesado exitosamente.";
            return View();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
        }
    }

    [HttpGet]
    public IActionResult Subasta(int id)
    {
        // Autorizaciones
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToAction("Login", "Usuarios");
        if (HttpContext.Session.GetString("Rol") != "Cliente") return View("Unauthorized");

        ViewBag.subasta = sistema.BuscarSubastaPorId(id);
        return View();
    }

    [HttpPost]
     public IActionResult Subasta(int idSubasta, int idCliente, double oferta)
    {
        // Autorizaciones
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToAction("Login", "Usuarios");
        if (HttpContext.Session.GetString("Rol") != "Cliente") return View("Unauthorized");

        try
        {
            ViewBag.subasta = sistema.BuscarSubastaPorId(idSubasta);
            //procesamos la oferta en sistema
            sistema.ProcesarOferta(idCliente, idSubasta, oferta, DateTime.Now);
            ViewBag.Exito = "Su oferta se ha procesado exitosamente.";
            return View();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
        }
    }
}