using CapaDatos;
using CapaEntidad;
using HospitalQhalyKay.Permisos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQhalyKay.Controllers
{
    //[Authorize]
    [ValidarSesion]
    public class FacturacionController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public List<FacturacionCLS> listarFacturacion()
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.listarFacturacion();
        }
        public List<FacturacionCLS> filtrarFacturacion(FacturacionCLS objFacturacion)
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.FiltrarFacturacion(objFacturacion);
        }

        public int GuardarFacturacion(FacturacionCLS oFacturacionCLS)
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.GuardarFacturacion(oFacturacionCLS);
        }
        public FacturacionCLS recuperarFacturacion(int iidfacturacion)
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.recuperarFacturacion(iidfacturacion);
        }

        public void EliminarFacturacion(int id)
        {
            FacturacionDAL obj = new FacturacionDAL();
            obj.EliminarFacturacion(id);
        }
    }
}
