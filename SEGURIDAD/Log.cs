using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ENTIDAD;
namespace SEGURIDAD
{
    public class Log
    {
        private static Log instancia;
        private Log()
        {

        }
        public static Log GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new Log();
            }
            return instancia;
        }
        public void LogExeption(string MensError)
        {
            string fech = DateTime.Now.ToString("dd MMMM yyyy");
            string hor = DateTime.Now.ToString(" HH mm ss");
            string ruta = Directory.GetCurrentDirectory() + @"\\ReporteError\\Reporte" + fech + hor + ".csv";
            StreamWriter mylogs = File.AppendText(Directory.GetCurrentDirectory() + @"\\Logs\\LogExeption\\Error " + fech + hor + ".csv");
            mylogs.WriteLine(MensError);
            mylogs.Close();
        }
        public void CrearLogErrorDisp(string MensError)
        {
            string fech = DateTime.Now.ToString("dd MMMM yyyy");
            string hor = DateTime.Now.ToString(" HH mm ss");
            string ruta = Directory.GetCurrentDirectory() + @"\\ReporteError\\Reporte" + fech + hor + ".csv";
            StreamWriter mylogs = File.AppendText(Directory.GetCurrentDirectory() + @"\\Logs\\LogsErrorDispositivo\\Error " + fech + hor + ".csv");
            mylogs.WriteLine(MensError);
            mylogs.Close();
        }
        public string CrearDocRespaldo(string ntransaccion, int montoentrega, string legajo, string nombre)
        {
            try
            {
                Usuarios usu = new Usuarios();
                string fech = DateTime.Now.ToString("dd MMMM yyyy");
                string hor = DateTime.Now.ToString(" HH mm ss");
                string ruta = Directory.GetCurrentDirectory() + @"\\ReporteError\\Reporte" + fech + hor + ".csv";
                string linea1 = "Reporte: DINERO NO ENTREGADO ";
                string linea2 = "USUARIO: " + nombre;// + " " + usu.Apellido;
                string linea3 = "LEGAJO: " + legajo;
                string linea4 = "NUMERO TRANSACCION: " + ntransaccion;
                string linea5 = "FECHA Y HORA: " + DateTime.Now;
                string linea6 = "MONTO: " + montoentrega;
                string linea7 = "ESTADO TEMPORAL: Devolución de dinero EN ESPERA";
                StreamWriter mylog = File.AppendText(Directory.GetCurrentDirectory() + @"\\ReporteError\\Reporte" + fech + hor + ".csv");
                mylog.Close();
                StreamWriter file = new StreamWriter(ruta, true);
                char pad = ' ';
                file.WriteLine(" ____________________________________________________________");
                file.WriteLine("|" + linea1.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea2.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea3.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea4.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea5.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea6.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea7.PadRight(60, pad) + "|");
                file.WriteLine("|____________________________________________________________|");
                file.Close();
                return ruta;
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
                SEGURIDAD.Log.GetInstancia().LogExeption(mensaje);
                return  mensaje;
            }
        }

        public string LogReembolsoCritico(string ntransaccion, int montoentrega, string legajo, string nombre)
        {
            try
            {
                Usuarios usu = new Usuarios();
                string fech = DateTime.Now.ToString("dd MMMM yyyy");
                string hor = DateTime.Now.ToString(" HH mm ss");
                string ruta = Directory.GetCurrentDirectory() + @"\\Logs\\LogReembolsoCritico\\Error " + fech + hor + ".csv";
                string linea1 = "Reporte: DINERO NO ENTREGADO ";
                string linea2 = "USUARIO: " + nombre;// + " " + usu.Apellido;
                string linea3 = "LEGAJO: " + legajo;
                string linea4 = "NUMERO TRANSACCION: " + ntransaccion;
                string linea5 = "FECHA Y HORA: " + DateTime.Now;
                string linea6 = "MONTO: " + montoentrega;
                string linea7 = "ESTADO TEMPORAL: Devolución de dinero EN ESPERA";
                StreamWriter mylog = File.AppendText(Directory.GetCurrentDirectory() + @"\\Logs\\LogReembolsoCritico\\Error " + fech + hor + ".csv");
                mylog.Close();
                StreamWriter file = new StreamWriter(ruta, true);
                char pad = ' ';
                file.WriteLine(" ____________________________________________________________");
                file.WriteLine("|" + linea1.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea2.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea3.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea4.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea5.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea6.PadRight(60, pad) + "|");
                file.WriteLine("|" + linea7.PadRight(60, pad) + "|");
                file.WriteLine("|____________________________________________________________|");
                file.Close();
                return ruta;
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
                SEGURIDAD.Log.GetInstancia().LogExeption(mensaje);
                return mensaje;
            }
        }
        public void ComplDocrespaldo(string ruta )
        {
            try
            {
                var lines = File.ReadAllLines(ruta);
                File.WriteAllLines(ruta, lines.Take(lines.Length - 2).ToArray());
                StreamWriter filee = new StreamWriter(ruta, true);
                filee.WriteLine("|ESTADO FINAL: Devolución de dinero EXITOSA" + "                  |");
                filee.WriteLine("|____________________________________________________________|");
                filee.Close();
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
                SEGURIDAD.Log.GetInstancia().LogExeption(mensaje);

            }
        }
    }
}
