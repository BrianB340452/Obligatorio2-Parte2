using Dominio;
using Microsoft.AspNetCore.Mvc;

public class PublicacionesController : Controller
{
    private Sistema sistema = Sistema.Instancia;

    [HttpGet]
    public IActionResult ListadoPublicaciones(string nombre, int estado)
    {
        ViewBag.publicaciones = sistema.ListarPublicacionesFiltradas(nombre, estado);
        ViewBag.nombre = nombre;
        ViewBag.estado = estado;

        return View();
    }

    [HttpGet]
    public IActionResult ListadoSubastas(string nombre, int estado)
    {
        ViewBag.subastas = sistema.ListarSubastasFiltradas(nombre, estado);
        ViewBag.nombre = nombre;
        ViewBag.estado = estado;

        return View();
    }

    [HttpGet]
    public IActionResult Venta(int id)
    {
        ViewBag.venta = sistema.BuscarVentaPorId(id);
        return View();
    }

    [HttpGet]
    public IActionResult Subasta(int id)
    {
        ViewBag.subasta = sistema.BuscarSubastaPorId(id);
        return View();
    }
}