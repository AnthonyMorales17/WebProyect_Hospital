using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class PacienteBL
    {
        public List<PacienteCLS> listarPacientes()
        {
            PacienteDAL obj = new PacienteDAL();
            return obj.listarPacientes();
        }
        public List<PacienteCLS> filtrarPaciente(PacienteCLS objPaciente)
        {
            PacienteDAL obj = new PacienteDAL();
            return obj.FiltrarPaciente(objPaciente);
        }

        public int GuardarPaciente(PacienteCLS oPacienteCLS)
        {
            PacienteDAL obj = new PacienteDAL();
            return obj.GuardarPaciente(oPacienteCLS);
        }
        public PacienteCLS recuperarPaciente(int iidpaciente)
        {
            PacienteDAL obj = new PacienteDAL();
            return obj.recuperarPaciente(iidpaciente);
        }

        public void EliminarPaciente(int id)
        {
            PacienteDAL obj = new PacienteDAL();
            obj.EliminarPaciente(id);
        }
    }
}
