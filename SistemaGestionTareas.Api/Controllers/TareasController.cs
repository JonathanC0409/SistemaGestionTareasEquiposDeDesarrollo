using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SistemaGestionTareas.Modelos;
using System.Data.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaGestionTareas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private DbConnection conexion;
        public TareasController(IConfiguration configuracion)
        {
            var cadenaConexion = configuracion.GetConnectionString("DefaultConection");
            conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
        }
        // GET: api/<TareasController>
        [HttpGet]
        public IEnumerable<Tarea> Get()
        {
            var tareas = conexion.Query<Tarea>("SELECT * FROM Tareas").ToList();
            foreach (var tarea in tareas)
            {
                tarea.Proyecto = conexion.QuerySingle<Proyecto>("SELECT * FROM Proyectos WHERE Id = @ProyectoId", new { ProyectoId = tarea.ProyectoId });
            }
            return tareas;

        }

        // GET api/<TareasController>/5
        [HttpGet("{id}")]
        public Tarea Get(int id)
        {
            var tarea = conexion.QuerySingle<Tarea>("SELECT * FROM Tareas WHERE Id = @Id", new { Id = id });
            return tarea;
        }

        // POST api/<TareasController>
        [HttpPost]
        public Tarea Post([FromBody] Tarea tarea)
        {
            conexion.Execute("INSERT INTO Tareas (Nombre, Descripcion, FechaVencimiento, Prioridad, Estado, ProyectoId) VALUES (@Nombre, @Descripcion, @FechaVencimiento, @Prioridad, @Estado, @ProyectoId)", tarea);
            return tarea;
        }

        // PUT api/<TareasController>/5
        [HttpPut("{id}")]
        public Tarea Put(int id, [FromBody] Tarea tarea)
        {
            conexion.Execute("UPDATE Tareas SET Nombre = @Nombre, Descripcion = @Descripcion, FechaVencimiento = @FechaVencimiento, Prioridad = @Prioridad, Estado = @Estado, ProyectoId = @ProyectoId WHERE Id = @Id", new { tarea.Nombre, tarea.Descripcion, tarea.FechaVencimiento, tarea.Prioridad, tarea.Estado, tarea.ProyectoId, Id = id });
            return tarea;
        }

        // DELETE api/<TareasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM Tareas WHERE Id = @Id", new { Id = id });
        }
    }
}
