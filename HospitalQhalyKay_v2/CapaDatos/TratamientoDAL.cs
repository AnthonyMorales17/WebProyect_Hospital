using System;
using System.Collections.Generic;
using System.Data;
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

        public TratamientoCLS recuperarTratamiento(int idTratamiento)
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

                        cmd.Parameters.AddWithValue("@Id", idTratamiento);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {

                            while (dr.Read())
                            {
                                oTratamientoCLS = new TratamientoCLS();
                                oTratamientoCLS.idTratamiento = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                oTratamientoCLS.idPaciente = dr.IsDBNull(1) ? 0 : dr.GetInt32(1); 
                                oTratamientoCLS.nombrePaciente = dr.IsDBNull(2) ? "" : dr.GetString(2);
                                oTratamientoCLS.descripcion = dr.IsDBNull(3) ? "" : dr.GetString(3);
                                oTratamientoCLS.fecha = dr.IsDBNull(4) ? DateOnly.MinValue : DateOnly.FromDateTime(dr.GetDateTime(4));
                                oTratamientoCLS.costo = dr.IsDBNull(5) ? 0 : (float)Math.Round(dr.GetDecimal(5), 2);
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

        public List<TratamientoCLS> filtrarTratamiento(TratamientoCLS obj)
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
                SqlTransaction transaction = null;
                try
                {
                    cn.Open();
                    transaction = cn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("uspGuardarTratamiento", cn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        int idTratamiento = Convert.ToInt32(oTratamientoCLS.idTratamiento);
                        int idPaciente = Convert.ToInt32(oTratamientoCLS.idPaciente);
                        decimal costo = Convert.ToDecimal(oTratamientoCLS.costo);
                        DateTime fecha = Convert.ToDateTime(oTratamientoCLS.fecha.ToDateTime(TimeOnly.MinValue));

                        cmd.Parameters.AddWithValue("@Id", idTratamiento);
                        cmd.Parameters.AddWithValue("@PacienteId", idPaciente);
                        cmd.Parameters.AddWithValue("@Descripcion", oTratamientoCLS.descripcion);
                        cmd.Parameters.AddWithValue("@Fecha", fecha);
                        cmd.Parameters.AddWithValue("@Costo", costo);

                        rpta = cmd.ExecuteNonQuery();

                        if (rpta > 0)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Rollback de la transacción en caso de error
                    transaction?.Rollback();
                    Console.WriteLine($"Error al guardar tratamiento: {ex.Message}");
                    Console.WriteLine($"Detalle de la excepción: {ex.ToString()}");
                    rpta = 0;
                }
                finally
                {
                    cn.Close();
                }
            }
            return rpta;
        }

        public void eliminarTratamiento(int idTratamiento)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarTratamiento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idTratamiento);
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
