using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQhalyKay.Controllers
{
    public class MedicoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<MedicoCLS> listarMedicos()
        {
            MedicoBL obj = new MedicoBL();
            return obj.listarMedicos();
        }
        public List<MedicoCLS> filtrarMedico(MedicoCLS objMedico)
        {
            MedicoBL obj = new MedicoBL();
            return obj.filtrarMedico(objMedico);
        }
        public string GuardarMedico(MedicoCLS objMedico)
        {
            MedicoDAL obj = new MedicoDAL();
            int resultado = obj.GuardarMedico(objMedico);
            return resultado.ToString();
        }

        public MedicoCLS recuperarMedico(int idMedico)
        {
            MedicoBL obj = new MedicoBL();
            return obj.recuperarMedico(idMedico);
        }
        public void eliminarMedico(int idMedico)
        {
            MedicoBL obj = new MedicoBL();
            obj.eliminarMedico(idMedico);
        }
    }
}
