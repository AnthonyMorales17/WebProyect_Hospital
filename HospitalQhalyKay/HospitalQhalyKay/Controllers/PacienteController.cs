using CapaDatos;
using CapaEntidad;
using HospitalQhalyKay.Permisos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQhalyKay.Controllers
{
    // [Authorize]
    [ValidarSesion]
    public class PacienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<PacienteCLS> listarPacientes()
        {
            PacienteDAL obj = new PacienteDAL();
            return obj.listarPacientes();
        }

        public List<PacienteCLS> filtrarPaciente(PacienteCLS objPaciente)
        {
            PacienteDAL obj = new PacienteDAL();
            return obj.FiltrarPaciente(objPaciente);
        }

        public int GuardarPaciente(PacienteCLS oPacienteCLS)
        {
            PacienteDAL obj = new PacienteDAL();
            return obj.GuardarPaciente(oPacienteCLS);
        }
        public PacienteCLS recuperarPaciente(int iidpaciente)
        {
            PacienteDAL obj = new PacienteDAL();
            return obj.recuperarPaciente(iidpaciente);
        }

        public void EliminarPaciente(int id)
        {
            PacienteDAL obj = new PacienteDAL();
            obj.EliminarPaciente(id);
        }
    }
}
