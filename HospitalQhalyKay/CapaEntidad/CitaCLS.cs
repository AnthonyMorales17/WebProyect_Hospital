using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CitaCLS
    {
        public int idCita { get; set; }
        public int idPaciente { get; set; }
        public string nombrePaciente { get; set; }
        public int idMedico { get; set; }
        public string nombreMedico { get; set; }
        public DateTime fechaHora { get; set; }
        public string estado { get; set; }

    }
}
