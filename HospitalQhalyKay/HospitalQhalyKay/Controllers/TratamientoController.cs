using CapaDatos;
using CapaEntidad;
using HospitalQhalyKay.Permisos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQhalyKay.Controllers
{
    // [Authorize]
    [ValidarSesion]
    public class TratamientoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

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
