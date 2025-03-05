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
                                oMedicoCLS.Nombre = dr.GetString(1);
                                oMedicoCLS.Apellido = dr.GetString(2);
                                oMedicoCLS.EspecialidadId = dr.GetInt32(3);
                                oMedicoCLS.Telefono = dr.GetString(4);
                                oMedicoCLS.Email = dr.GetString(5);

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

        public MedicoCLS recuperarMedico(int iidmedico)
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

                        cmd.Parameters.AddWithValue("@Id", iidmedico);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {



                            while (dr.Read())
                            {
                                oMedicoCLS = new MedicoCLS();
                                oMedicoCLS.idMedico = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                oMedicoCLS.Nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                                oMedicoCLS.Apellido = dr.IsDBNull(2) ? "" : dr.GetString(2);
                                oMedicoCLS.EspecialidadId = dr.IsDBNull(3) ? 0 : dr.GetInt32(3);
                                oMedicoCLS.Telefono = dr.IsDBNull(4) ? "" : dr.GetString(4);
                                oMedicoCLS.Email = dr.IsDBNull(5) ? "" : dr.GetString(5);

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
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre == null ? "" : obj.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", obj.Apellido == null ? "" : obj.Apellido);
                        cmd.Parameters.AddWithValue("@Email", obj.Email == null ? "" : obj.Email);
                        cmd.Parameters.AddWithValue("@EspecialidadId", obj.EspecialidadId == 0 ? 0 : obj.EspecialidadId);
                        //   
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            MedicoCLS oMedicoCLS;
                            listarMedicos = new List<MedicoCLS>();


                            while (dr.Read())
                            {
                                oMedicoCLS = new MedicoCLS();
                                oMedicoCLS.idMedico = dr.GetInt32(0);
                                oMedicoCLS.Nombre = dr.GetString(1);
                                oMedicoCLS.Apellido = dr.GetString(2);
                                oMedicoCLS.EspecialidadId = dr.GetInt32(3);
                                oMedicoCLS.Telefono = dr.GetString(4);
                                oMedicoCLS.Email = dr.GetString(5);
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
                        cmd.Parameters.AddWithValue("@Nombre", string.IsNullOrEmpty(oMedicoCLS.Nombre) ? (object)DBNull.Value : oMedicoCLS.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", string.IsNullOrEmpty(oMedicoCLS.Apellido) ? (object)DBNull.Value : oMedicoCLS.Apellido);
                        cmd.Parameters.AddWithValue("@Apellido", string.IsNullOrEmpty(oMedicoCLS.Apellido) ? (object)DBNull.Value : oMedicoCLS.Apellido);
                        cmd.Parameters.AddWithValue("@EspecialidadId", oMedicoCLS.EspecialidadId == 0 ? 0 : oMedicoCLS.EspecialidadId);
                        cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(oMedicoCLS.Telefono) ? (object)DBNull.Value : oMedicoCLS.Telefono);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(oMedicoCLS.Email) ? (object)DBNull.Value : oMedicoCLS.Email);

                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    cn.Close();
                    throw new Exception("Error al guardar la especialidad: " + ex.Message);
                }
            }
            return rpta;
        }

        public void EliminarMedico(int id)

        {


            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarMedico", cn))
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
