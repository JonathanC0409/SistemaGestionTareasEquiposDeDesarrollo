using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.ConsumeApi;
using SistemaGestionTareas.Modelos;

namespace SistemaGestionTareas.Presentacion.Controllers
{
    public class ProyectosController : Controller
    {
        [Authorize]
        // GET: ProyectosController
        public ActionResult Index()
        {
            var proyectos = CRUD<Proyecto>.GetAll();
            return View(proyectos);
        }

        // GET: ProyectosController/Details/5
        public ActionResult Details(int id)
        {
            var proyecto = CRUD<Proyecto>.GetById(id);
            return View(proyecto);
        }

        // GET: ProyectosController/Create
        public ActionResult Create()
        {
            ViewBag.Usuarios = GetUsuarios();
            return View();
        }
        private List<SelectListItem> GetUsuarios()
        {
            var usuarios = CRUD<Usuario>.GetAll();
            return usuarios.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.Nombre} {u.Apellido}"
            }).ToList();
        }
        // POST: ProyectosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proyecto proyecto)
        {
            try
            {
                CRUD<Proyecto>.Create(proyecto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProyectosController/Edit/5
        public ActionResult Edit(int id)
        {
            var proyecto = CRUD<Proyecto>.GetById(id);
            return View(proyecto);
        }

        // POST: ProyectosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Proyecto proyecto)
        {
            try
            {
                CRUD<Proyecto>.Update(id, proyecto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProyectosController/Delete/5
        public ActionResult Delete(int id)
        {
            var proyecto = CRUD<Proyecto>.GetById(id);
            return View(proyecto);
        }

        // POST: ProyectosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Proyecto proyecto)
        {
            try
            {
                CRUD<Proyecto>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
