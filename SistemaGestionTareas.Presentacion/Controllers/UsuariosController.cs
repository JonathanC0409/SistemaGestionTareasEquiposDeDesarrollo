using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.ConsumeApi;
using SistemaGestionTareas.Modelos;

namespace SistemaGestionTareas.Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        [Authorize]
        // GET: UsuariosController
        public ActionResult Index()
        {
            var usuarios = CRUD<Usuario>.GetAll();
            return View(usuarios);
        }

        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
            var usuario = CRUD<Usuario>.GetById(id);
            return View(usuario);
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                CRUD<Usuario>.Create(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = CRUD<Usuario>.GetById(id);
            return View(usuario);
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario usuario)
        {
            try
            {
                CRUD<Usuario>.Update(id, usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            var usuario = CRUD<Usuario>.GetById(id);
            return View(usuario);
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Usuario usuario)
        {
            try
            {
                CRUD<Usuario>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
