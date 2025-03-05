using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CitaDAL : ConexionDAL
    {
        public List<CitaCLS> listarCitas()
        {
            List<CitaCLS> listarCitas = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarCitas", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            CitaCLS oCitaCLS;
                            listarCitas = new List<CitaCLS>();

                            while (dr.Read())
                            {
                                oCitaCLS = new CitaCLS();
                                oCitaCLS.idCita = dr.GetInt32(0);
                                oCitaCLS.idPaciente = dr.GetInt32(1);

                                oCitaCLS.nombrePaciente = dr.GetString(2);
                                oCitaCLS.idMedico = dr.GetInt32(3);
                                oCitaCLS.nombreMedico = dr.GetString(4);
                                oCitaCLS.fechaHora = dr.GetDateTime(5);
                                oCitaCLS.estado = dr.GetString(6);



                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listarCitas = null;
                    throw;
                }
            }
            return listarCitas;
        }

        public CitaCLS recuperarCita(int iidcita)
        {

            CitaCLS oCitaCLS = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarCita", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", iidcita);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {



                            while (dr.Read())
                            {
                                oCitaCLS = new CitaCLS();
                                oCitaCLS.idCita = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                oCitaCLS.idPaciente = dr.IsDBNull(1) ? 0 : dr.GetInt32(1);
                                oCitaCLS.nombrePaciente = dr.IsDBNull(2) ? "" : dr.GetString(2);
                                oCitaCLS.idMedico = dr.IsDBNull(3) ? 0 : dr.GetInt32(3);
                                oCitaCLS.nombreMedico = dr.IsDBNull(4) ? "" : dr.GetString(4);
                                oCitaCLS.fechaHora = dr.IsDBNull(5) ? DateTime.MinValue : dr.GetDateTime(5);
                                oCitaCLS.estado = dr.IsDBNull(6) ? "" : dr.GetString(6);

                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    oCitaCLS = null;

                }
                return oCitaCLS;
            }
        }

        public List<CitaCLS> FiltrarCita(CitaCLS obj)
        {

            List<CitaCLS> listarCitas = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarCitas", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PacienteId", obj.idPaciente == null ? "" : obj.idPaciente);
                        cmd.Parameters.AddWithValue("@MedicoId", obj.idMedico == null ? "" : obj.idMedico);
                        cmd.Parameters.AddWithValue("@FechaHora", obj.fechaHora == null ? DateTime.MinValue : obj.fechaHora);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            CitaCLS oCitaCLS;
                            listarCitas = new List<CitaCLS>();


                            while (dr.Read())
                            {
                                oCitaCLS = new CitaCLS();

                                oCitaCLS.idCita = dr.GetInt32(0);
                                oCitaCLS.idPaciente = dr.GetInt32(1);
                                oCitaCLS.nombrePaciente = dr.GetString(2);
                                oCitaCLS.idMedico = dr.GetInt32(3);
                                oCitaCLS.nombreMedico = dr.GetString(4);
                                oCitaCLS.fechaHora = dr.GetDateTime(5);
                                oCitaCLS.estado = dr.GetString(6);

                                listarCitas.Add(oCitaCLS);

                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    listarCitas = null;
                    throw;
                }
            }

            return listarCitas;
        }

        public int GuardarCita(CitaCLS oCitaCLS)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarCita", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", oCitaCLS.idCita == 0 ? 0 : oCitaCLS.idCita);
                        cmd.Parameters.AddWithValue("@PacienteId", oCitaCLS.idPaciente == 0 ? 0 : oCitaCLS.idPaciente);
                        cmd.Parameters.AddWithValue("@MedicoId", oCitaCLS.idMedico == 0 ? 0 : oCitaCLS.idMedico);
                        cmd.Parameters.AddWithValue("@FechaHora", oCitaCLS.fechaHora == DateTime.MinValue ? DateTime.MinValue : oCitaCLS.fechaHora);
                        cmd.Parameters.AddWithValue("@Estado", oCitaCLS.estado == null ? "" : oCitaCLS.estado);


                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    cn.Close();
                    throw new Exception("Error al guardar la cita: " + ex.Message);
                }
            }
            return rpta;
        }

        public void EliminarCita(int id)

        {


            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarCita", cn))
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
