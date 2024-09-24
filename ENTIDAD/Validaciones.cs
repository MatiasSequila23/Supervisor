using System;
using System.Collections.Generic;
using System.Text;

namespace ENTIDAD
{
    public class Validaciones
    {

        public static bool ValidarQueSeaDni(string numeroString, out string dni)
        {
            bool retorno = false;
            dni = numeroString;

            if ((numeroString.Length == 8 || numeroString.Length == 7) && int.TryParse(numeroString, out int a))
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }

            return retorno;
        
        }


        public static bool UsuarioExiste(List<Supervisores> usuario, string dni)
        {
            bool retorno = false;

            foreach (Supervisores item in usuario)
            {
                if (item.Dni == dni)
                {
                    retorno = true;
                    break;
                }
            }
            return retorno;
        }

        public static bool PassExiste(List<Supervisores> usuario, string dni, string pass)
        {
            bool retorno = false;

            foreach (Supervisores item in usuario)
            {
                if (item.Dni == dni && item.Pass == pass)
                {
                    retorno = true;
                    break;
                }
            }
            return retorno;
        }


    }


   

}
