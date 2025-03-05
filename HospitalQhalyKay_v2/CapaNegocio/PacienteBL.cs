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
            return obj.filtrarPaciente(objPaciente);
        }

        public string GuardarPaciente(PacienteCLS oPacienteCLS)
        {
            PacienteDAL obj = new PacienteDAL();
            int resultado = obj.GuardarPaciente(oPacienteCLS);
            return resultado.ToString();
        }
        public PacienteCLS recuperarPaciente(int iidpaciente)
        {
            PacienteDAL obj = new PacienteDAL();
            return obj.recuperarPaciente(iidpaciente);
        }

        public void eliminarPaciente(int id)
        {
            PacienteDAL obj = new PacienteDAL();
            obj.eliminarPaciente(id);
        }
    }
}
