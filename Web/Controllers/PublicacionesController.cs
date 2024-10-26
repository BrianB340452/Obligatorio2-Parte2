using Dominio;
using Microsoft.AspNetCore.Mvc;

public class PublicacionesController : Controller
{
    private Sistema sistema = Sistema.Instancia;

    public IActionResult Index()
    {
        List<Publicacion> publicaciones = sistema.Publicaciones;
        return View(publicaciones);
    }

    public PartialViewResult BuscarPublicaciones(string filtro, int estado)
    {
        List<Publicacion> publicaciones = sistema.ListarPublicacionesFiltradas(filtro, estado);
        return PartialView("TablaListado", publicaciones);
    }
}
