using Cmv.Entidades;
using Cmv.Utilerias;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cmv.Disponible.Controllers
{
    public class HomeController : Controller
    {
        private Cmv.Disponible.DAO.DisponibleDAO dispDAO = null;
        public ActionResult Index(Usuario s)
        {
            dispDAO = new DAO.DisponibleDAO();
            Cajas caja = new Cajas();
            caja = dispDAO.ConsultaDisponibleCajero();
            return View(caja);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult Enviar(string items)
        {  
            string msj = "Te informamos que la o las siguientes cajas <br/>@  cuentan con efectivo mayor al  permito";
            try
            {
                //List<Cmv.Entidades.Disponible> lstCajas = JsonConvert.DeserializeObject<List<Cmv.Entidades.Disponible>>(items);
                //var  lstCajasAux=  lstCajas.GroupBy(p => p.DescSucursal).ToList();
                //foreach (var item in lstCajasAux)
                //{
                //    foreach (var a in item)
                //    {
                //        if (lstCajas.Exists(p => p.DescSucursal == a.DescSucursal))
                //        {
                //            var lstCorreoCajas = lstCajas.FindAll(p => p.DescSucursal.ToString().Equals(a.DescSucursal));
                //            if (lstCorreoCajas != null)
                //            {
                //                string cajeros = string.Empty;
                //                string correoJefe = lstCorreoCajas[0].correJefeSucursal;
                //                foreach (var usuarios in lstCorreoCajas)
                //                    cajeros += usuarios.NombreUsuario + "<br/>";
                //                Correo.Notificacion(msj.Replace("@", cajeros).ToString(), new List<string>(){"victor.reyez@cmv.mx"}, new List<string>() /*{ a.correJefeSucursal}*/, CorreoEnum.INTERTNO, "Disponibilidad en Cajas");
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            return Json(true);
        }
    }
}
