using System;
using System.Collections.Generic;
using System.Text;

namespace ENTIDAD
{
    public class Logs
    {
        public DateTime Fecha;
        public string Esi;
        public string nTransaccion;
        public string Datos;


        public Logs( DateTime fecha, string esi, string ntransaccion, string datos)
        {
            this.Fecha = fecha;
            this.Esi = esi;
            this.nTransaccion = ntransaccion;
            this.Datos = datos;
        }

        public string CadenaParaGuardar()
        {
            return this.Fecha + "|" + this.Esi + "|" + this.nTransaccion + "|" + this.Datos;
        }


    }
}
