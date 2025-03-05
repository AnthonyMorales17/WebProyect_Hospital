using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQhalyKay.Controllers
{
    public class TratamientoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<TratamientoCLS> listarTratamientos()
        {
            TratamientoBL obj = new TratamientoBL();
            return obj.listarTratamientos();
        }
        public List<TratamientoCLS> filtrarTratamiento(TratamientoCLS objTratamiento)
        {
            TratamientoBL obj = new TratamientoBL();
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
            TratamientoBL obj = new TratamientoBL();
            return obj.recuperarTratamiento(idTratamiento);
        }

        public void eliminarTratamiento(int idTratamiento)
        {
            TratamientoBL obj = new TratamientoBL();
            obj.eliminarTratamiento(idTratamiento);
        }
    }
}
