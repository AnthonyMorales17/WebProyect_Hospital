using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class EspecialidadDAL : ConexionDAL
    {
        public List<EspecialidadCLS> listarEspecialidades()
        {
            List<EspecialidadCLS> listarEspecialidades = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarEspecialidades", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            EspecialidadCLS oEspecialidadesCLS;
                            listarEspecialidades = new List<EspecialidadCLS>();

                            while (dr.Read())
                            {
                                oEspecialidadesCLS = new EspecialidadCLS();
                                oEspecialidadesCLS.idEspecialidad = dr.GetInt32(0);
                                oEspecialidadesCLS.nombre = dr.GetString(1);
                                listarEspecialidades.Add(oEspecialidadesCLS);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listarEspecialidades = null;
                    throw;
                }
            }
            return listarEspecialidades;
        }

        public EspecialidadCLS recuperarEspecialidad(int idEspecialidad)
        {
            EspecialidadCLS oEspecialidadCLS = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarEspecialidad", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idEspecialidad);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {

                            while (dr.Read())
                            {
                                oEspecialidadCLS = new EspecialidadCLS();
                                oEspecialidadCLS.idEspecialidad = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                oEspecialidadCLS.nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    oEspecialidadCLS = null;

                }
                return oEspecialidadCLS;
            }
        }

        public List<EspecialidadCLS> filtrarEspecialidad(EspecialidadCLS obj)
        {

            List<EspecialidadCLS> listarEspecialidades = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarEspecialidades", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", obj.nombre == null ? "" : obj.nombre);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            EspecialidadCLS oEspecialidadCLS;
                            listarEspecialidades = new List<EspecialidadCLS>();

                            int posNombre = dr.GetOrdinal("nombre");
                            int posDireccion = dr.GetOrdinal("direccion");

                            while (dr.Read())
                            {
                                oEspecialidadCLS = new EspecialidadCLS();
                                oEspecialidadCLS.idEspecialidad = dr.GetInt32(0);
                                oEspecialidadCLS.nombre = dr.GetString(1);


                                listarEspecialidades.Add(oEspecialidadCLS);
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    listarEspecialidades = null;
                    throw;
                }
            }

            return listarEspecialidades;
        }

        public int GuardarEspecialidad(EspecialidadCLS oEspecialidadCLS)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarEspecialidad", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", oEspecialidadCLS.idEspecialidad == 0 ? 0 : oEspecialidadCLS.idEspecialidad);
                        cmd.Parameters.AddWithValue("@Nombre", oEspecialidadCLS.nombre == null ? "" : oEspecialidadCLS.nombre);
                        bool esInsercion = oEspecialidadCLS.idEspecialidad == 0;
                        rpta = cmd.ExecuteNonQuery();
                        rpta = 1;
                    }
                }
                catch (Exception ex)
                {
                    cn.Close();
                    rpta = 0;
                }
            }
            return rpta;
        }

        public void eliminarEspecialidad(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarEspecialidad", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    throw;
                }

            }

        }
    }
}