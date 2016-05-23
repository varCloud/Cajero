using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Cmv.Utilerias
{
  public static class Correo
    {
        private static MailMessage mail;
        private static SmtpClient client;
        private static Attachment at;
        private static AlternateView htmlView;
        private static LinkedResource imagenFondo;
        private static LinkedResource imagenFirma;
        private static string DireccionCorreo;
        private static string Servidor;

        private static void IncializaCorreo(CorreoEnum tipoCorreo)
        {
            mail = new MailMessage();
            client = new SmtpClient();
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            if (tipoCorreo == CorreoEnum.INTERTNO)
            {
                client.UseDefaultCredentials = false; // se comenta cuando  es externo
                client.Credentials = new NetworkCredential();
            }
            else
            {
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["cuentaCorreo"].ToString(), ConfigurationManager.AppSettings["pass"].ToString());
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["cuentaCorreo"].ToString());
            client.Host = ConfigurationManager.AppSettings["servidor"].ToString();
        }

        public static void Notificacion(string mensaje, List<string> CC, List<string> TO, CorreoEnum tipoCorreo,string Asunto)
        {
            try
            {
                IncializaCorreo(tipoCorreo);
                AgregarTO(TO);
                AgregarCC(CC);
                mail.Body += Cabecera(Asunto);
                mail.Body += Cuerpo(mensaje);
                mail.Body += PiePagina();
                PintaImagenes();
                client.Send(mail);
                mail.Attachments.Clear();
                mail.Body = string.Empty;
                mail.To.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static void AgregarTO(List<string> TO)
        {
            foreach (string item in TO)
                mail.To.Add(item);

        }

        private static void AgregarCC(List<string> CC)
        {
            foreach (string item in CC)
                mail.CC.Add(item);

        }
        public static string Cabecera(string Asunto)
        {
            mail.Subject = Asunto;
            return "<html><body background='cid:imagenFondo'>";
        }

        private static string Cuerpo(string mensaje)
        {
            string cuerpo = "";

            cuerpo += @"<p><b> Seguimiento de Cajas </b> <br /><br />
                                    Presente.<br />
                                    &nbsp;&nbsp;&nbsp;Por este medio le informamos que :<br/>" + mensaje + @"<br/>
                                    Reciban un cordial saludo.<br/>
                                    Quedo a la orden
                                <br/>
                                <br/>
                                <i>Nota: este es un correo generado por una aplicacion web 
                                   </p>";
            //mail.Attachments.Add(new Attachment(Path.Combine(rutaReporte, admin.siniestro.Id_Siniestro + ".pdf"), System.Net.Mime.MediaTypeNames.Application.Pdf));
            return cuerpo;
        }

        private static String PiePagina()
        {
            StringBuilder pie = new StringBuilder();
            pie.Append("<br /><br /><div>");
            pie.Append("<IMG SRC='cid:imagenFirma' WIDTH=400 HEIGHT=70 ALT='TIMBRADO'>");
            pie.Append("<font face='Tahoma' SIZE=2  COLOR=gray><br/>  ACATITA DE BAJAN No. 222, COL. LOMAS DE HIDALGO <br />");
            pie.Append("TEL: (443) 3226-300, EXT. 2408, MORELIA, MICH.<br />");
            pie.Append("LADA SIN COSTO: 01 800 3000 268<br />");
            pie.Append("www.cajamorelia.com.mx</font>");
            pie.Append("</body>");
            pie.Append("</html>");
            return pie.ToString();
        }

        private static void PintaImagenes()
        {

            //BUSCAR LA MANERA DE SIMPLICAR COMO ADJUNTAR LAS IMAGENES EN EL CORREO  :( //
            //var appConfig = new ExeConfigurationFileMap { ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.config") };
            //valoresConfig = ConfigurationManager.OpenMappedExeConfiguration(appConfig, ConfigurationUserLevel.None);

            if (!File.Exists("fondoCorreo.png"))
                RecursoUtils.fondoCorreo.Save("fondoCorreo.png");
            if (!File.Exists("firma_timbres.png"))
                RecursoUtils.firma_correo.Save("firma_timbres.png");

            string attachmentPath = Path.Combine("firma_timbres.png");  //valoresConfig.AppSettings.Settings["imagenFirma"].Value.ToString();
            string contentID = Path.GetFileName(attachmentPath).Replace(".", "");
            Attachment imagenFirma = new Attachment(attachmentPath);
            imagenFirma.ContentDisposition.Inline = true;
            imagenFirma.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            imagenFirma.ContentId = contentID;
            imagenFirma.ContentType.MediaType = "image/png";
            imagenFirma.ContentType.Name = Path.GetFileName(attachmentPath);
            mail.Attachments.Add(imagenFirma);
            mail.Body = mail.Body.Replace("imagenFirma", contentID);

            attachmentPath = Path.Combine("fondoCorreo.png"); ;//valoresConfig.AppSettings.Settings["imagenFondo"].Value.ToString();
            contentID = Path.GetFileName(attachmentPath).Replace(".", "");
            Attachment imagenFondo = new Attachment(attachmentPath);
            imagenFondo.ContentDisposition.Inline = true;
            imagenFondo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            imagenFondo.ContentId = contentID;
            imagenFondo.ContentType.MediaType = "image/png";
            imagenFondo.ContentType.Name = Path.GetFileName(attachmentPath);
            mail.Attachments.Add(imagenFondo);
            mail.Body = mail.Body.Replace("imagenFondo", contentID);
        }
    }

  public enum CorreoEnum
  {
      INTERTNO = 1,
      EXTERNO = 2
  }
}
