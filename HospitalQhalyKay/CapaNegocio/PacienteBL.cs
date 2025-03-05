using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class TratamientoBL
    {
        public List<TratamientoCLS> listarTratamientos()
        {
            TratamientoDAL obj = new TratamientoDAL();
            return obj.listarTratamientos();
        }

        public List<TratamientoCLS> filtrarTratamiento(TratamientoCLS objTratamiento)
        {
            TratamientoDAL obj = new TratamientoDAL();
            return obj.FiltrarTratamiento(objTratamiento);
        }

        public int GuardarTratamiento(TratamientoCLS oTratamientoCLS)
        {
            TratamientoDAL obj = new TratamientoDAL();
            return obj.GuardarTratamiento(oTratamientoCLS);
        }
        public TratamientoCLS recuperarTratamiento(int iidtratamiento)
        {
            TratamientoDAL obj = new TratamientoDAL();
            return obj.recuperarTratamiento(iidtratamiento);
        }

        public void EliminarTratamiento(int id)
        {
            TratamientoDAL obj = new TratamientoDAL();
            obj.EliminarTratamiento(id);
        }

    }
}
