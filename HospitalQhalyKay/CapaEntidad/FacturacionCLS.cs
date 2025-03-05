using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class FacturacionCLS
    {
        public int idFacturacion { get; set; }
        public int idPaciente { get; set; }
        public string nombrePaciente { get; set; }
        public float monto { get; set; }
        public string metodoPago { get; set; }
        public DateOnly fechaPago { get; set; }


    }
}
