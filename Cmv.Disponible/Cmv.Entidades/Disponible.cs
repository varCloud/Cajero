using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmv.Entidades
{
  public  class Disponible
    {
      public Disponible()
      {
          enviarCorreo = false;
      }
        public string NumUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string DescSucursal { get; set; }
        public string EstatusCaja { get; set; }
        public decimal DisponibleCaja { get; set; }
        public decimal MaximoDisponible { get; set; }
        public decimal PorcenjeUsado { get; set; }
        public Codigo codigo { get; set; }
        public string correJefeSucursal { get; set; }
        public bool enviarCorreo { get; set; }
    }

  public enum Codigo
  { 
        verde =1,
        amarillo =2,
        rojo=3
  }
}
