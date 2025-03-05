﻿using CapaDatos;
using CapaEntidad;
using HospitalQhalyKay.Permisos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQhalyKay.Controllers
{
    //[Authorize]
    [ValidarSesion]
    public class CitaController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        public List<CitaCLS> listarCitas()
        {
            CitaDAL obj = new CitaDAL();
            return obj.listarCitas();
        }

        public List<CitaCLS> filtrarCita(CitaCLS objCita)
        {
            CitaDAL obj = new CitaDAL();
            return obj.FiltrarCita(objCita);
        }

        public int GuardarCita(CitaCLS oCitaCLS)
        {
            CitaDAL obj = new CitaDAL();
            return obj.GuardarCita(oCitaCLS);
        }
        public CitaCLS recuperarCita(int iidcita)
        {
            CitaDAL obj = new CitaDAL();
            return obj.recuperarCita(iidcita);
        }

        public void EliminarCita(int id)
        {
            CitaDAL obj = new CitaDAL();
            obj.EliminarCita(id);
        }
    }
}
