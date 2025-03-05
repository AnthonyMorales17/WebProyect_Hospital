using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class EspecialidadesBL
    {
        public List<EspecialidadCLS> listarEspecialidades()
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            return obj.listarEspecialidades();
        }

        public List<EspecialidadCLS> filtrarEspecialidad(EspecialidadCLS objEspecialidad)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            return obj.filtrarEspecialidad(objEspecialidad);
        }

        public int GuardarEspecialidad(EspecialidadCLS oEspecialidadCLS)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            return obj.GuardarEspecialidad(oEspecialidadCLS);
        }

        public EspecialidadCLS recuperarEspecialidad(int idEspecialidad)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            return obj.recuperarEspecialidad(idEspecialidad);
        }

        public void eliminarEspecialidad(int idEspecialidad)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            obj.eliminarEspecialidad(idEspecialidad);
        }

    }

}
