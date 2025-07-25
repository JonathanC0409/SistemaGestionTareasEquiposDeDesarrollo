using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.ConsumeApi;
using SistemaGestionTareas.Modelos;
using SistemaGestionTareas.Presentacion.Models;
using System.Diagnostics;

namespace SistemaGestionTareas.Presentacion.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        

        [Authorize]
        [HttpPost]
        public IActionResult Busqueda(string name)
        {
            var proyectos = CRUD<Proyecto>.GetAll();
            var usuarios = CRUD<Usuario>.GetAll();
            foreach (var proyecto in proyectos)
            {
                if (proyecto.Nombre.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Details", "Proyectos", new { id = proyecto.Id });
                }
            }
            foreach (var usuario in usuarios)
            {
                if (usuario.Nombre.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Details", "Usuarios", new { id = usuario.Id });
                }
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
