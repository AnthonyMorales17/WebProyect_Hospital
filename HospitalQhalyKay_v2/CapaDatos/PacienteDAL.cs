﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class PacienteDAL : ConexionDAL
    {
        public List<PacienteCLS> listarPacientes()
        {
            List<PacienteCLS> listarPacientes = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarPacientes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            PacienteCLS oPacienteCLS;
                            listarPacientes = new List<PacienteCLS>();

                            while (dr.Read())
                            {
                                oPacienteCLS = new PacienteCLS();
                                oPacienteCLS.idPaciente = dr.GetInt32(0);
                                oPacienteCLS.nombre = dr.GetString(1);
                                oPacienteCLS.apellido = dr.GetString(2);
                                oPacienteCLS.fechaNacimiento = dr.IsDBNull(3) ? DateOnly.MinValue : DateOnly.FromDateTime(dr.GetDateTime(3));
                                oPacienteCLS.telefono = dr.GetString(4);
                                oPacienteCLS.email = dr.GetString(5);
                                oPacienteCLS.direccion = dr.GetString(6);
                                listarPacientes.Add(oPacienteCLS);  
                            }


                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listarPacientes = null;
                    throw;
                }
            }
            return listarPacientes;
        }


        public PacienteCLS recuperarPaciente(int idPaciente)
        {

            PacienteCLS oPacienteCLS = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarPaciente", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idPaciente);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                oPacienteCLS = new PacienteCLS();
                                oPacienteCLS.idPaciente = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                oPacienteCLS.nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                                oPacienteCLS.apellido = dr.IsDBNull(2) ? "" : dr.GetString(2);
                                oPacienteCLS.fechaNacimiento = dr.IsDBNull(3) ? DateOnly.MinValue : DateOnly.FromDateTime(dr.GetDateTime(3));
                                oPacienteCLS.telefono = dr.IsDBNull(4) ? "" : dr.GetString(4);
                                oPacienteCLS.email = dr.IsDBNull(5) ? "" : dr.GetString(5);
                                oPacienteCLS.direccion = dr.IsDBNull(6) ? "" : dr.GetString(6);
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    oPacienteCLS = null;

                }
                return oPacienteCLS;
            }
        }

        public List<PacienteCLS> filtrarPaciente(PacienteCLS obj)
        {

            List<PacienteCLS> listarPacientes = null;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarPacientes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", obj.idPaciente == 0 ? 0 : obj.idPaciente);
                        cmd.Parameters.AddWithValue("@Nombre", obj.nombre == null ? "" : obj.nombre);
                        cmd.Parameters.AddWithValue("@Apellido", obj.apellido == null ? "" : obj.apellido);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", obj.fechaNacimiento == DateOnly.MinValue ? DateOnly.MinValue : obj.fechaNacimiento);
                        cmd.Parameters.AddWithValue("@Email", obj.email == null ? "" : obj.email);
                        SqlDataReader dr = cmd.ExecuteReader();



                        if (dr != null)
                        {
                            PacienteCLS oPacienteCLS;
                            listarPacientes = new List<PacienteCLS>();



                            while (dr.Read())
                            {
                                oPacienteCLS = new PacienteCLS();
                                oPacienteCLS.idPaciente = dr.GetInt32(0);
                                oPacienteCLS.nombre = dr.GetString(1);
                                oPacienteCLS.apellido = dr.GetString(2);
                                oPacienteCLS.fechaNacimiento = dr.IsDBNull(3) ? DateOnly.MinValue : DateOnly.FromDateTime(dr.GetDateTime(3));
                                oPacienteCLS.email = dr.GetString(4);
                                oPacienteCLS.direccion = dr.GetString(5);

                                listarPacientes.Add(oPacienteCLS);



                            }
                        }
                    }

                }
                catch (Exception)
                {
                    cn.Close();
                    listarPacientes = null;
                    throw;
                }
            }

            return listarPacientes;
        }


        public int GuardarPaciente(PacienteCLS oPacienteCLS)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarPaciente", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", oPacienteCLS.idPaciente == 0 ? 0 : oPacienteCLS.idPaciente);
                        cmd.Parameters.AddWithValue("@Nombre", string.IsNullOrEmpty(oPacienteCLS.nombre) ? (object)DBNull.Value : oPacienteCLS.nombre);
                        cmd.Parameters.AddWithValue("@Apellido", string.IsNullOrEmpty(oPacienteCLS.apellido) ? (object)DBNull.Value : oPacienteCLS.apellido);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", oPacienteCLS.fechaNacimiento == DateOnly.MinValue ? (object)DBNull.Value : oPacienteCLS.fechaNacimiento.ToDateTime(TimeOnly.MinValue));
                        cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(oPacienteCLS.telefono) ? (object)DBNull.Value : oPacienteCLS.telefono);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(oPacienteCLS.email) ? (object)DBNull.Value : oPacienteCLS.email);
                        cmd.Parameters.AddWithValue("@Direccion", string.IsNullOrEmpty(oPacienteCLS.direccion) ? (object)DBNull.Value : oPacienteCLS.direccion);
                        bool esInsercion = oPacienteCLS.idPaciente == 0;
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

        public void eliminarPaciente(int idPaciente)

        {


            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarPaciente", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idPaciente);


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
