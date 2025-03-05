using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class TratamientoCLS
    {
        public int idTratamiento { get; set; }
        public int idPaciente { get; set; }
        public string nombrePaciente { get; set; }
        public string descripcion { get; set; }
        public DateOnly fecha { get; set; }
        public float costo { get; set; }

    }
}
