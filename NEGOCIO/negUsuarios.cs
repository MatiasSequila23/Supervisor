using System;
using System.Collections.Generic;
using System.Text;
using ENTIDAD;
using DAO;

namespace NEGOCIO
{
    public class negUsuarios
    {
        public static Usuarios BuscarUsuario(string legajo)
        {
            return daoUsuarios.BuscarUsuario(legajo);
        }

        public static int ModificarOffset(string dni)
        {
            return daoUsuarios.ModificarOffset(dni);
        }

        public static string BuscarOffset(string legajo)
        {
            return daoUsuarios.BuscarOffset(legajo);
        }

        public static int GuardarOffset(string offset, string legajo)
        {
            return daoUsuarios.GuardarOffset(offset,legajo);
        }

        public static bool DescontarRetiro(string legajo, int restante, DateTime fecha)
        {

            bool estado = daoUsuarios.DescontarRetiro(legajo, restante, fecha);
            return estado;
        }

        public static bool RestaurarMonto(string legajo, int importetemporal, DateTime fecha)
        {
            bool estado = daoUsuarios.RastaurarMonto(legajo, importetemporal, fecha);
            return estado;
        }
    }
}
