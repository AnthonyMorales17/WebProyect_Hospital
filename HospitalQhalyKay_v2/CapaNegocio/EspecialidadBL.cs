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

        public string GuardarEspecialidad(EspecialidadCLS oEspecialidadCLS)
        {
            EspecialidadDAL obj = new EspecialidadDAL();
            int resultado = obj.GuardarEspecialidad(oEspecialidadCLS);
            return resultado.ToString();
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
