using ENTIDAD;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;

namespace Supervisor
{
    public partial class Inicio : Form
    {
        string captcha = "";
        TcpClient client = new TcpClient();

        public Inicio()
        {
            InitializeComponent();
        }


        private void Inicio_Load(object sender, EventArgs e)
        {


            Iniciando ini = new Iniciando();
            ini.ShowDialog();


                //Thread.Sleep(30000);

                try
                {

                panel1.Visible = false;

                timer1.Enabled = true;                                                                                              // habilitamos el timer de la hora

                lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
                lblFecha.Text = DateTime.Now.ToString(" dd MMMM yyyy ");

                lblError.Text = "";
                client.Connect("127.0.0.1", 4500);
                ////////////// RESPUESTA CONEXION A GAVETAS ///////////////
                NetworkStream stream = client.GetStream();
                Byte[] data = new Byte[1000];
                Int32 bytes = stream.Read(data, 0, data.Length);
                string responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //MessageBox.Show(responseData);
                string msg = "<?xml version = \"1.0\" encoding = \"ISO -8859-1\" ?><init/>";
                string msn = Connect("127.0.0.1", msg);

                //MessageBox.Show(msn);

                ////////////// FIN RESPUESTA A CONEXION GAVETAS ////////////////


                var msjcompl = SEGURIDAD.Seguridad.GetInstancia().Controller2(msn);
                int CodigoError = Convert.ToInt32(msjcompl.Item1);
                string MensError = msjcompl.Item2;
                if (CodigoError != 0)
                {

                    if (MensError.Contains("Empty Cassette"))
                    {
                        panel1.Visible = true;
                    }
                    else
                    {

                        string datas = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                        string datos = "Error Inicio Servicio";
                        // Archivos.EscribeArchivoLogs(DateTime.Now, "F", datas, datos, nombrearchivo);
                        SEGURIDAD.Log.GetInstancia().CrearLogErrorDisp(MensError);
                        FormError error = new FormError();
                        error.ShowDialog();
                    }

                }


                Cursor.Hide();                                                                                                                                       // escondemos el cursor, para que no se vea en la pantalla tactil
                lblError.Text = "";
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
            }// lblError lo ponemos en vacio
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
                    //Console.WriteLine("Sent: {0}", message);

                    // Buffer to store the response bytes.
                    data = new Byte[1000];

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    //Console.WriteLine("Received: {0}", responseData);
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
            else
            {
                FormError error = new FormError();
                error.ShowDialog();
            }
            return responseData;
        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            captcha = "";

            captcha += "1";                                                                                                                                       // agregamos el valor del boton al string captcha.

            if (captcha.Length == 4)                                                                                                                              // nos preguntamos si el captcha tiene la longitud de 4 caracteres.
            {
                if (captcha == "1234")                                                                                                                            // nos preguntamos si el captcha es igual a 1234 que seria el orden como se apretan los botones.
                {
                    Supervisor sup = new Supervisor(client);                                                                                                            // instanciamos el form supervisor.
                    sup.ShowDialog();                                                                                                                             // abrimos el form supervisor.
                }
                else
                {
                    captcha = "";                                                                                                                                 // si el captcha no es 1234, restablecemos la variable a vacio para ingresar de nuevo.
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            captcha += "2";                                                                                                                                       // agregamos el valor del boton al string captcha.

            if (captcha.Length == 4)                                                                                                                              // nos preguntamos si el captcha tiene la longitud de 4 caracteres.
            {
                if (captcha == "1234")                                                                                                                            // nos preguntamos si el captcha es igual a 1234 que seria el orden como se apretan los botones.
                {
                    Supervisor sup = new Supervisor(client);                                                                                                            // instanciamos el form supervisor.
                    sup.ShowDialog();                                                                                                                             // abrimos el form supervisor.
                }
                else
                {
                    captcha = "";                                                                                                                                 // si el captcha no es 1234, restablecemos la variable a vacio para ingresar de nuevo.
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            captcha += "3";                                                                                                                                       // agregamos el valor del boton al string captcha.

            if (captcha.Length == 4)                                                                                                                              // nos preguntamos si el captcha tiene la longitud de 4 caracteres.
            {
                if (captcha == "1234")                                                                                                                            // nos preguntamos si el captcha es igual a 1234 que seria el orden como se apretan los botones.
                {
                    Supervisor sup = new Supervisor(client);                                                                                                            // instanciamos el form supervisor.
                    sup.ShowDialog();                                                                                                                             // abrimos el form supervisor.
                }
                else
                {
                    captcha = "";                                                                                                                                 // si el captcha no es 1234, restablecemos la variable a vacio para ingresar de nuevo.
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            captcha += "4";                                                                                                                                       // agregamos el valor del boton al string captcha.

            if (captcha.Length == 4)                                                                                                                              // nos preguntamos si el captcha tiene la longitud de 4 caracteres.
            {
                if (captcha == "1234")                                                                                                                            // nos preguntamos si el captcha es igual a 1234 que seria el orden como se apretan los botones.
                {
                    Supervisor sup = new Supervisor(client);                                                                                                            // instanciamos el form supervisor.
                    sup.ShowDialog();                                                                                                                             // abrimos el form supervisor.
                }
                else
                {
                    captcha = "";                                                                                                                                 // si el captcha no es 1234, restablecemos la variable a vacio para ingresar de nuevo.
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (this.txtDNI.Text.Length < 5)
            {
                this.txtDNI.Text = this.txtDNI.Text + "1";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (this.txtDNI.Text.Length < 5)
            {
                this.txtDNI.Text = this.txtDNI.Text + "2";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (this.txtDNI.Text.Length < 5)
            {
                this.txtDNI.Text = this.txtDNI.Text + "3";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (this.txtDNI.Text.Length < 5)
            {
                this.txtDNI.Text = this.txtDNI.Text + "4";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (this.txtDNI.Text.Length < 5)
            {
                this.txtDNI.Text = this.txtDNI.Text + "5";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (this.txtDNI.Text.Length < 5)
            {
                this.txtDNI.Text = this.txtDNI.Text + "6";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (this.txtDNI.Text.Length < 5)
            {
                this.txtDNI.Text = this.txtDNI.Text + "7";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (this.txtDNI.Text.Length < 5)
            {
                this.txtDNI.Text = this.txtDNI.Text + "8";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (this.txtDNI.Text.Length < 5)
            {
                this.txtDNI.Text = this.txtDNI.Text + "9";
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (this.txtDNI.Text.Length < 5)
            {
                this.txtDNI.Text = this.txtDNI.Text + "0";
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string aux = this.txtDNI.Text;
            if (aux.Length > 0)
            {
                this.txtDNI.Text = aux.Substring(0, aux.Length - 1);
            }
            if (aux.Length == 1)
            {
                this.lblError.Text = "";
            }
        }

        private void btnBorrarTodo_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            this.txtDNI.Text = "";

            this.lblError.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString(" dd MMMM yyyy ");
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            string legajo = txtDNI.Text;

            if (txtDNI.Text != "")
            {
                if (legajo.Length == 5)
                {
                    Usuarios usu = new Usuarios();

                    usu = negUsuarios.BuscarUsuario(legajo);

                    if (usu != null)
                    {
                        txtDNI.Text = "";
                        lblError.Text = "";
                        IniciarSesion inicia = new IniciarSesion(usu,client);
                        inicia.ShowDialog();
                    }
                    else
                    {
                        lblError.Text = "Legajo inexistente.";
                    }
                }
                else
                {
                    lblError.Text = "Legajo incorrecto.";
                }
            }
            else
            {
                lblError.Text = "Debe ingresar un legajo.";
            }

        }

        private void Inicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSiguiente.PerformClick();
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                string legajo = txtDNI.Text;

                if (txtDNI.Text != "")
                {
                    if (legajo.Length == 5)
                    {
                        Usuarios usu = new Usuarios();

                        usu = negUsuarios.BuscarUsuario(legajo);

                        if (usu != null)
                        {
                            lblError.Text = "";
                            txtDNI.Text = "";
                            IniciarSesion inicia = new IniciarSesion(usu,client);
                            inicia.ShowDialog();
                        }
                        else
                        {
                            lblError.Text = "Legajo inexistente.";
                        }
                    }
                    else
                    {
                        lblError.Text = "Legajo incorrecto.";
                    }
                }
                else
                {
                    lblError.Text = "Debe ingresar un legajo.";
                }
            }
        }

        private void btnIrBalanceo_Click(object sender, EventArgs e)
        {
            try
            {
                Supervisor sup = new Supervisor(client);
                sup.ShowDialog();

                string msg = "<?xml version = \"1.0\" encoding = \"ISO -8859-1\" ?><init/>";
                string msn = Connect("127.0.0.1", msg);

                panel1.Visible = false;
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
