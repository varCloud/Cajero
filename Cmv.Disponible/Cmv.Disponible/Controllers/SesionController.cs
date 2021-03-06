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
        Usuario usr_ = new Usuario();
        private Usuario SesionUsuario;

        bool valido = false;
        

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public JsonResult ValidaLogin(Usuario usr)
        {
            bool esUsuarioCorrecto = false;
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
                    SesionUsuario = SesionDAO.ObtenerInformacionUsuarioLogeado(usr);
                    //return RedirectToAction("Index", "Home");
                    Session["SesionUsuario"] = SesionUsuario;
                    esUsuarioCorrecto = true;
                    //return RedirectToAction("Inicio", "Inicio");

                    //valido = true;
                    //return Json(res, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.AddModelError("password", "Usuario / contraseña invalido");
                    esUsuarioCorrecto = false;
                   // return View("Login");

                }
            }
            else
            {
                esUsuarioCorrecto = false;
                //ModelState.AddModelError("loginIncorrecto", "Usuario/contraseña invalido");
                //return View("Login");
            }


            return Json(esUsuarioCorrecto);
            //if (valido)
            //{
            //    @Url.Action("Index", "Home", usr_);
            //}
        }


    }
}
