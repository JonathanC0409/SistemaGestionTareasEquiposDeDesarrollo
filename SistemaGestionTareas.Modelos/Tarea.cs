using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTareas.Modelos
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Prioridad { get; set; } 
        public string Estado { get; set; } 
        //Claves foraneas
        public int ProyectoId { get; set; } 
        //Navegadores
        public Proyecto? Proyecto { get; set; }
    }
}
