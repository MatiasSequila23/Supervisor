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
    public partial class Dispensar : Form
    {
        string nombrearchivo = DateTime.Now.ToString("dd MMMM yyyy") + ".csv";

        private int counter = 7;

        int cont = 0;

        TcpClient client = new TcpClient();
        string msg;

        int inicial1;
        string dispensado1;
        int inicial2;
        string dispensado2;
        int montoentrega;


        public Dispensar(TcpClient cliente, string men, int ini1, string disp1, int ini2, string disp2, int monto)
        {
            SesionCliente.dispenso = 0;
            this.montoentrega = monto;
            this.inicial1 = ini1;
            this.dispensado1 = disp1;
            this.inicial2 = ini2;
            this.dispensado2 = disp2;

            this.msg = men;
            this.client = cliente;
            InitializeComponent();
        }

        private void Dispensar_Load(object sender, EventArgs e)
        {
            Cursor.Hide();

            txtRetiro.Visible = false;
            picRetirar.Visible = false;
            txtRetirar.Visible = false;

            timer2.Enabled = true;

            flecha1.Visible = false;
            flecha2.Visible = false;
            flecha3.Visible = false;
            timer1.Enabled = true;
            this.Activate();

            txtRetiro.Text = "";
            //btnAceptar.Visible = false;
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
                    SesionCliente.dispenso = 0;
                    FormError frm = new FormError();
                    frm.ShowDialog();
                }
            }
            else
            {
                SesionCliente.dispenso = 0;
                //timer2.Stop();
                FormError error = new FormError();
                error.ShowDialog();
            }
            return responseData;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cont == 8)
            {
                SesionCliente.Mensaje = Connect("127.0.0.1", msg);
                var msn = SEGURIDAD.Seguridad.GetInstancia().Controller2(SesionCliente.Mensaje);
                int Cod = Convert.ToInt32(msn.Item1);
                if (Cod != 0)
                {
                    this.Close();
                }
                else
                {
                    SesionCliente.dispenso = 1;
                    BalanceoArchivos b;
                    b = EscribeArchivo(inicial1.ToString("D8"), inicial2.ToString("D8"), dispensado1, dispensado2);
                    txtRetiro.Text = "USTED RETIRÓ: $" + montoentrega.ToString() + ".-";
                    txtRetiro.Visible = true;
                    txtRetirar.Visible = true;
                    picRetirar.Visible = true;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    pictureBox1.Visible = false;

                    flechagris.Visible = false;
                    flecha1.Visible = false;
                    flecha2.Visible = false;
                    flecha3.Visible = false;

                    flechaclara1.Visible = false;
                    flechaclara2.Visible = false;
                    flechaclara3.Visible = false;

                    //btnAceptar.Visible = true;
                    
                }
                timer1.Enabled = false;
               

            }
            else
            {
                if (cont == 0 || cont == 3 || cont == 6 || cont == 9)
                {
                    flechagris.Visible = false;

                    flechaclara1.Visible = false;
                    flechaclara2.Visible = false;
                    flechaclara3.Visible = false;

                    flecha1.Visible = true;
                    flecha2.Visible = false;
                    flecha3.Visible = false;
                }
                else
                {
                    if (cont == 1 || cont == 4 || cont == 7)
                    {
                        flechagris.Visible = false;

                        flechaclara1.Visible = true;
                        flechaclara2.Visible = false;
                        flechaclara3.Visible = false;

                        flecha1.Visible = false;
                        flecha2.Visible = true;
                        flecha3.Visible = false;
                    }
                    else
                    {
                        flechagris.Visible = true;

                        flechaclara1.Visible = false;
                        flechaclara2.Visible = true;
                        flechaclara3.Visible = false;

                        flecha1.Visible = false;
                        flecha2.Visible = false;
                        flecha3.Visible = true;
                    }
                }

                cont++;
            }
        }

        public static BalanceoArchivos EscribeArchivo(string monto1, string monto2, string dispensado1, string dispensado2)
        {

            DateTime diaHora = DateTime.Now;

            BalanceoArchivos d = new BalanceoArchivos(monto1, monto2, dispensado1, dispensado2);                                                     // se instancia el archivo balanceo, y  se envian los parametros. En balanceo archivos se elimina la ultima linea del archivo, asi se reescribe despues.
            Archivos.GenerarCSV(Directory.GetCurrentDirectory() + "\\utils\\balanceo.csv", d.CadenaParaGuardar());                                   // se genera el archivo guardado, con la cadena de caracteres que se va a guradar.

            return d;
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            counter--;                                                                                                                             // en cada tick del timer, descontamos uno a counter, que van a ser los segundos.


            try
            {
                if (counter == 0)                                                                                                                  // nos preguntamos si counter es igual a 0, si es si, quiere decir que se acabo la sesion.
                {
                    this.Close();
                                                                                 // abrimos el form de mensaje de error. Este se cierra tocando cualquier parte de la pantalla
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
                SEGURIDAD.Log.GetInstancia().LogExeption(mensaje);
                FormError frm = new FormError();
                frm.ShowDialog();
            }
        }

    }
}
