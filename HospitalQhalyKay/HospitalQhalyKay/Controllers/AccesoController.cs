using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HospitalQhalyKay.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace HospitalQhalyKay.Controllers
{

    public class AccesoController : Controller
    {
        static string connectionString = "Server=DESKTOP-VBHBEII\\SQLEXPRESS; Database=HospitalDB; User ID=sa; Password=admin; TrustServerCertificate=true";
        
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario oUsuario)
        {
            if (oUsuario.Clave != oUsuario.ConfirmarClave)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View(oUsuario);
            }

            bool registrado;
            string mensaje;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                    cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
                    cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    registrado = false;
                    mensaje = "Error al registrar: " + ex.Message;
                }
            }

            ViewData["Mensaje"] = mensaje;
            return registrado ? RedirectToAction("Login", "Acceso") : View(oUsuario);
        }

        [HttpPost]
        public IActionResult Login(Usuario oUsuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                    cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);

                    connection.Open();
                    var result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        oUsuario.idUsuario = Convert.ToInt32(result);
                    }
                    else
                    {
                        oUsuario.idUsuario = 0;
                    }
                }
                catch (Exception ex)
                {
                    ViewData["Mensaje"] = "Error de inicio de sesión: " + ex.Message;
                    return View(oUsuario);
                }
            }

            if (oUsuario.idUsuario != 0)
            {
                HttpContext.Session.SetInt32("UsuarioId", oUsuario.idUsuario);
                HttpContext.Session.SetString("Usuario", oUsuario.Correo);

                Console.WriteLine("✅ Sesión guardada en Login: " + HttpContext.Session.GetString("Usuario"));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario o contraseña incorrecta";
                return View(oUsuario);
            }
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            TempData["Mensaje"] = "Ha cerrado sesión exitosamente";
            return RedirectToAction("Login", "Acceso");
        }
    }
}
