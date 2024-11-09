using Dominio;
using Microsoft.AspNetCore.Mvc;

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

        ViewBag.subastas = sistema.ListarSubastasFiltradas(nombre, estado);
        ViewBag.nombre = nombre;
        ViewBag.estado = estado;

        return View();
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

    [HttpGet]
    public IActionResult Subasta(int id)
    {
        // Autorizaciones
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToAction("Login", "Usuarios");
        if (HttpContext.Session.GetString("Rol") != "Cliente") return View("Unauthorized");

        ViewBag.subasta = sistema.BuscarSubastaPorId(id);
        return View();
    }
}