using AccesoDatos;
using Cmv.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmv.Disponible.DAO
{
   public class DisponibleDAO
    {
       private DBManager db;
       private string conexion = string.Empty;
       public DisponibleDAO(string conexion)
       {
           this.conexion = conexion;
               //@"Server=cmv8008\proyecto2;Database=HAPE;User Id=sa_temp;Password=Abcde1";
            
       }
        //verde =1,
        //amarillo =2,
        //rojo=3
       public Cajas ConsultaDisponibleCajero()
       {
           List<Cmv.Entidades.Disponible> lstDis = new List<Cmv.Entidades.Disponible>();
           Cajas c = new Cajas();
           DBManager db = null;
           try
           {
               db = new DBManager(this.conexion);
               db.Open();
               db.CreateParameters(2);
               db.AddParameters(0, "@tipo_consulta", 1);
               db.AddParameters(1, "@cerrada", 0);
               db.ExecuteReader(CommandType.StoredProcedure, "SP_CMV_CONSULTA_DISPONIBLE_CAJERO");
               c.lstCajas1 = new List<Entidades.Disponible>();
               c.lstCajas2 = new List<Entidades.Disponible>();
               c.lstCajas3 = new List<Entidades.Disponible>();
               Cmv.Entidades.Disponible d;
                while (db.DataReader.Read())
                {
                   
                    d = new Entidades.Disponible();
                    d.DescSucursal = db.DataReader["sucursal"].ToString();
                    d.NumUsuario = db.DataReader["numusuario"].ToString();
                    d.NombreUsuario = db.DataReader["nombre_usuario"].ToString();
                    d.DisponibleCaja = Convert.ToDecimal(db.DataReader["disponible_caja"].ToString());
                    d.MaximoDisponible = Convert.ToDecimal(db.DataReader["maximo_disponible"].ToString());
                    d.PorcenjeUsado = Convert.ToDecimal(db.DataReader["porcentaje_usado"].ToString());
                    d.correJefeSucursal = db.DataReader["correo_jefe_sucursal"].ToString();
                    if ((Codigo)Convert.ToInt16(db.DataReader["codigo"]) == Codigo.verde)
                    {
                        d.codigo =Codigo.verde;
                        c.lstCajas1.Add(d);
                    }
                    else if (((Codigo)Convert.ToInt16(db.DataReader["codigo"]) == Codigo.amarillo))
                    {
                        d.codigo = Codigo.amarillo;
                        c.lstCajas2.Add(d);
                    }
                    else if (((Codigo)Convert.ToInt16(db.DataReader["codigo"]) == Codigo.rojo))
                    {
                        d.codigo = Codigo.amarillo;
                        c.lstCajas3.Add(d);
                    }
 
                }
           }
           catch (Exception ex)
           {
               throw new Exception("en la funcion : ConsultaDisponibleCajero ", ex);
           }
           finally
           {
               db.Dispose();

           }
           return c;
       }
    }
}
