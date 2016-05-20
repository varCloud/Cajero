using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cmv.Entidades
{
    public class Usuario
    {
        public Usuario()
        {
            conexion = new Conexion();
        }
        public Conexion conexion { get; set; }

        public int idDepartemento { get; set; }

        
        /// <summary>
        /// Representa el numero de usuario
        /// </summary>
        [Required(ErrorMessage = "El numero de usuario es requerido")]
        public int noUsuario { get; set; }

        /// <summary>
        /// Identifica el usuario
        /// </summary>
        [Required(ErrorMessage = "El campo de usuario es requerido")]
        public string usuario { get; set; }

        /// <summary>
        /// Representa la contraseña del usuario
        /// </summary>
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string password { get; set; }
        /// <summary>
        /// Representa el rol del usuario
        /// </summary>
        public int idRol { get; set; }
        /// <summary>
        /// Representa la sucursal del usuario
        /// </summary>
        public int idSucursal { get; set; }
        //<summary>
        //Representa el correo del usuario
        /// </summary>
        public string correo { get; set; }
        /// <summary>
        /// Representra el nombre del usuario
        /// </summary>
        public string nombre { get; set; }
        /// <summary>
        /// Representa el apellido paterno del usuario
        /// </summary>
        public string apellidoPaterno { get; set; }
        /// <summary>
        /// Representa el apellido materno del usuario
        /// </summary>
        public string apellidoMaterno { get; set; }
        /// <summary>
        /// Obtiene o establece el rfc del usuario
        /// </summary>
        public String RFC { get; set; }
        /// <summary>
        /// Representa los permisos especiales de un usuario
        /// </summary>
        public string permisosEspeciales { get; set; }
        /// <summary>
        /// Representa la decripcion de un rol
        /// </summary>
        public string nombreRol { get; set; }

        public string usuarioRed { get; set; }

        /// <summary>
        /// Representa el puesto del usuario
        /// </summary>
        public string Puesto { get; set; }
    }
}