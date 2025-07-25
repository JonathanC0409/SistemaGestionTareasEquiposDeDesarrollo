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
    public class UsuariosController : ControllerBase
    {
        private DbConnection conexion;
        public UsuariosController(IConfiguration configuracion)
        {
            var cadenaConexion = configuracion.GetConnectionString("DefaultConection");
            conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
        }
        // GET: api/<UsuariosController>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            var usuarios = conexion.Query<Usuario>("SELECT * FROM Usuarios").ToList();
            return usuarios;
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            var usuario = conexion.QuerySingle<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new { Id = id });
            var proyectos = usuario.Proyectos = conexion.Query<Proyecto>("SELECT * FROM Proyectos WHERE UsuarioId = @UsuarioId", new { UsuarioId = usuario.Id }).ToList();
            foreach (var proyecto in proyectos)
            {
                proyecto.Tareas = conexion.Query<Tarea>("SELECT * FROM Tareas WHERE ProyectoId = @ProyectoId", new { ProyectoId = proyecto.Id }).ToList();
            }
            return usuario;
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public Usuario Post([FromBody] Usuario usuario)
        {
            {
                
}
            conexion.Execute("INSERT INTO Usuarios (Nombre, Apellido, Email, Contraseña) VALUES (@Nombre, @Apellido, @Email, @Contraseña)", usuario);
           
            return usuario;
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public Usuario Put(int id, [FromBody] Usuario usuario)
        {
            conexion.Execute("UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Contraseña = @Contraseña WHERE Id = @Id", new { usuario.Nombre, usuario.Apellido, usuario.Email, usuario.Contraseña, Id = id });
            return usuario;
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
        }
    }
}
