using Dominio;
using Microsoft.AspNetCore.Mvc;

public class PublicacionesController : Controller
{
    private Sistema sistema = Sistema.Instancia;

    public IActionResult Index()
    {
        return View();
    }

    public PartialViewResult Buscar(string nombre, int estado)
    {
        ViewBag.publicaciones = sistema.ListarPublicacionesFiltradas(nombre, estado);
        return PartialView();
    }
}
