using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<MedicoCLS> filtrarMedico(MedicoCLS objEspecialidad)
        {
            MedicoDAL obj = new MedicoDAL();
            return obj.FiltrarMedico(objEspecialidad);
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

        public void EliminarEspecialidad(int id)
        {
            MedicoDAL obj = new MedicoDAL();
            obj.EliminarMedico(id);
        }
    }
}
