using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTareas.Modelos
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Completado { get; set; }
        //Claves foraneas
        public int UsuarioId { get; set; }

        //Navegadores
        public List<Tarea>? Tareas { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
