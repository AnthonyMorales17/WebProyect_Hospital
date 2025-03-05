using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HospitalQhalyKay.Permisos
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var usuarioSesion = httpContext.Session.GetString("Usuario");

            Console.WriteLine(" Validando sesión: " + usuarioSesion);

            if (string.IsNullOrEmpty(usuarioSesion)) // Si no hay usuario en sesión
            {
                Console.WriteLine(" No hay sesión activa, redirigiendo a Login...");
                filterContext.Result = new RedirectToActionResult("Login", "Acceso", null);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
