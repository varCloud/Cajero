using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cmv.Entidades
{
    public class Conexion
    {

        public string servidor { get; set; }
        public string baseDatos { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }

        public string ObtenerConexion()
        {
            return "Server=" + servidor + ";Database=" + baseDatos + ";User Id=" + usuario + ";Password=" + password;
        }
 

    }
}