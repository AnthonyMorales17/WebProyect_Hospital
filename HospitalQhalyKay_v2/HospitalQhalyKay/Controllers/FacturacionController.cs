using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQhalyKay.Controllers
{
    public class FacturacionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public List<FacturacionCLS> listarFacturacion()
        {
            FacturacionBL obj = new FacturacionBL();
            return obj.listarFacturacion();
        }
        public List<FacturacionCLS> filtrarFacturacion(FacturacionCLS objFacturacion)
        {
            FacturacionBL obj = new FacturacionBL();
            return obj.filtrarFacturacion(objFacturacion);
        }

        public string GuardarFacturacion(FacturacionCLS oFacturacionCLS)
        {
            FacturacionDAL obj = new FacturacionDAL();
            int resultado = obj.GuardarFacturacion(oFacturacionCLS);
            return resultado.ToString();
        }
        public FacturacionCLS recuperarFacturacion(int idFacturacion)
        {
            FacturacionBL obj = new FacturacionBL();
            return obj.recuperarFacturacion(idFacturacion);
        }

        public void EliminarFacturacion(int idFacturacion)
        {
            FacturacionBL obj = new FacturacionBL();
            obj.eliminarFacturacion(idFacturacion);
        }
    }
}
