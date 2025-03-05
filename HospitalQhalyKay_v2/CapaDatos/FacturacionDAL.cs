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
    public class FacturacionDAL : ConexionDAL
    {
        public FacturacionCLS recuperarFacturacion(int iidfacturacion)
        {

            FacturacionCLS oFacturacionCLS = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarFacturacion", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", iidfacturacion);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {

                            while (dr.Read())
                            {
                                oFacturacionCLS = new FacturacionCLS();
                                oFacturacionCLS.idFacturacion = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                oFacturacionCLS.idPaciente = dr.IsDBNull(1) ? 0 : dr.GetInt32(1);
                                oFacturacionCLS.nombrePaciente = dr.IsDBNull(2) ? "" : dr.GetString(2);
                                oFacturacionCLS.monto = dr.IsDBNull(3) ? 0 : Convert.ToSingle(dr.GetDecimal(3)); // Corrección aquí
                                oFacturacionCLS.metodoPago = dr.IsDBNull(4) ? "" : dr.GetString(4);
                                oFacturacionCLS.fechaPago = dr.IsDBNull(5) ? DateOnly.MinValue : DateOnly.FromDateTime(dr.GetDateTime(5));
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    oFacturacionCLS = null;

                }
                return oFacturacionCLS;
            }
        }

        public List<FacturacionCLS> filtrarFacturacion(FacturacionCLS obj)
        {

            List<FacturacionCLS> listarFacturacion = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarFacturaciones", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PacienteId", obj.idPaciente == 0 ? (object)DBNull.Value : obj.idPaciente);
                        cmd.Parameters.AddWithValue("@FechaPago", obj.fechaPago == DateOnly.MinValue ? (object)DBNull.Value : obj.fechaPago);


                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            FacturacionCLS oFacturacionCLS;
                            listarFacturacion = new List<FacturacionCLS>();



                            while (dr.Read())
                            {
                                oFacturacionCLS = new FacturacionCLS();
                                oFacturacionCLS.idFacturacion = dr.GetInt32(0);
                                oFacturacionCLS.idPaciente = dr.GetInt32(1);
                                oFacturacionCLS.nombrePaciente = dr.GetString(2);
                                oFacturacionCLS.monto = (float)Math.Round(dr.GetDecimal(3), 2);
                                oFacturacionCLS.metodoPago = dr.GetString(4);
                                oFacturacionCLS.fechaPago = DateOnly.FromDateTime(dr.GetDateTime(5));
                                listarFacturacion.Add(oFacturacionCLS);
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    listarFacturacion = null;
                    throw;
                }
            }

            return listarFacturacion;
        }

        public List<FacturacionCLS> listarFacturacion()
        {
            List<FacturacionCLS> listarFacturacion = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarFacturaciones", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            FacturacionCLS oFacturacionCLS;
                            listarFacturacion = new List<FacturacionCLS>();

                            while (dr.Read())
                            {
                                oFacturacionCLS = new FacturacionCLS();
                                oFacturacionCLS.idFacturacion = dr.GetInt32(0);
                                oFacturacionCLS.idPaciente = dr.GetInt32(1);
                                oFacturacionCLS.nombrePaciente = dr.GetString(2);
                                oFacturacionCLS.monto = (float)Math.Round(dr.GetDecimal(3), 2);
                                oFacturacionCLS.metodoPago = dr.GetString(4);
                                oFacturacionCLS.fechaPago = DateOnly.FromDateTime(dr.GetDateTime(5));
                                listarFacturacion.Add(oFacturacionCLS);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listarFacturacion = null;
                    throw;
                }
            }
            return listarFacturacion;
        }

        public int GuardarFacturacion(FacturacionCLS oFacturacionCLS)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlTransaction transaction = null;
                try
                {
                    cn.Open();
                    transaction = cn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("uspGuardarFacturacion", cn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        int idFacturacion = Convert.ToInt32(oFacturacionCLS.idFacturacion);
                        int idPaciente = Convert.ToInt32(oFacturacionCLS.idPaciente);
                        decimal monto = Convert.ToDecimal(oFacturacionCLS.monto);
                        DateTime fechaPago = Convert.ToDateTime(oFacturacionCLS.fechaPago);

                        // Validaciones
                        if (idPaciente <= 0)
                        {
                            throw new ArgumentException("ID de Paciente inválido");
                        }

                        if (monto <= 0)
                        {
                            throw new ArgumentException("Monto debe ser mayor a cero");
                        }

                        if (string.IsNullOrWhiteSpace(oFacturacionCLS.metodoPago))
                        {
                            throw new ArgumentException("Método de Pago no puede estar vacío");
                        }

                        cmd.Parameters.AddWithValue("@Id", idFacturacion);
                        cmd.Parameters.AddWithValue("@PacienteId", idPaciente);
                        cmd.Parameters.AddWithValue("@Monto", monto);
                        cmd.Parameters.AddWithValue("@MetodoPago", oFacturacionCLS.metodoPago);
                        cmd.Parameters.AddWithValue("@FechaPago", fechaPago);

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
                    transaction?.Rollback();

                    Console.WriteLine($"Error al guardar facturación: {ex.Message}");
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

        public void eliminarFacturacion(int idFacturacion)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarFacturacion", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idFacturacion);


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
