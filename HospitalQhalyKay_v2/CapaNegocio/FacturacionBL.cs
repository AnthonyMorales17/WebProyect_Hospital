using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class FacturacionBL
    {
        public List<FacturacionCLS> listarFacturacion()
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.listarFacturacion();
        }
        public List<FacturacionCLS> filtrarFacturacion(FacturacionCLS objFacturacion)
        {
            FacturacionDAL obj = new FacturacionDAL();
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
            FacturacionDAL obj = new FacturacionDAL();
            return obj.recuperarFacturacion(idFacturacion);
        }

        public void eliminarFacturacion(int idFacturacion)
        {
            FacturacionDAL obj = new FacturacionDAL();
            obj.eliminarFacturacion(idFacturacion);
        }


    }
}
