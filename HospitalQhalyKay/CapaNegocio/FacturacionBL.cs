using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
