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
    public class CajerosController : Controller
    {
        private Cmv.Disponible.DAO.DisponibleDAO dispDAO = null;
        private Notificacion notificacion = null;
        private Usuario sesionActual;
        public ActionResult Cajeros(Usuario s)
        {
            
            this.sesionActual = (Usuario) this.Session["SesionUsuario"];
            dispDAO = new DAO.DisponibleDAO(this.sesionActual.conexion.ObtenerConexion());
            Cajas caja = new Cajas();
            caja = dispDAO.ConsultaDisponibleCajero();
            return View(caja);
        }

        public JsonResult EnviarCorreoProximas(List<Cmv.Entidades.Disponible> lstCajas2)
        {
            CrearNotificacion();
            try
            {
                if (lstCajas2.Count > 0)
                {

                    string msj = string.Empty;
                    var groupedCustomerList = lstCajas2
                        .Where(p => p.enviarCorreo)
                        .GroupBy(u => u.DescSucursal)
                        .ToList();

                    if (groupedCustomerList.Count > 0)
                    {
                        foreach (var item in groupedCustomerList)
                        {
                            msj = "La o las siguientes cajas estan por exceder el efectivo permitido";
                            List<string> nombresExcedidos = lstCajas2.Where(p => p.DescSucursal == item.Key && p.enviarCorreo == true).Select(p => p.NombreUsuario).ToList<string>();
                            foreach (string nombre in nombresExcedidos)
                            {
                                msj += "<br/>• " + nombre;
                            }
                            Cmv.Entidades.Disponible cajaActual = (Cmv.Entidades.Disponible)item.ElementAt(0);
                            Correo.Notificacion(msj, null, new List<string>() { cajaActual.correJefeSucursal }, CorreoEnum.INTERTNO, "Efectivo en Cajas");
                            
                        }
                        notificacion.mensaje = "Envio de Correo Exitso";
                        notificacion.estiloNotificacion = EstiloNotificacion.success.ToString();
                    }
                    else
                    {
                        notificacion.estiloNotificacion = EstiloNotificacion.info.ToString();
                        notificacion.mensaje = "Debe seleccionar almenos un registros";
                    }
                }
            }
            catch (Exception ex)
            {
                notificacion.mensaje = ex.Message;
                return Json(notificacion);
            }

            return Json(notificacion);
        }

        public JsonResult EnviarCorreoExcedidas(List<Cmv.Entidades.Disponible> lstCajas3)
        {
            CrearNotificacion();
            try
            {
                if (lstCajas3.Count > 0)
                {

                    string msj = string.Empty;
                    
                    var groupedCustomerList = lstCajas3
                        .Where(p => p.enviarCorreo)
                        .GroupBy(u => u.DescSucursal)
                        .ToList();

                    if (groupedCustomerList.Count > 0)
                    {
                        foreach (var item in groupedCustomerList)
                        {
                            msj = "La o las siguientes cajas cuentan con efectivo mayor al permitido";
                            List<string> nombresExcedidos = lstCajas3.Where(p => p.DescSucursal == item.Key && p.enviarCorreo == true).Select(p => p.NombreUsuario).ToList<string>();
                            foreach (string nombre in nombresExcedidos)
                            {
                                msj += "<br/>• " + nombre;
                            }
                            Cmv.Entidades.Disponible cajaActual = (Cmv.Entidades.Disponible)item.ElementAt(0);
                            Correo.Notificacion(msj, null, new List<string>() { cajaActual.correJefeSucursal }, CorreoEnum.INTERTNO, "Efectivo en Cajas");
                        }
                        notificacion.mensaje = "Envio de Correo Exitso";
                        notificacion.estiloNotificacion = EstiloNotificacion.success.ToString();
                    }
                    else
                    {
                        notificacion.estiloNotificacion = EstiloNotificacion.info.ToString();
                        notificacion.mensaje = "Debe seleccionar almenos un registros";
                    }
                }
            }
            catch (Exception ex)
            {
                notificacion.mensaje = ex.Message;
                return Json(notificacion);
            }

            return Json(notificacion);
        }

        public void CrearNotificacion()
        {
            notificacion = new Notificacion();
            notificacion.mensaje = "Error al enviar correos";
            notificacion.titulo = "Mensaje";
            notificacion.estiloNotificacion = EstiloNotificacion.error.ToString();
            notificacion.posicion = "top right";
 
        }
    }
}
