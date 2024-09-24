using System;
using System.Collections.Generic;
using System.Text;
using ENTIDAD;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class daoUsuarios
    {

        public static Usuarios BuscarUsuario(string legajo)                                                                            // metodo para buscar el usuario.
        {
            try
            {
                Usuarios obj = new Usuarios();                                                                                          // declaramos un objeto de usuarios

                Conexion cn = new Conexion();                                                                                           // creamos un objeto conexion
                MySqlConnection conexion = cn.conectar();                                                                               // guardamos en conexion la ruta de la bd.
                conexion.Open();                                                                                                        // abrimos la conexion a la base
                MySqlCommand cmd = new MySqlCommand("select * from usuarios where legajo = '" + legajo + "'", conexion);                      // enviamos la consulta a la base de datos junto con la conexion y guardamos en cmd el resultado
                MySqlDataReader rd = cmd.ExecuteReader();                                                                               // en el objeto rd guardamos el de resultado de cmd

                if (rd.Read())                                                                                                          // nos preguntamos si la lectura de rd es verdadera
                {

                    obj.Nombre = rd.GetString(1);
                    obj.Importe = rd.GetInt32(2);
                    obj.Offset = rd.GetString(3);
                    obj.legajo = rd.GetString(0);
                    conexion.Close();
                    cmd.Connection.Close();

                    return obj;                                                                                                         // devolvemos el objeto lleno.
                }
                else
                {
                    obj = null;                                                                                                       // si no es verdadero, obj lo igualamos a null
                    conexion.Close();
                    cmd.Connection.Close();
                    return obj;                                                                                                         // devolvemos el obj vacio.
                }
            }
            catch (Exception ex)
            {
                // Qué ha sucedido
                var mensaje = "Error message: " + ex.Message;
                // Información sobre la excepción interna
                if (ex.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + ex.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + ex.StackTrace;
                return null;
            }

        }


        public static int ModificarOffset(string legajo)                                                                               // metodo para modificar el offset.
        {
            int Filas = 0;
            Conexion cn = new Conexion();
            MySqlConnection conexion = cn.conectar();
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("update usuarios set offset = '0000', fecha = @fecha where legajo = '" + legajo + "'", conexion);

            try
            {
                cmd.Parameters.Add("@fecha", MySqlDbType.DateTime).Value = DateTime.Now;
               
                Filas = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Filas = 0;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return Filas;

            
        }


        public static string BuscarOffset(string legajo)                                                                               // metodo para buscar el offset.
        {
            string off;                                                                                                             // variable string para almacenar el offset.

            Conexion cn = new Conexion();                                                                                           // creamos un objeto conexion cn.
            MySqlConnection conexion = cn.conectar();                                                                               // guardamos la conexion en la variable conexion llamando a la funcion conectar de la clase cn
            conexion.Open();                                                                                                        // abrimos la conexion
            MySqlCommand cmd = new MySqlCommand("select offset from usuarios where legajo ='"+legajo+"'", conexion);                      // guardamos la consulta y la conexion en cmd.
            MySqlDataReader rd = cmd.ExecuteReader();                                                                               // en el objeto rd guardamos el de resultado de cmd

            if (rd.Read())                                                                                                          // nos preguntamos si la lectura de rd es verdadera
            {
                off = rd.GetString(0);                                                                                              // guardamos lo leido en la variable off. La posicion es 0 porque es lo unico que  trajo de la consulta.
                conexion.Close();
                cmd.Connection.Close();
                return off;                                                                                                         // devolvemos el offset
            }
            else
            {
                off = "";                                                                                                           // si no es verdadera. devolvemos offset vacio.
                conexion.Close();
                cmd.Connection.Close();
                return off;
            }

        }


        public static int GuardarOffset(string offset, string legajo)                                                                  // metodo para guardar un nuevo offset.
        {
            int Filas = 0;                                                                                                          // declaramos una variable int filas igual a 0
            Conexion cn = new Conexion();                                                                                           // creamos un objeto conexion cn.
            MySqlConnection conexion = cn.conectar();                                                                               // guardamos la conexion en la variable conexion llamando a la funcion conectar de la clase cn
            conexion.Open();                                                                                                        // abrimos la conexion
            MySqlCommand cmd = new MySqlCommand("update usuarios set offset = '"+offset+"' where legajo = '" + legajo + "'", conexion);   // guardamos la consulta y la conexion en cmd.

            try
            {
                Filas = cmd.ExecuteNonQuery();                                                                                      // guardamos en filas la cantidad de filas que se ejecutaron en cmd.
            }
            catch (Exception e)
            {
                Filas = 0;                                                                                                          // si no se pudo ejecutar se guarda 0
            }
            finally
            {
                cmd.Connection.Close();                                                                                             // se cierra la conexion
            }

            return Filas;                                                                                                           // devolvemos la cantidad de filas, que en este caso va a ser siempre 1 se se modifico algun offset
        }
   

        public static bool DescontarRetiro(string legajo, int restante, DateTime fecha)
        {
            int Filas = 0;
            bool estado= false;
            Conexion cn = new Conexion();
            MySqlConnection conexion = cn.conectar();
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("update usuarios set pago = pago - @pago, fecha = @fecha where legajo = @legajo", conexion);

            try
            {
                cmd.Parameters.Add("@legajo", MySqlDbType.String).Value = legajo;
                cmd.Parameters.Add("@fecha", MySqlDbType.DateTime).Value = fecha;
                cmd.Parameters.Add("@pago", MySqlDbType.Int32).Value = restante;


                Filas = cmd.ExecuteNonQuery();
                if (Filas == 1)
                {
                    estado =  true;
                }
                else
                {
                    estado = false;
                }
                //estado = true;
            }
            catch (Exception e)
            {
                Filas = 0;
                estado = false;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return estado;
            //return Filas;
        }
        public static bool RastaurarMonto(string legajo, int importetemporal, DateTime fecha)
        {
            int Filas = 0;
            bool estado = false;
            Conexion cn = new Conexion();
            MySqlConnection conexion = cn.conectar();
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("update usuarios set pago = @pago, fecha = @fecha where legajo = @legajo", conexion);

            try
            {
                cmd.Parameters.Add("@legajo", MySqlDbType.String).Value = legajo;
                cmd.Parameters.Add("@fecha", MySqlDbType.DateTime).Value = fecha;
                cmd.Parameters.Add("@pago", MySqlDbType.Int32).Value = importetemporal;


                Filas = cmd.ExecuteNonQuery();
                if (Filas == 1)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }
                //estado = true;
            }
            catch (Exception e)
            {
                Filas = 0;
                estado = false;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return estado;
            //return Filas;
        }


    }
}
