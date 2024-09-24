using System;
using System.Collections.Generic;
using System.Text;

namespace ENTIDAD
{
    public class Supervisores
    {


        public string Dni { get; set; }
        public string Pass { get; set; }
        public string Balanceo { get; set; }
        public string Clave { get; set; }


        public Supervisores(string dni, string pass, string balanceo, string clave)
        {
            this.Dni = dni;
            this.Pass = pass;
            this.Balanceo = balanceo;
            this.Clave = clave;
        }
        
      

        public static bool parseo(List<string[]> listado, out List<Supervisores> usuario)
        {
            bool retorno = false;
            bool flag = false;
            usuario = new List<Supervisores>();

            foreach (string[] item in listado)
            {
                if (flag == true)
                {
                    string a = ConvertStringArrayToString(item);
                    string[] datos = a.Split(';');

                    Supervisores p = new Supervisores(datos[0], datos[1], datos[2], datos[3]);
                    usuario.Add(p);
                    retorno = true;

                }
                else
                {
                    flag = true;
                }


            }



            return retorno;
        }


        public static string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append('.');
            }
            return builder.ToString();
        }


    }
}
