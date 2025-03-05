using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class MedicoBL
    {
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

        public string GuardarMedico(MedicoCLS oMedicoCLS)
        {
            MedicoDAL obj = new MedicoDAL();
            int resultado = obj.GuardarMedico(oMedicoCLS);
            return resultado.ToString();
        }
        public MedicoCLS recuperarMedico(int idMedico)
        {
            MedicoDAL obj = new MedicoDAL();
            return obj.recuperarMedico(idMedico);
        }

        public void eliminarMedico(int idMedico)
        {
            MedicoDAL obj = new MedicoDAL();
            obj.eliminarMedico(idMedico);
        }
    }
}
