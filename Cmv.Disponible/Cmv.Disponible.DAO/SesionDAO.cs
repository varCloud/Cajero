using AccesoDatos;
using Cmv.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmv.Disponible.DAO
{
    public class SesionDAO
    {

    public string ContraseñaEncriptada { get; set; }
        public string Instancia { get; set; }
        public string BaseDatos { get; set; }
        public string SP_CLAVES_VALIDA_CONTRASENA;
        public string SP_CMV_OBTENER_DATOS_USUARIO { get; set; }
        private string stringConection = string.Empty;
        private Dictionary<string, object> InfoUsuario { get; set; }
        private string connectionString = string.Empty;

        public SesionDAO()
        {
            try
            {
                this.connectionString = ConfigurationManager.ConnectionStrings["connectionForLogin"].ConnectionString;
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                Instancia = builder.DataSource;
                BaseDatos = builder.InitialCatalog;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            #region SP_DATOS_USUARIO
            this.SP_CMV_OBTENER_DATOS_USUARIO = @"	SELECT UPPER(A.USUARIO)USUARIO,UPPER(A.NOMBRE_S)NOMBRE,UPPER(A.APELLIDO_PATERNO)AP,UPPER(A.APELLIDO_MATERNO)AM,
		                                            A.NUMEMP,A.ID_DE_SUCURSAL,UPPER(B.DESCRIPCION)SUCURSAL,B.REGION, UPPER(C.DESCRIPCION)AREA,UPPER(D.NOM_DEPARTAMENTO)DEPARTAMENTO,
		                                            UPPER(A.CORREO)CORREO,UPPER(E.NOM_ROL)  PUESTO, A.ID_ROL
	                                            FROM CLAVES A JOIN SUCURSALES B ON (A.ID_DE_SUCURSAL=B.ID_DE_SUCURSAL)
		                                            INNER JOIN AREAS C ON A.ID_AREA=C.ID_AREA
		                                            INNER JOIN SICORP_DPTOS D ON A.ID_DPTO=D.ID_DPTO
		                                            INNER JOIN SICORP_ROLES E ON A.ID_ROL=E.ID_ROL
	                                            WHERE A.NUMUSUARIO=@#numUsuario ";
            #endregion;
            #region SP_CLAVES
            this.SP_CLAVES_VALIDA_CONTRASENA = @"
            declare @contrasena_claves varchar(40), @status int
            select @contrasena_claves = contrasena from claves where numusuario = @#numusuario

            if @@error <> 0

	            begin

	            select @status = 0
	            print 'Error al leer claves'
	            goto exit_proc

	            end

            if @contrasena_claves = @#contrasena

	            select @status = 1

            else

	            select @status = 0

            exit_proc:

            select @status ESTATUS";
            #endregion
        }

        /// <summary>
            /// valida si un usuario tiene accesos a la base de datos especificada, para un mejor funcionamiento verifica si existe el SP con el nombre SP_CLAVES_VALIDA_CONTRASENA
            /// si no existe el SP, no te preocupes se ejecutara una query independiente,pero si verifica que exista la tabla con el nombre CLAVES si no existe pues ahora si preocupate :)
            /// </summary>
            /// <param name="instancia">El servidor de base de datos al que te deseas conectar ejem cmv8001\fira</param>
            /// <param name="baseDatos">base datos a la que te deseas conectar ejem HAPE</param>
            /// <returns>Retorna true si tiene acceso a la base de datos,false si no tiene acceso a la base de datos</returns>
        public bool ValidaUsuario(Usuario usuario)
            {
                try
                {
                    uint usr__ = Convert.ToUInt32(usuario.noUsuario);


                    ContraseñaEncriptada = EncrypSHA1.EnciptaSHA1(usr__.ToString(), usuario.password.ToString()).ToUpper().Substring(0, 30);
                    

                    //stringConection = "Server =" + Instancia + "; Database =" + BaseDatos + "; User Id =" + usuario.usuario + " ;Password =" + ContraseñaEncriptada + "";
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con.Open();
                        }
                        catch (Exception ex)
                        {
                            return false;
                        }
                        try
                        {
                            string textoCmd = "exec SP_CLAVES_VALIDA_CONTRASENA " + usuario.noUsuario.ToString() + ", '" + ContraseñaEncriptada + "'";
                            SqlCommand cmd = new SqlCommand(textoCmd, con);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                return Convert.ToBoolean(reader["ESTATUS"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                if (ex.Message.ToString().Contains("Could not find stored procedure"))
                                {
                                    this.SP_CLAVES_VALIDA_CONTRASENA = this.SP_CLAVES_VALIDA_CONTRASENA.Replace("@#numusuario", usuario.noUsuario.ToString()).Replace("@#contrasena", "'" + ContraseñaEncriptada + "'");
                                    SqlCommand cmd = new SqlCommand(this.SP_CLAVES_VALIDA_CONTRASENA, con);
                                    SqlDataReader reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        return Convert.ToBoolean(reader["ESTATUS"]);
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            catch (Exception exs)
                            {
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;

            }


        /// <summary>
        /// obtiene la informacion basica del usuario logeado,para un mejor funcionamiento verifica que existe el SP con el nombre SP_CMV_OBTENER_DATOS_USUARIO
        /// retorna numUsuario,usuario,contraseña,idRol,sucursal,correo,nombre,ap,am,numEmp,idSucursal,stringConection,contraseñaEncriptada
        /// </summary>
        /// <returns></returns>
        public Usuario ObtenerInformacionUsuarioLogeado(Usuario usuario)
        {
            Usuario usuario_ = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_CMV_OBTENER_DATOS_USUARIO " + usuario.noUsuario , con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    usuario_= llenarUsuario(reader);
                }
                catch (Exception ex)
                {
                    try
                    {
                        if (ex.Message.ToString().Contains("Could not find stored procedure") || ex.Message.ToString().Contains("No se encontró el procedimiento almacenado"))
                        {
                            this.SP_CMV_OBTENER_DATOS_USUARIO = this.SP_CMV_OBTENER_DATOS_USUARIO.Replace("@#numUsuario", usuario.noUsuario.ToString());
                            SqlCommand cmd = new SqlCommand(this.SP_CMV_OBTENER_DATOS_USUARIO, con);
                            SqlDataReader reader = cmd.ExecuteReader();
                            usuario_ = llenarUsuario(reader);
                        }
                        else if (ex.Message.ToString().Contains(""))
                        {

                        }

                    }
                    catch (Exception exs)
                    {

                        throw exs;
                    }

                }
            }
            return usuario_;
        }

        public Usuario llenarUsuario(SqlDataReader reader)
        {
            Usuario usuario = null;
            while (reader.Read())
            {
                usuario = new Usuario();
                //usuario.noUsuario = Convert.ToInt32(reader["NUMUSUARIO"].ToString());
                usuario.usuario = reader["USUARIO"].ToString();
                //usuario.password = reader["CONTRASENA"].ToString();
                usuario.idRol = Convert.ToInt32(reader["ID_ROL"].ToString());
                usuario.idSucursal = Convert.ToInt32(reader["ID_DE_SUCURSAL"].ToString());
                usuario.correo = reader["CORREO"].ToString();
                usuario.nombre = reader["NOMBRE"].ToString();
                usuario.apellidoPaterno = reader["AP"].ToString();
                usuario.apellidoMaterno = reader["AM"].ToString();
                usuario.conexion.servidor = Instancia;
                usuario.conexion.baseDatos = BaseDatos;
                usuario.conexion.password = ContraseñaEncriptada;
                usuario.conexion.usuario = usuario.usuario;
            }
            return usuario;

        }


        
    }
}
