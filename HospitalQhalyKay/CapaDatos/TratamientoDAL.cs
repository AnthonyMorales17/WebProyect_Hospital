using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class TratamientoDAL : ConexionDAL
    {

        public List<TratamientoCLS> listarTratamientos()
        {
            List<TratamientoCLS> listarTratamientos = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarTratamientos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            TratamientoCLS oTratamientoCLS;
                            listarTratamientos = new List<TratamientoCLS>();

                            while (dr.Read())
                            {
                                oTratamientoCLS = new TratamientoCLS();
                                oTratamientoCLS.idTratamiento = dr.GetInt32(0);
                                oTratamientoCLS.idPaciente = dr.GetInt32(1);
                                oTratamientoCLS.nombrePaciente = dr.GetString(2);
                                oTratamientoCLS.descripcion = dr.GetString(3);
                                oTratamientoCLS.fecha = dr.IsDBNull(4) ? DateOnly.MinValue : DateOnly.FromDateTime(dr.GetDateTime(4));
                                oTratamientoCLS.costo = (float)Math.Round(dr.GetDecimal(5), 2);



                                listarTratamientos.Add(oTratamientoCLS);



                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listarTratamientos = null;
                    throw;
                }
            }
            return listarTratamientos;
        }

        public TratamientoCLS recuperarTratamiento(int iidtratamiento)
        {

            TratamientoCLS oTratamientoCLS = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarTratamiento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", iidtratamiento);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {



                            while (dr.Read())
                            {
                                oTratamientoCLS = new TratamientoCLS();
                                oTratamientoCLS.idTratamiento = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                oTratamientoCLS.idPaciente = dr.IsDBNull(0) ? 0 : dr.GetInt32(1);
                                oTratamientoCLS.nombrePaciente = dr.IsDBNull(0) ? "" : dr.GetString(2);
                                oTratamientoCLS.descripcion = dr.IsDBNull(0) ? "" : dr.GetString(3);
                                oTratamientoCLS.fecha = dr.IsDBNull(0) ? DateOnly.MinValue : DateOnly.FromDateTime(dr.GetDateTime(4));
                                oTratamientoCLS.costo = (float)Math.Round(dr.GetDecimal(5), 2);


                            }


                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    oTratamientoCLS = null;

                }
                return oTratamientoCLS;
            }
        }

        public List<TratamientoCLS> FiltrarTratamiento(TratamientoCLS obj)
        {

            List<TratamientoCLS> listarTratamientos = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarTratamientos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PacienteId", obj.idPaciente == 0 ? Convert.DBNull : obj.idPaciente);
                        cmd.Parameters.AddWithValue("@Fecha", obj.fecha == DateOnly.MinValue ? Convert.DBNull : obj.fecha);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            TratamientoCLS oTratamientoCLS;
                            listarTratamientos = new List<TratamientoCLS>();


                            while (dr.Read())
                            {
                                oTratamientoCLS = new TratamientoCLS();

                                oTratamientoCLS.idTratamiento = dr.GetInt32(0);
                                oTratamientoCLS.idPaciente = dr.GetInt32(1);
                                oTratamientoCLS.nombrePaciente = dr.GetString(2);
                                oTratamientoCLS.descripcion = dr.GetString(3);
                                oTratamientoCLS.fecha = dr.IsDBNull(4) ? DateOnly.MinValue : DateOnly.FromDateTime(dr.GetDateTime(4));
                                oTratamientoCLS.costo = (float)Math.Round(dr.GetDecimal(5), 2);
                                listarTratamientos.Add(oTratamientoCLS);
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    listarTratamientos = null;
                    throw;
                }
            }

            return listarTratamientos;
        }

        public int GuardarTratamiento(TratamientoCLS oTratamientoCLS)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarTratamiento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", oTratamientoCLS.idTratamiento == 0 ? 0 : oTratamientoCLS.idTratamiento);
                        cmd.Parameters.AddWithValue("@PacienteId", oTratamientoCLS.idPaciente == 0 ? 0 : oTratamientoCLS.idPaciente);
                        cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrEmpty(oTratamientoCLS.descripcion) ? (object)DBNull.Value : oTratamientoCLS.descripcion);
                        cmd.Parameters.AddWithValue("@Fecha", oTratamientoCLS.fecha == DateOnly.MinValue ? (object)DBNull.Value : oTratamientoCLS.fecha);
                        cmd.Parameters.AddWithValue("@Costo", oTratamientoCLS.costo == 0 ? 0 : oTratamientoCLS.costo);

                        rpta = cmd.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    cn.Close();
                    throw new Exception("Error al guardar tratamiento: " + ex.Message);
                }
            }
            return rpta;
        }

        public void EliminarTratamiento(int id)

        {


            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarTratamiento", cn))
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
