using Dominio;
using Microsoft.AspNetCore.Mvc;

public class PublicacionesController : Controller
{
    private Sistema sistema = Sistema.Instancia;

    public IActionResult Index(string nombre, int estado)
    {
        ViewBag.publicaciones = sistema.ListarPublicacionesFiltradas(nombre, estado);
        ViewBag.nombre = nombre;
        ViewBag.estado = estado;

        return View();
    }

    public IActionResult Venta(int id)
    {
        ViewBag.venta = sistema.BuscarVentaPorId(id);
        return View();
    }

    public IActionResult Subasta(int id)
    {
        ViewBag.subasta = sistema.BuscarSubastaPorId(id);
        return View();
    }
}