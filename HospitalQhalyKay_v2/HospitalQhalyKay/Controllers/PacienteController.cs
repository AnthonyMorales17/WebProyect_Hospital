using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQhalyKay.Controllers
{
    public class PacienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<PacienteCLS> listarPacientes()
        {
            PacienteBL obj = new PacienteBL();
            return obj.listarPacientes();
        }

        public PacienteCLS recuperarPaciente(int idPaciente)
        {
            PacienteBL obj = new PacienteBL();
            return obj.recuperarPaciente(idPaciente);
        }

        public List<PacienteCLS> filtrarPaciente(PacienteCLS obj)
        {
            PacienteBL objPaciente = new PacienteBL();
            return objPaciente.filtrarPaciente(obj);
        }

        public string GuardarPaciente(PacienteCLS oPacienteCLS)
        {
            PacienteDAL obj = new PacienteDAL();
            int resultado = obj.GuardarPaciente(oPacienteCLS);
            return resultado.ToString();
        }

        public void eliminarPaciente(int idPaciente)
        {
            PacienteBL obj = new PacienteBL();
            obj.eliminarPaciente(idPaciente);
        }
    }
}
