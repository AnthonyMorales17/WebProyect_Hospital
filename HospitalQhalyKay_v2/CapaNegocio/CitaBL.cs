using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CitaBL
    {
        public List<CitaCLS> listarCitas()
        {
            CitaDAL obj = new CitaDAL();
            return obj.listarCitas();
        }
        public List<CitaCLS> filtrarCita(CitaCLS objCita)
        {
            CitaDAL obj = new CitaDAL();
            return obj.FiltrarCita(objCita);
        }

        public int GuardarCita(CitaCLS oCitaCLS)
        {
            CitaDAL obj = new CitaDAL();
            return obj.GuardarCita(oCitaCLS);
        }
        public CitaCLS recuperarCita(int iidcita)
        {
            CitaDAL obj = new CitaDAL();
            return obj.recuperarCita(iidcita);
        }

        public void EliminarCita(int id)
        {
            CitaDAL obj = new CitaDAL();
            obj.EliminarCita(id);
        }
    }
}
