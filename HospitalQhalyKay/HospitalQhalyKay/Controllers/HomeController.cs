using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HospitalQhalyKay.Permisos;
using System.Diagnostics;
using HospitalQhalyKay.Models;
using Microsoft.AspNetCore.Authorization;

namespace HospitalQhalyKay.Controllers
{
   // [Authorize]
      [ValidarSesion]
    public class HomeController : Controller

    {
        
        public IActionResult Index()
        {
            var usuarioCorreo = HttpContext.Session.GetString("Usuario");
            Console.WriteLine("🔎 Usuario en sesión al cargar Home: " + usuarioCorreo);
            ViewBag.UsuarioCorreo = usuarioCorreo;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            TempData["Mensaje"] = "Ha cerrado sesión exitosamente";
            return RedirectToAction("Login", "Acceso");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
