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
    public class ProyectosController : ControllerBase
    {
        private DbConnection conexion;
        public ProyectosController(IConfiguration configuracion)
        {
            var cadenaConexion = configuracion.GetConnectionString("DefaultConection");
            conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

        }
        // GET: api/<ProyectosController>
        [HttpGet]
        public IEnumerable<Proyecto> Get()
        {
            var proyectos = conexion.Query<Proyecto>("SELECT * FROM Proyectos").ToList();
            return proyectos;
        }

        // GET api/<ProyectosController>/5
        [HttpGet("{id}")]
        public Proyecto Get(int id)
        {
            var proyecto = conexion.QuerySingle<Proyecto>("SELECT * FROM Proyectos WHERE Id = @Id", new { Id = id });
            return proyecto;
        }

        // POST api/<ProyectosController>
        [HttpPost]
        public Proyecto Post([FromBody] Proyecto proyecto)
        {
            conexion.Execute("INSERT INTO Proyectos (Nombre, Completado, UsuarioId) VALUES (@Nombre, @Completado, @UsuarioId)", proyecto);
            return proyecto;
        }

        // PUT api/<ProyectosController>/5
        [HttpPut("{id}")]
        public Proyecto Put(int id, [FromBody] Proyecto proyecto)
        {
            conexion.Execute("UPDATE Proyectos SET Nombre = @Nombre, Completado = @Completado, UsuarioId = @UsuarioId WHERE Id = @Id", proyecto);
            return proyecto;
        }

        // DELETE api/<ProyectosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM Proyectos WHERE Id = @Id", new { Id = id });
        }
    }
}
