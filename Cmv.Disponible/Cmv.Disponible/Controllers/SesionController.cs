﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cmv.Entidades;
using Cmv.Disponible;
using Cmv.Disponible.Models;

namespace Cmv.Disponible.Controllers
{
    public class SesionController : Controller
    {
        //
        // GET: /Sesion/

        private Cmv.Disponible.DAO.SesionDAO SesionDAO = null;
        private Usuario usuario;


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ValidaLogin(Usuario usr)
        {
            ModelState.Remove("conexion");
            ModelState.Remove("idDepartemento");
            ModelState.Remove("idRol");
            ModelState.Remove("idSucursal");
            ModelState.Remove("correo");
            ModelState.Remove("nombre");
            ModelState.Remove("apellidoPaterno");
            ModelState.Remove("apellidoMaterno");
            ModelState.Remove("RFC");
            ModelState.Remove("permisosEspeciales");
            ModelState.Remove("nombreRol");
            ModelState.Remove("usuarioRed");
            ModelState.Remove("Puesto");

            if (ModelState.IsValid)
            {
                SesionDAO = new Cmv.Disponible.DAO.SesionDAO();

                //Usuario res = SesionDAO.ValidaUsuario(usuario);
                if (SesionDAO.ValidaUsuario(usr))
                {
                    Usuario usr_ = new Usuario();
                    usr_ = SesionDAO.ObtenerInformacionUsuarioLogeado(usr);
                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "Home", usr_);

                    //return Json(res, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View("Login");

                }
            }
            else
            {
                ModelState.AddModelError("loginIncorrecto", "Usuario/contraseña invalido");
                return View("MostrarLogin");
            }
        }


    }
}