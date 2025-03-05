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
            return obj.filtrarTratamiento(objTratamiento);
        }

        public string GuardarTratamiento(TratamientoCLS oTratamientoCLS)
        {
            TratamientoDAL obj = new TratamientoDAL();
            int resultado = obj.GuardarTratamiento(oTratamientoCLS);
            return resultado.ToString();
        }
        public TratamientoCLS recuperarTratamiento(int idTratamiento)
        {
            TratamientoDAL obj = new TratamientoDAL();
            return obj.recuperarTratamiento(idTratamiento);
        }

        public void eliminarTratamiento(int idTratamiento)
        {
            TratamientoDAL obj = new TratamientoDAL();
            obj.eliminarTratamiento(idTratamiento);
        }
    }
}
