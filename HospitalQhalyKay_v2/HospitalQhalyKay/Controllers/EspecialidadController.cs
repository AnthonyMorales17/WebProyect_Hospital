using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;



namespace HospitalQhalyKay.Controllers
{
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

        public string GuardarEspecialidad(EspecialidadCLS oEspecialidadCLS)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            int resultado = obj.GuardarEspecialidad(oEspecialidadCLS);
            return resultado.ToString();
        }

        public void eliminarEspecialidad(int idEspecialidad)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            obj.eliminarEspecialidad(idEspecialidad);
        }


    }
}
