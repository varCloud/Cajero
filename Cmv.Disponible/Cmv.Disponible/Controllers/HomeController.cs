using Cmv.Entidades;
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
        public ActionResult Index()
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
    }
}
