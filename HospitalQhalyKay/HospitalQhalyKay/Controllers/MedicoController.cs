using CapaDatos;
using CapaEntidad;
using HospitalQhalyKay.Permisos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQhalyKay.Controllers
{
    //[Authorize]
    [ValidarSesion]
    public class MedicoController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        public List<MedicoCLS> listarMedicos()
        {
            MedicoDAL obj = new MedicoDAL();
            return obj.listarMedicos();
        }
        public List<MedicoCLS> filtrarMedico(MedicoCLS objMedico)
        {
            MedicoDAL obj = new MedicoDAL();
            return obj.FiltrarMedico(objMedico);
        }
        public int GuardarMedico(MedicoCLS oMedicoCLS)
        {
            MedicoDAL obj = new MedicoDAL();
            return obj.GuardarMedico(oMedicoCLS);
        }

        public MedicoCLS recuperarMedico(int iidmedico)
        {
            MedicoDAL obj = new MedicoDAL();
            return obj.recuperarMedico(iidmedico);
        }
        public void EliminarMedico(int id)
        {
            MedicoDAL obj = new MedicoDAL();
            obj.EliminarMedico(id);
        }
    }
}
