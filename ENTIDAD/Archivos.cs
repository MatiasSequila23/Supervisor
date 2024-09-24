using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ENTIDAD
{
    public class Archivos
    {

        public static List<string[]> parseCSV(string path)
        {
            List<string[]> parsedData = new List<string[]>();

            using (StreamReader readFile = new StreamReader(path))
            {
                string line;
                string[] row;

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(',');
                    parsedData.Add(row);
                }
            }
            return parsedData;
        }


        public static string LeeUltimoRegistro(string archivo)
        {
            string lastLine = File.ReadLines(archivo).Last();
            return lastLine;
        }

        public static void EliminaUltimaLinea(string archivo)
        {
            List<string> lines = File.ReadAllLines(archivo).ToList();

            File.WriteAllLines(archivo, lines.GetRange(0, lines.Count - 1).ToArray());
        }


        public static void Guardado(string inicial1, string inicial2, string dispensado1, string dispensado2)
        {
            Archivos archivo = new Archivos();

            string path = Directory.GetCurrentDirectory() + "\\utils\\balanceo.CSV";
            archivo.Guardar(path, inicial1, inicial2, dispensado1, dispensado2);

        }



        public static void GuardadoTransaccion(string transaccion)
        {
            Archivos archivo = new Archivos();

            string path = Directory.GetCurrentDirectory() + "\\utils\\Transaccion.CSV";
            archivo.GuardarTransaccion(path, transaccion);

        }

        public bool Guardar(string archivo, string inicial1, string inicial2, string dispensado1, string dispensado2)
        {
            bool retorno = false;
            try
            {
                using (StreamWriter sw = new StreamWriter(archivo, false, UTF8Encoding.ASCII))
                {
                    sw.WriteLine(inicial1, inicial2, dispensado1, dispensado2);
                    retorno = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return retorno;
        }


        public bool GuardarTransaccion(string archivo, string transaccion)
        {
            bool retorno = false;
            try
            {
                using (StreamWriter sw = new StreamWriter(archivo, false, UTF8Encoding.ASCII))
                {
                    sw.WriteLine(transaccion);
                    retorno = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return retorno;
        }


        public static void GenerarCSV(string ruta, string cadena)
        {
            using (StreamWriter mylogs = File.AppendText(ruta))         //se crea el archivo
            {

                mylogs.WriteLine(cadena);
                mylogs.Close();

            }
        }


        public static Logs EscribeArchivoLogs(DateTime fecha, string esi, string ntransaccion, string datos, string nombrearch)
        {
            Logs d = new Logs(fecha, esi, ntransaccion, datos);                                                                                  // se instancia el archivo balanceo, y  se envian los parametros. En balanceo archivos se elimina la ultima linea del archivo, asi se reescribe despues.
            Archivos.GenerarCSV(Directory.GetCurrentDirectory() + "\\Logs\\"+nombrearch, d.CadenaParaGuardar());                                   // se genera el archivo guardado, con la cadena de caracteres que se va a guradar.

            return d;
        }
    }
}
