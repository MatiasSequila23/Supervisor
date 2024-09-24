using ENTIDAD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supervisor
{
    public partial class ConfirmarBalanceo : Form
    {

        string cod = "Codigo de Error: ";
        string ms = "  Mensaje: ";

        TcpClient client = new TcpClient();

        string nombrearchivo;
        int gav1;
        int gav2;
        string monto1;
        string monto2;
        string doc;

        TextReader txtReader;
        TextWriter txtWriter;

        public ConfirmarBalanceo( string narchivo, int g1, int g2, string mon1, string mon2, string docu, TcpClient cliente)
        {
            InitializeComponent();

            this.nombrearchivo = narchivo;
            this.gav1 = g1;
            this.gav2 = g2;
            this.monto1 = mon1;
            this.monto2 = mon2;
            this.doc = docu;

            this.client = cliente;
        }

    

        public static BalanceoArchivos EscribeArchivo(string monto1, string monto2, string dispensado1, string dispensado2)
        {

            DateTime diaHora = DateTime.Now;

            BalanceoArchivos d = new BalanceoArchivos(monto1, monto2, dispensado1, dispensado2);                                                 // se instancia el archivo balanceo, y  se envian los parametros. En balanceo archivos se elimina la ultima linea del archivo, asi se reescribe despues.
            Archivos.GenerarCSV(Directory.GetCurrentDirectory() + "\\utils\\balanceo.csv", d.CadenaParaGuardar());                               // se genera el archivo guardado, con la cadena de caracteres que se va a guradar.

            return d;
        }

        private void ConfirmarBalanceo_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
            try
            {

                panel1.Visible = false;

                txtCantG1.Text = monto1.ToString();
                txtCantG2.Text = monto2.ToString();
                txtDenG1.Text = gav1.ToString();
                txtDenG2.Text = gav2.ToString();

                int total = ((gav1 * Convert.ToInt32(monto1)) + (gav2 * Convert.ToInt32(monto2)));
                txtTotal.Text = "$ " + total.ToString() + ".-";
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
                FormError frm = new FormError();
                frm.ShowDialog();
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
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
                FormError frm = new FormError();
                frm.ShowDialog();

            }

        }

        public string Connect(String server, String message)
        {
            string responseData = String.Empty;
            if (client.Connected)
            {
                try
                {
                    // Translate the passed message into ASCII and store it as a Byte array.
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                    // Get a client stream for reading and writing.
                    NetworkStream stream = client.GetStream();

                    // Send the message to the connected TcpServer. 
                    stream.Write(data, 0, data.Length);

                    // Receive the TcpServer.response.

                    // Buffer to store the response bytes.
                    data = new Byte[1000];

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);
                }
                catch (Exception ex)
                {
                    SesionIniciada.BanderaError = 1;
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
                    FormError frm = new FormError();
                    frm.ShowDialog();
                }
                
            }
            else
            {

                FormError error = new FormError();
                error.ShowDialog();
            }
            return responseData;
        }

        public string Controller(string message)
        {
            int inicio = message.IndexOf("<code>") + "<code>".Length;                                                                    // guardamos en inicio desde donde aparece la palabra amount en el mensaje que nos devuelve pagos, y le sumamos la cantidad de  caracteres que contiene <amount> para saber justo donde empieza el monto 
            int fin = message.IndexOf("</code>");                                                                                        // guardamos en fin desde donde empieza la palabra </amount> para saber donde termina el monto.

            string codigo = message.Substring(inicio, fin - inicio);
            ////// esto puede no ir ////
            int inicio2 = message.IndexOf("<message>") + "<message>".Length;                                                                    // guardamos en inicio desde donde aparece la palabra amount en el mensaje que nos devuelve pagos, y le sumamos la cantidad de  caracteres que contiene <amount> para saber justo donde empieza el monto 
            int fin2 = message.IndexOf("</message>");                                                                                        // guardamos en fin desde donde empieza la palabra </amount> para saber donde termina el monto.
            string msj = message.Substring(inicio2, fin2 - inicio2);
            string messag = codigo + msj;
            return codigo;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if(txtReader != null)
            {
                txtReader.Close();
            }
            if(txtWriter != null)
            {
                txtWriter.Close();
            }

            try
            {
                string den;
                if (Directory.Exists(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv"))
                {
                    File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv");
                }

                using (txtReader = File.OpenText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv"))
                {
                    using (txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv"))
                    {

                        
                        while (true)
                        {

                            string line = txtReader.ReadLine();
                            if (line != null)
                            {
                                int deno = Convert.ToInt32(line.Substring(1, 5));
                                string letra = line.Substring(0, 1);
                                int pos = Convert.ToInt32(line.Substring(6, 1));

                                if (deno == gav1)
                                {
                                    den = deno.ToString("D5");

                                    line = letra + den + "1";
                                    txtWriter.WriteLine(line);
                                }
                                else
                                {
                                    if (deno == gav2)
                                    {
                                        den = deno.ToString("D5");
                                        line = letra + den + "2";
                                        txtWriter.WriteLine(line);
                                    }
                                    else
                                    {
                                        den = deno.ToString("D5");
                                        line = letra + den + "0";
                                        txtWriter.WriteLine(line);
                                    }
                                }
                            }
                            else { break; }
                        }
                        txtReader.Close();
                        txtWriter.Close();
                        File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                        File.Copy(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv", Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                        File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv"); 
                    } 
                }


                string gaveta1 = "";
                string gaveta2 = "";

                EscribeArchivo(monto1, monto2, "0", "0");



                ////////////////////////////////////    INICIO TABLA AUXILIAR //////////////////////



                string den2;
                if (Directory.Exists(Directory.GetCurrentDirectory() + "\\Tablas\\Tab2.csv"))
                {
                    File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tab2.csv");
                }

                using (txtReader = File.OpenText(Directory.GetCurrentDirectory() + "\\Tablas\\Tab.csv"))
                {
                    using (txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Tablas\\Tab2.csv"))
                    {


                        while (true)
                        {

                            string line = txtReader.ReadLine();
                            if (line != null)
                            {
                                int deno = Convert.ToInt32(line.Substring(1, 5));
                                string letra = line.Substring(0, 1);
                                int pos = Convert.ToInt32(line.Substring(6, 1));

                                if (deno == gav1)
                                {
                                    den2 = deno.ToString("D5");

                                    line = letra + den2 + "1";
                                    txtWriter.WriteLine(line);
                                }
                                else
                                {
                                    if (deno == gav2)
                                    {
                                        den2 = deno.ToString("D5");
                                        line = letra + den2 + "2";
                                        txtWriter.WriteLine(line);
                                    }
                                    else
                                    {
                                        den2 = deno.ToString("D5");
                                        line = letra + den2 + "0";
                                        txtWriter.WriteLine(line);
                                    }
                                }
                            }
                            else { break; }
                        }
                        txtReader.Close();
                        txtWriter.Close();
                        File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tab.csv");
                        File.Copy(Directory.GetCurrentDirectory() + "\\Tablas\\Tab2.csv", Directory.GetCurrentDirectory() + "\\Tablas\\Tab.csv");
                        File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tab2.csv");
                    }
                }


                //////////////////// FIN TABLA AUXILIAR ////////////////////


                                                                                                                              // llamamos ala funcion escribir archivo y enviamos el monto1, monto2, y 0 y 0 que seran los billetes dispensados cuando se ingresa, que no se dispenso ninguno todavia.

                for (int i = 3; i >= 0; i--)                                                                                                                                      // iniciamos un ciclo for de 4 vueltas por la cantidad de gavetas que hay. Si habria 6 gavetas, tendria que ser de 6 vueltas. (gavetas declaradas en archivo, no habilitadas)
                {

                    StreamReader archivo = File.OpenText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");                                                               // con el streamreader abrimos el archivo que vamos a leer.

                    string linea = "";                                                                                                                                            // declaramos una variable string linea en vacio, aca almacenaremos la linea leida desde el archivo.
                    int a = 0;                                                                                                                                                    // declaramos una variable int a 0 que despues vamos a ir sumando de a uno.
                    while (!archivo.EndOfStream)                                                                                                                                  // iniciamos un while que lea el archivo hasta el final
                    {
                        linea = archivo.ReadLine();                                                                                                                               // guardamos en la variable linea la linea del archivo leida.
                        if (a == i) break;                                                                                                                                        // preguntamos si a (inicialmente 0) es igual a i (inicialmente 3, va descontando por vuelta). Esto es para que empiece tomando la linea de la gaveta de mas denominacion y asi empiece diviendo el monto por los billetes mas grandes. Si es igual, sale del while.
                        a++;                                                                                                                                                      // sumamos 1 a la variable a
                    }

                    string posicion = linea.Substring(6, 1);                                                                                                                      // guardamos en el string posicion la posicion de la gaveta.        

                    if (posicion != "0")                                                                                                                                          // nos preguntamos si posicion es distinto de 0
                    {
                        string ga = linea.Substring(0, 1);                                                                                                                        // guardamos en la variable ga, la letra asignada a la gaveta.
                                                                                                                                                                                  // pasamos a int la denominacion para que nos saque los ceros que estan de mas y poder realizar calculos.                                                                                                                                                       // guardamos el resto de la division entre importe y deno, para que nos de un monto para entregar en billetes mas chicos.
                                                                                                                                                                                  // a importe le restamos el importe menos la multiplicacion entre billetes y deno. Para que cuando vuelva la vuelta del for, importe se haya descontado con lo que se entrego en billetes de la denominacion mas alta.
                        switch (posicion)                                                                                                                                         // hacemos un switch de posicion, para saber que gaveta esta seleccionada.
                        {

                            case "2":                                                                                                                                             // si posicion es 2                                                       
                                gaveta2 = ga;                                                                                                                                     // en gaveta2, guardamos la letra de la gaveta seleccionada.
                                break;

                            case "1":                                                                                                                                             // si posicion es 1                                    
                                gaveta1 = ga;                                                                                                                                     // en gaveta1, guardamos la letra de la gaveta seleccionada.
                                break;

                        }
                    }
                }

                string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                          //leemos el numero de transaccion desde el archivo.                      
                string datos = doc + ";" + monto1 + gaveta1 + ";" + monto2 + gaveta2;                                                                                            // guardamos el dni del supervisor y los montos de cada gaveta
                Archivos.EscribeArchivoLogs(DateTime.Now, "B", data, datos, nombrearchivo);                                                                                      // escribimos en el archivo el balanceo y los montos de las gavetas

                string msg = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><endexchg/>";
                string msn = Connect("127.0.0.1", msg);
                var msjcompl = SEGURIDAD.Seguridad.GetInstancia().Controller2(msn);
                int CodigoError = Convert.ToInt32(msjcompl.Item1);
                string MensError = msjcompl.Item2;
                if (CodigoError != 0)
                {
                    SesionIniciada.BanderaError = 1;
                    datos = "Error Bloqueo De Cartuchos";
                    Archivos.EscribeArchivoLogs(DateTime.Now, "F", data, datos, nombrearchivo);
                    SEGURIDAD.Log.GetInstancia().CrearLogErrorDisp(MensError);
                    FormError error = new FormError();
                    error.ShowDialog();
                }

                SesionIniciada ini = new SesionIniciada(client);
                ini.bloqueo = true;

                panel1.Visible = true;
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
                FormError frm = new FormError();
                frm.ShowDialog();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
