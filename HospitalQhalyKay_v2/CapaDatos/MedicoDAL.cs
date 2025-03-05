using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class MedicoDAL : ConexionDAL
    {
        public List<MedicoCLS> listarMedicos()
        {
            List<MedicoCLS> listarMedicos = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarMedicos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            MedicoCLS oMedicoCLS;
                            listarMedicos = new List<MedicoCLS>();

                            while (dr.Read())
                            {
                                oMedicoCLS = new MedicoCLS();
                                oMedicoCLS.idMedico = dr.GetInt32(0);
                                oMedicoCLS.nombre = dr.GetString(1);
                                oMedicoCLS.apellido = dr.GetString(2);
                                oMedicoCLS.especialidadId = dr.GetInt32(3);
                                oMedicoCLS.telefono = dr.GetString(4);
                                oMedicoCLS.email = dr.GetString(5);

                                listarMedicos.Add(oMedicoCLS);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listarMedicos = null;
                    throw;
                }
            }
            return listarMedicos;
        }

        public MedicoCLS recuperarMedico(int idMedico)
        {

            MedicoCLS oMedicoCLS = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarMedico", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idMedico);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                oMedicoCLS = new MedicoCLS();
                                oMedicoCLS.idMedico = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                oMedicoCLS.nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                                oMedicoCLS.apellido = dr.IsDBNull(2) ? "" : dr.GetString(2);
                                oMedicoCLS.especialidadId = dr.IsDBNull(3) ? 0 : dr.GetInt32(3);
                                oMedicoCLS.telefono = dr.IsDBNull(4) ? "" : dr.GetString(4);
                                oMedicoCLS.email = dr.IsDBNull(5) ? "" : dr.GetString(5);

                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    oMedicoCLS = null;

                }
                return oMedicoCLS;
            }
        }

        public List<MedicoCLS> FiltrarMedico(MedicoCLS obj)
        {

            List<MedicoCLS> listarMedicos = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarMedicos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", obj.idMedico == 0 ? 0 : obj.idMedico);
                        cmd.Parameters.AddWithValue("@Nombre", obj.nombre == null ? "" : obj.nombre);
                        cmd.Parameters.AddWithValue("@Apellido", obj.apellido == null ? "" : obj.apellido);
                        cmd.Parameters.AddWithValue("@Email", obj.email == null ? "" : obj.email);
                        cmd.Parameters.AddWithValue("@EspecialidadId", obj.especialidadId == 0 ? 0 : obj.especialidadId);
                           
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            MedicoCLS oMedicoCLS;
                            listarMedicos = new List<MedicoCLS>();


                            while (dr.Read())
                            {
                                oMedicoCLS = new MedicoCLS();
                                oMedicoCLS.idMedico = dr.GetInt32(0);
                                oMedicoCLS.nombre = dr.GetString(1);
                                oMedicoCLS.apellido = dr.GetString(2);
                                oMedicoCLS.especialidadId = dr.GetInt32(3);
                                oMedicoCLS.telefono = dr.GetString(4);
                                oMedicoCLS.email = dr.GetString(5);
                                listarMedicos.Add(oMedicoCLS);
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    listarMedicos = null;
                    throw;
                }
            }

            return listarMedicos;
        }

        public int GuardarMedico(MedicoCLS oMedicoCLS)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarMedico", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", oMedicoCLS.idMedico == 0 ? 0 : oMedicoCLS.idMedico);
                        cmd.Parameters.AddWithValue("@Nombre", oMedicoCLS.nombre == null ? "" : oMedicoCLS.nombre);
                        cmd.Parameters.AddWithValue("@Apellido", oMedicoCLS.apellido == null ? "" : oMedicoCLS.apellido);
                        cmd.Parameters.AddWithValue("@EspecialidadId", oMedicoCLS.especialidadId == 0 ? 0 : oMedicoCLS.especialidadId);
                        cmd.Parameters.AddWithValue("@Telefono", oMedicoCLS.telefono == null ? "" : oMedicoCLS.telefono);
                        cmd.Parameters.AddWithValue("@Email", oMedicoCLS.email == null ? "" : oMedicoCLS.email);

                        bool esInsercion = oMedicoCLS.idMedico == 0;
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

        public void eliminarMedico(int idMedico)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarMedico", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idMedico);
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
