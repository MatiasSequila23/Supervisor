using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SEGURIDAD
{
    public class Seguridad
    {
        private static Seguridad instancia;
        private Seguridad()
        {

        }
        public static Seguridad GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new Seguridad();
            }
            return instancia;
        }
        public Tuple<string, string> Controller2(string message)
        {
            try
            {
                int inicio = message.IndexOf("<code>") + "<code>".Length;                                                                    // guardamos en inicio desde donde aparece la palabra amount en el mensaje que nos devuelve pagos, y le sumamos la cantidad de  caracteres que contiene <amount> para saber justo donde empieza el monto 
                int fin = message.IndexOf("</code>");                                                                                        // guardamos en fin desde donde empieza la palabra </amount> para saber donde termina el monto.
                string codigo = message.Substring(inicio, fin - inicio);
                int inicio2 = message.IndexOf("<message>") + "<message>".Length;                                                                    // guardamos en inicio desde donde aparece la palabra amount en el mensaje que nos devuelve pagos, y le sumamos la cantidad de  caracteres que contiene <amount> para saber justo donde empieza el monto 
                int fin2 = message.IndexOf("</message>");                                                                                        // guardamos en fin desde donde empieza la palabra </amount> para saber donde termina el monto.
                string msj = message.Substring(inicio2, fin2 - inicio2);
                string messag = "Codigo de error: " + codigo + " Mensaje del error: " + msj;
                return Tuple.Create(codigo, messag);

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
                return null;
            }
        }

        public Tuple<int, int,int> PuedeRetirar(int Importetotal)
        {
            try
            {

                int retira = 0;
                int gavhabilitadas = 0;
                int denominacion1 = 0;
                int denominacion2 = 0;

                string letra = "";
                string letrag1 = "";
                string letrag2 = "";
                string posg1 = "";
                string posg2 = "";
                int bill = 0;

                StreamReader archivo = File.OpenText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");                     // guardamos en la variable archivo, el archivo de las tablas, para ir leyendolo.                                                 

                string linea = "";                                                                                                  // declaramos una variable linea en vacio. Para leer cada linea del archivo.                                                     
                int a = 0;                                                                                                          // variable int a en 0                                                    
                while (!archivo.EndOfStream)                                                                                        // iniciamos un ciclo while que lea el archivo hasta el final, cuando termina de leer el archivo, termina el while.                                                   
                {

                    linea = archivo.ReadLine();                                                                                     // en la variable linea guarda la linea leida.                                                     

                    letra = linea.Substring(0, 1);
                    int den = Convert.ToInt32(linea.Substring(1, 5));                                                               // guardamos la demonominacion en la variable den
                    string pos = linea.Substring(6, 1);                                                                             // guardamos la posicion en la variable pos

                    switch (pos)
                    {
                        case "1":
                            denominacion1 = den;
                            letrag1 = letra;
                            posg1 = pos;
                            gavhabilitadas++;
                            break;

                        case "2":
                            denominacion2 = den;
                            letrag2 = letra;
                            posg2 = pos;
                            gavhabilitadas++;
                            break;
                    }
                }
                int DenMen = 0;
                archivo.Close();            // cerramos el archivo que estamos leyendo de las gavetas

                if (gavhabilitadas != 0)
                {
                    if (denominacion1 == 500 && denominacion2 == 200 || denominacion1 == 200 && denominacion2 == 500)
                    {

                        if (Importetotal == 300)
                        {
                            DenMen = 200;
                            Importetotal = 200;
                            retira = Importetotal;
                        }
                        else
                        {
                            if (Importetotal < 200)
                            {
                                DenMen = 200;
                                retira = 0;
                            }
                            else
                            {
                                DenMen = 200;
                                int aux = Importetotal % 100;
                                Importetotal = Importetotal - aux;
                                retira = Importetotal;
                            }

                        }
                    }
                    else
                    {
                        if (gavhabilitadas == 2 && denominacion1 > denominacion2)
                        {
                            DenMen = denominacion2;

                        }
                        else if (gavhabilitadas == 1 || gavhabilitadas == 2)
                        {
                            DenMen = denominacion1;
                        }
                        if (Importetotal >= DenMen)
                        {
                            bill = Importetotal / DenMen;
                        }
                        retira = bill * DenMen;
                    }               
                }

                return Tuple.Create(retira, DenMen, gavhabilitadas);
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
                return null;
            }
        }
    }
}
