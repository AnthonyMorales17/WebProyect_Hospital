using CapaDatos;
using CapaEntidad;
using HospitalQhalyKay.Permisos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace HospitalQhalyKay.Controllers
{
    // [Authorize]
    [ValidarSesion]
    public class EspecialidadController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public List<EspecialidadCLS> listarEspecialidades()
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            return obj.listarEspecialidades();
        }

        public List<EspecialidadCLS> filtrarEspecialidad(EspecialidadCLS objEspecialidad)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            return obj.filtrarEspecialidad(objEspecialidad);
        }

        public EspecialidadCLS recuperarEspecialidad(int idEspecialidad)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            return obj.recuperarEspecialidad(idEspecialidad);
        }

        public int GuardarEspecialidad(EspecialidadCLS oEspecialidadCLS)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            return obj.GuardarEspecialidad(oEspecialidadCLS);
        }

        public void eliminarEspecialidad(int idEspecialidad)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            obj.eliminarEspecialidad(idEspecialidad);
        }


    }
}
