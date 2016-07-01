using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmv.Entidades
{
   public class Notificacion
    {
        public int estatus { get; set; }
        public string mensaje { get; set; }
        public string titulo { get; set; }
        public string estiloNotificacion { get; set; }
        public string posicion { get; set; }
    }
}
