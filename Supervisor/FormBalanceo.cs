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
    public partial class FormBalanceo : Form
    {
        string nombrearchivo = DateTime.Now.ToString("dd MMMM yyyy") + ".csv";

        TcpClient client = new TcpClient();

        int text = 1;
        string dni;
        internal static object balanceo;

        public FormBalanceo(string doc, TcpClient cliente)
        {
            InitializeComponent();
            this.dni = doc;
            this.client = cliente;
        }

        private void Balanceo_Load(object sender, EventArgs e)
        {
            Cursor.Hide();

            try
            {
                cmbDen1.SelectedText = "";
                cmbDen2.SelectedText = "";

                lblError1.Text = "";


                //// llenamos los combo box

                StreamReader archivo = File.OpenText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");                     // guardamos en la variable archivo, el archivo de las tablas, para ir leyendolo.                                                 

                string linea = "";                                                                                                  // declaramos una variable linea en vacio. Para leer cada linea del archivo.                                                     
                while (!archivo.EndOfStream)                                                                                        // iniciamos un ciclo while que lea el archivo hasta el final, cuando termina de leer el archivo, termina el while.                                                   
                {

                    linea = archivo.ReadLine();                                                                                     // en la variable linea guarda la linea leida.                                                     

                    int den = Convert.ToInt32(linea.Substring(1, 5));                                                               // guardamos la demonominacion en la variable den

                    cmbDen1.Items.Add(den);                                                                                         // agregamos den a cada combobox

                    cmbDen2.Items.Add(den);

                }

                archivo.Close();
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
            }                                                                                                  // cerramos el archivo abierto
        }

        private void txtMonto1_Leave(object sender, EventArgs e)
        {
            text = 1;
        }

        private void txtMonto2_Leave(object sender, EventArgs e)
        {
            text = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtMonto1.Text.Length < 10)
                {
                    this.txtMonto1.Text = this.txtMonto1.Text + "1";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtMonto2.Text.Length < 10)
                    {
                        this.txtMonto2.Text = this.txtMonto2.Text + "1";
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtMonto1.Text.Length < 10)
                {
                    this.txtMonto1.Text = this.txtMonto1.Text + "2";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtMonto2.Text.Length < 10)
                    {
                        this.txtMonto2.Text = this.txtMonto2.Text + "2";
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtMonto1.Text.Length < 10)
                {
                    this.txtMonto1.Text = this.txtMonto1.Text + "3";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtMonto2.Text.Length < 10)
                    {
                        this.txtMonto2.Text = this.txtMonto2.Text + "3";
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtMonto1.Text.Length < 10)
                {
                    this.txtMonto1.Text = this.txtMonto1.Text + "4";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtMonto2.Text.Length < 10)
                    {
                        this.txtMonto2.Text = this.txtMonto2.Text + "4";
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtMonto1.Text.Length < 10)
                {
                    this.txtMonto1.Text = this.txtMonto1.Text + "5";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtMonto2.Text.Length < 10)
                    {
                        this.txtMonto2.Text = this.txtMonto2.Text + "5";
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtMonto1.Text.Length < 10)
                {
                    this.txtMonto1.Text = this.txtMonto1.Text + "6";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtMonto2.Text.Length < 10)
                    {
                        this.txtMonto2.Text = this.txtMonto2.Text + "6";
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtMonto1.Text.Length < 10)
                {
                    this.txtMonto1.Text = this.txtMonto1.Text + "7";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtMonto2.Text.Length < 10)
                    {
                        this.txtMonto2.Text = this.txtMonto2.Text + "7";
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtMonto1.Text.Length < 10)
                {
                    this.txtMonto1.Text = this.txtMonto1.Text + "8";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtMonto2.Text.Length < 10)
                    {
                        this.txtMonto2.Text = this.txtMonto2.Text + "8";
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtMonto1.Text.Length < 10)
                {
                    this.txtMonto1.Text = this.txtMonto1.Text + "9";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtMonto2.Text.Length < 10)
                    {
                        this.txtMonto2.Text = this.txtMonto2.Text + "9";
                    }
                }
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtMonto1.Text.Length < 10)
                {
                    this.txtMonto1.Text = this.txtMonto1.Text + "0";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtMonto2.Text.Length < 10)
                    {
                        this.txtMonto2.Text = this.txtMonto2.Text + "0";
                    }
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                string aux = this.txtMonto1.Text;
                if (aux.Length > 0)
                {
                    this.txtMonto1.Text = aux.Substring(0, aux.Length - 1);
                }
                if (aux.Length == 1)
                {
                    this.lblError1.Text = "";
                }
            }
            else
            {
                if (text == 2)
                {
                    string aux = this.txtMonto2.Text;
                    if (aux.Length > 0)
                    {
                        this.txtMonto2.Text = aux.Substring(0, aux.Length - 1);
                    }
                    if (aux.Length == 1)
                    {
                        this.lblError1.Text = "";
                    }
                }
            }
        }

        private void btnBorrarTodo_Click(object sender, EventArgs e)
        {
            txtMonto1.Text = "";
            txtMonto2.Text = "";

            lblError1.Text = "";
        }

        private void picBtnAgregar_Click(object sender, EventArgs e)
        {

            ///////////////////////////// Agregado: Modificado para cargar billetes en una sola gaveta si asi lo desea/////////////////////////////////////////////
            try
            {

                if (txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text == "" && cmbDen2.Text == "" || txtMonto1.Text == "" && cmbDen1.Text == "" && txtMonto2.Text != "" && cmbDen2.Text != ""
                    || txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text != "" && cmbDen2.Text != "")
                {
                    int gav1 = Convert.ToInt32(cmbDen1.SelectedItem);
                    int gav2 = Convert.ToInt32(cmbDen2.SelectedItem);
                    if (txtMonto1.Text == "") txtMonto1.Text = "0";  /////  la variable Monto1 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null/// 
                    if (txtMonto2.Text == "") txtMonto2.Text = "0"; /////  la variable Monto2 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null///
                    string monto1 = txtMonto1.Text;
                    string monto2 = txtMonto2.Text;

                    ConfirmarBalanceo confirmar = new ConfirmarBalanceo(nombrearchivo, gav1, gav2, monto1, monto2, dni, client);
                    confirmar.ShowDialog();
                    //picBtnAgregar.Focus();


                    this.Close();

                }
                else
                {
                    lblError1.Text = "Debe completar todos los campos.";
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
                    //  Stream stream = client.GetStream();
                    NetworkStream stream = client.GetStream();

                    // Send the message to the connected TcpServer. 
                    stream.Write(data, 0, data.Length);
                    //Console.WriteLine("Sent: {0}", message);

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
            int inicio2 = message.IndexOf("<message>") + "<message>".Length;                                                                    // guardamos en inicio desde donde aparece la palabra amount en el mensaje que nos devuelve pagos, y le sumamos la cantidad de  caracteres que contiene <amount> para saber justo donde empieza el monto 
            int fin2 = message.IndexOf("</message>");                                                                                        // guardamos en fin desde donde empieza la palabra </amount> para saber donde termina el monto.
            string msj = message.Substring(inicio2, fin2 - inicio2);
            string messag = codigo + msj;
            return codigo;
        }

        private void Balanceo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnProcesar.PerformClick();
            }
        }

        private void txtMonto1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    if (txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text == "" && cmbDen2.Text == "" || txtMonto1.Text == "" && cmbDen1.Text == "" && txtMonto2.Text != "" && cmbDen2.Text != ""
                  || txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text != "" && cmbDen2.Text != "")
                    {
                        int gav1 = Convert.ToInt32(cmbDen1.SelectedItem);
                        int gav2 = Convert.ToInt32(cmbDen2.SelectedItem);
                        if (txtMonto1.Text == "") txtMonto1.Text = "0";  /////  la variable Monto1 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null/// 
                        if (txtMonto2.Text == "") txtMonto2.Text = "0"; /////  la variable Monto2 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null///
                        string monto1 = txtMonto1.Text;
                        string monto2 = txtMonto2.Text;

                        ConfirmarBalanceo confirmar = new ConfirmarBalanceo(nombrearchivo, gav1, gav2, monto1, monto2, dni, client);
                        confirmar.ShowDialog();
                        //picBtnAgregar.Focus();


                        this.Close();

                    }
                    else
                    {
                        lblError1.Text = "Debe completar todos los campos.";
                    }
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

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                ///////////////////////////// Agregado: Modificado para cargar billetes en una sola gaveta si asi lo desea/////////////////////////////////////////////

                if (txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text == "" && cmbDen2.Text == "" || txtMonto1.Text == "" && cmbDen1.Text == "" && txtMonto2.Text != "" && cmbDen2.Text != ""
                    || txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text != "" && cmbDen2.Text != "")
                {
                    int gav1 = Convert.ToInt32(cmbDen1.SelectedItem);
                    int gav2 = Convert.ToInt32(cmbDen2.SelectedItem);
                    if (txtMonto1.Text == "") txtMonto1.Text = "0";  /////  la variable Monto1 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null/// 
                    if (txtMonto2.Text == "") txtMonto2.Text = "0"; /////  la variable Monto2 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null///
                    string monto1 = txtMonto1.Text;
                    string monto2 = txtMonto2.Text;

                    ConfirmarBalanceo confirmar = new ConfirmarBalanceo(nombrearchivo, gav1, gav2, monto1, monto2, dni, client);
                    confirmar.ShowDialog();
                    //picBtnAgregar.Focus();


                    this.Close();

                }
                else
                {
                    lblError1.Text = "Debe completar todos los campos.";
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

        private void cmbDen1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    if (txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text == "" && cmbDen2.Text == "" || txtMonto1.Text == "" && cmbDen1.Text == "" && txtMonto2.Text != "" && cmbDen2.Text != ""
                  || txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text != "" && cmbDen2.Text != "")
                    {
                        int gav1 = Convert.ToInt32(cmbDen1.SelectedItem);
                        int gav2 = Convert.ToInt32(cmbDen2.SelectedItem);
                        if (txtMonto1.Text == "") txtMonto1.Text = "0";  /////  la variable Monto1 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null/// 
                        if (txtMonto2.Text == "") txtMonto2.Text = "0"; /////  la variable Monto2 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null///
                        string monto1 = txtMonto1.Text;
                        string monto2 = txtMonto2.Text;

                        ConfirmarBalanceo confirmar = new ConfirmarBalanceo(nombrearchivo, gav1, gav2, monto1, monto2, dni, client);
                        confirmar.ShowDialog();
                        //picBtnAgregar.Focus();


                        this.Close();

                    }
                    else
                    {
                        lblError1.Text = "Debe completar todos los campos.";
                    }
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

        private void txtMonto2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text == "" && cmbDen2.Text == "" || txtMonto1.Text == "" && cmbDen1.Text == "" && txtMonto2.Text != "" && cmbDen2.Text != ""
              || txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text != "" && cmbDen2.Text != "")
                {
                    int gav1 = Convert.ToInt32(cmbDen1.SelectedItem);
                    int gav2 = Convert.ToInt32(cmbDen2.SelectedItem);
                    if (txtMonto1.Text == "") txtMonto1.Text = "0";  /////  la variable Monto1 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null/// 
                    if (txtMonto2.Text == "") txtMonto2.Text = "0"; /////  la variable Monto2 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null///
                    string monto1 = txtMonto1.Text;
                    string monto2 = txtMonto2.Text;

                    ConfirmarBalanceo confirmar = new ConfirmarBalanceo(nombrearchivo, gav1, gav2, monto1, monto2, dni, client);
                    confirmar.ShowDialog();
                    this.Close();

                }
                else
                {
                    lblError1.Text = "Debe completar todos los campos.";
                }
            }
        }

        private void cmbDen2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbDen2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    if (txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text == "" && cmbDen2.Text == "" || txtMonto1.Text == "" && cmbDen1.Text == "" && txtMonto2.Text != "" && cmbDen2.Text != ""
                  || txtMonto1.Text != "" && cmbDen1.Text != "" && txtMonto2.Text != "" && cmbDen2.Text != "")
                    {
                        int gav1 = Convert.ToInt32(cmbDen1.SelectedItem);
                        int gav2 = Convert.ToInt32(cmbDen2.SelectedItem);
                        if (txtMonto1.Text == "") txtMonto1.Text = "0";  /////  la variable Monto1 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null/// 
                        if (txtMonto2.Text == "") txtMonto2.Text = "0"; /////  la variable Monto2 necesita que su contenido sea un string , para eso se agrega en numero 0, de lo contario sera null///
                        string monto1 = txtMonto1.Text;
                        string monto2 = txtMonto2.Text;

                        ConfirmarBalanceo confirmar = new ConfirmarBalanceo(nombrearchivo, gav1, gav2, monto1, monto2, dni, client);
                        confirmar.ShowDialog();
                        //picBtnAgregar.Focus();


                        this.Close();

                    }
                    else
                    {
                        lblError1.Text = "Debe completar todos los campos.";
                    }
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
