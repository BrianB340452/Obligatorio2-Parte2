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

    [HttpPost]
    public IActionResult Venta(int idVenta, int idCliente, double oferta)
    {
        // Autorizaciones
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToAction("Login", "Usuarios");
        if (HttpContext.Session.GetString("Rol") != "Cliente") return View("Unauthorized");

        try
        {
            ViewBag.venta = sistema.BuscarVentaPorId(idVenta);
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