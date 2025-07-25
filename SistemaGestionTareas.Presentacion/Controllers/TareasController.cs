using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.ConsumeApi;
using SistemaGestionTareas.Modelos;

namespace SistemaGestionTareas.Presentacion.Controllers
{
    public class TareasController : Controller
    {
        [Authorize]
        // GET: TareasController
        public ActionResult Index()
        {
            var tareas = CRUD<Tarea>.GetAll();
            return View(tareas);
        }

        // GET: TareasController/Details/5
        public ActionResult Details(int id)
        {
            var tarea = CRUD<Tarea>.GetById(id);
            return View(tarea);
        }

        // GET: TareasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarea tarea)
        {
            try
            {
                CRUD<Tarea>.Create(tarea);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TareasController/Edit/5
        public ActionResult Edit(int id)
        {
            var tarea = CRUD<Tarea>.GetById(id);
            return View(tarea);
        }

        // POST: TareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tarea tarea)
        {
            try
            {
                CRUD<Tarea>.Update(id, tarea);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TareasController/Delete/5
        public ActionResult Delete(int id)
        {
            var tarea = CRUD<Tarea>.GetById(id);
            return View(tarea);
        }

        // POST: TareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tarea tarea)
        {
            try
            {
                CRUD<Tarea>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult Reporte(string orden)
        {
            var tareasLINQ = CRUD<Tarea>.GetAll().AsQueryable();

            tareasLINQ = orden switch
            {
                "prioridad" => tareasLINQ.OrderBy(t => t.Prioridad),
                "fecha" => tareasLINQ.OrderBy(t => t.FechaVencimiento),
                "estado" => tareasLINQ.OrderBy(t => t.Estado),
                _ => tareasLINQ.OrderBy(t => t.Id)
            };

            if(orden == null)
            {
                var tareas = CRUD<Tarea>.GetAll();
                return View(tareas);

            }

            return View(tareasLINQ.ToList());
        }
    }
}
