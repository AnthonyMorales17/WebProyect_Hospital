using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class MedicoCLS
    {
        public int idMedico { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int especialidadId { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}
