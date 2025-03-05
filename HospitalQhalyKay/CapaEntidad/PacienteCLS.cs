using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class PacienteCLS
    {
        public int idPaciente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateOnly fechaNacimiento { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
    }
}
