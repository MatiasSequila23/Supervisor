using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ENTIDAD
{
    public class Conexion
    {
        

        String SqlRuta = LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\BD\\Conexion.ini");

        public static string LeeUltimoRegistro(string archivo)
        {
            string lastLine = File.ReadLines(archivo).Last();
            return lastLine;
        }




        public MySqlConnection conectar()
        {

            MySqlConnection cmd = new MySqlConnection();               //crea una conexion en la ruta
            cmd.ConnectionString = SqlRuta;                            //guarda la ruta en la variable cmd, que es una instancia de la clase sql connection
            return cmd;                                                //devuelve la conexion sql
        }
    
    }
}
