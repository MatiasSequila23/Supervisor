using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ENTIDAD;
using NEGOCIO;
using System.Net.Sockets;

namespace Supervisor
{
    public partial class Supervisor : Form
    {
        TcpClient client = new TcpClient();

        string nombrearchivo = DateTime.Now.ToString("dd MMMM yyyy") + ".csv";                                                                  // declaramos una variable string nombrearchivo y le guardamos la fecha de hoy mas la extension .csv, que es el nombre que vamos ausar para guardar los archivos logs por fechas

        public List<ENTIDAD.Supervisores> usuarios;
        int text = 0;
        public Supervisor(TcpClient cliente)
        {
            InitializeComponent();

            this.client = cliente;

            string ruta = Directory.GetCurrentDirectory() + "\\utils\\Usuarios.csv";                                                           // se lee el archivo que contiene a los usuarios
            List<string[]> listado = Archivos.parseCSV(ruta);                                                                                  // se guarda en una lista de string los usuarios dentro del archivo.

            bool flag = Supervisores.parseo(listado, out List<Supervisores> usuariosAux);     
            if (flag == true)
            {
                this.usuarios = usuariosAux;
            }
        
        }
    
        /// ////////////////////  BOTONES PARA ESCRIBIR CON TECLADO TACTIL ///////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            if(text == 1)
            {
                if (this.txtDNI.Text.Length < 8)
                {
                    this.txtDNI.Text = this.txtDNI.Text + "1";
                }
            }
            else
            {
                if(text == 2)
                {
                      if (this.txtPassword.Text.Length < 11)
            {
                this.txtPassword.Text = this.txtPassword.Text + "1";
            }
                }
            }       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtDNI.Text.Length < 8)
                {
                    this.txtDNI.Text = this.txtDNI.Text + "2";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtPassword.Text.Length < 11)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "2";
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtDNI.Text.Length < 8)
                {
                    this.txtDNI.Text = this.txtDNI.Text + "3";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtPassword.Text.Length < 11)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "3";
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtDNI.Text.Length < 8)
                {
                    this.txtDNI.Text = this.txtDNI.Text + "4";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtPassword.Text.Length < 11)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "4";
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtDNI.Text.Length < 8)
                {
                    this.txtDNI.Text = this.txtDNI.Text + "5";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtPassword.Text.Length < 11)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "5";
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtDNI.Text.Length < 8)
                {
                    this.txtDNI.Text = this.txtDNI.Text + "6";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtPassword.Text.Length < 11)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "6";
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtDNI.Text.Length < 8)
                {
                    this.txtDNI.Text = this.txtDNI.Text + "7";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtPassword.Text.Length < 11)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "7";
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtDNI.Text.Length < 8)
                {
                    this.txtDNI.Text = this.txtDNI.Text + "8";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtPassword.Text.Length < 11)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "8";
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtDNI.Text.Length < 8)
                {
                    this.txtDNI.Text = this.txtDNI.Text + "9";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtPassword.Text.Length < 11)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "9";
                    }
                }
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (text == 1)
            {
                if (this.txtDNI.Text.Length < 8)
                {
                    this.txtDNI.Text = this.txtDNI.Text + "0";
                }
            }
            else
            {
                if (text == 2)
                {
                    if (this.txtPassword.Text.Length < 11)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "0";
                    }
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if(text == 1)
            {
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
            else
            {
                if(text == 2)
                {
                    string aux = this.txtPassword.Text;
                    if (aux.Length > 0)
                    {
                        this.txtPassword.Text = aux.Substring(0, aux.Length - 1);
                    }
                    if (aux.Length == 1)
                    {
                        this.lblError.Text = "";
                    }
                }
            }          
        }

        private void btnBorrarTodo_Click(object sender, EventArgs e)
        {
            this.txtPassword.Text = "";
            this.txtDNI.Text = "";
            this.lblError.Text = "";
        }

        private void txtDNI_Leave(object sender, EventArgs e)
        {
            text = 1;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            text = 2;
        }

        private void Supervisor_Load(object sender, EventArgs e)
        {
            txtDNI.Focus();
            Cursor.Hide();

            timer1.Enabled = true;
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString(" dd MMMM yyyy ");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString(" dd MMMM yyyy ");
        }

        public void EscribirLogsyTransaccion(DateTime fecha, string esi, string ntransaccion, string datos, string esi2 )
        {
            try
            {

                Archivos.EscribeArchivoLogs(fecha, esi, ntransaccion, datos, nombrearchivo);                                                                              // Escribe en el archivo que el dni no esta registrado
                string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                            //leemos el numero de transaccion desde el archivo.                     
                datos = "Fin Transaccion";
                Archivos.EscribeArchivoLogs(fecha, esi2, ntransaccion, datos, nombrearchivo);                                                                                      // Escribe en el archivo fin de transaccion
                bool flag2 = int.TryParse(data, out int idTrucha);                                                                                                 // lo que guardo data, lo pasamos a int para sumar.
                idTrucha++;                                                                                                                                        // sumamos uno a idtrucha, que sera el proximo id a guardar.
                string id_transaccion = idTrucha.ToString("D6");                                                                                                   // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                         // borramos la ultima fila de transaccion
                Archivos.GuardadoTransaccion(id_transaccion);
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


       
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "\\Logs\\" + nombrearchivo) != true)                                                          // cuando apretamos el boton ingresar,preguntamos si el archivo con el nombre de la fecha del dia existe.
                {

                    TextWriter txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Logs\\" + nombrearchivo);                                       // si no existe el archivo, lo creamos en la carpeta logs de nombre la fecha de hoy.
                    txtWriter.Close();                                                                                                                          // cerramos el archivo creado
                    Archivos.GuardadoTransaccion("000001");                                                                                                     // reiniciamos a 1 el contador de transacciones.        

                }

                string datos;

                string ntransaccion;

                if (txtDNI.Text != "" && txtPassword.Text != "")
                {
                    ntransaccion = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                      //leemos el numero de transaccion desde el archivo.  

                    datos = "Inicio Transaccion";
                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion, datos, nombrearchivo);                                                                                // guardamos en el archivo el inicio de la transaccion


                    bool flag = Validaciones.ValidarQueSeaDni(this.txtDNI.Text, out string dni);                                                                         // validamos que el dni sea un numero entre 7 y 8 caracteres.

                    Archivos.EscribeArchivoLogs(DateTime.Now, "S", ntransaccion, dni, nombrearchivo);                                                                       // Escribe en el archivo que cargo el usuario de forma correcta.

                    string pass = txtPassword.Text;

                    if (flag == true)
                    {
                        if (Validaciones.UsuarioExiste(this.usuarios, dni))                                                                                              // validamos que el usuario exista, si existe se inicia el form sesion iniciada.
                        {
                            if (Validaciones.PassExiste(this.usuarios, dni, pass))
                            {
                                // DESBLOQUEO DE GAVETAS ///

                                string message = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><startexchg/>";
                                string mensaje = Connect("127.0.0.1", message);
                                var msjcompl = SEGURIDAD.Seguridad.GetInstancia().Controller2(mensaje);
                                int CodigoError = Convert.ToInt32(msjcompl.Item1);
                                string MensError = msjcompl.Item2;
                                if (CodigoError != 0)
                                {
                                    string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                                    datos = "Error Desbloqueo De Cartuchos";
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "F", data, datos, nombrearchivo);
                                    SEGURIDAD.Log.GetInstancia().CrearLogErrorDisp(MensError);
                                    FormError error = new FormError();
                                    error.ShowDialog();
                                }

                                SesionIniciada se = new SesionIniciada(client);
                                se.CargarDatos(this.usuarios, dni);                                                                                                          // se cargan los datos en el form sesion iniciada.
                                se.ShowDialog();
                                this.lblError.Text = "";
                                this.txtDNI.Text = "";
                                txtPassword.Text = "";

                            }
                            else
                            {
                                lblError.Text = "CONTRASEÑA INCORRECTA.";
                                datos = dni + " Contraseña incorrecta.";
                                string esi = "E";
                                string esi2 = "I";
                                EscribirLogsyTransaccion(DateTime.Now, esi, ntransaccion, datos, esi2);

                            }

                        }
                        else
                        {
                            this.lblError.Text = "ESTE DNI NO ESTA REGISTRADO.";
                            datos = dni + " No registrado.";
                            string esi = "E";
                            string esi2 = "I";
                            EscribirLogsyTransaccion(DateTime.Now, esi, ntransaccion, datos, esi2);

                        }
                    }
                    else
                    {
                        this.lblError.Text = "DNI INCORRECTO.";
                    }
                }
                else
                {
                    lblError.Text = "DEBE COMPLETAR TODOS LOS CAMPOS.";
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

        private void Supervisor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnIngresar.PerformClick();
            }
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            string dni = txtDNI.Text;

            if(dni.Length == 8)
            {
                txtPassword.Focus();
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                try
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\Logs\\" + nombrearchivo) != true)                                                          // cuando apretamos el boton ingresar,preguntamos si el archivo con el nombre de la fecha del dia existe.
                    {

                        TextWriter txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Logs\\" + nombrearchivo);                                       // si no existe el archivo, lo creamos en la carpeta logs de nombre la fecha de hoy.
                        txtWriter.Close();                                                                                                                          // cerramos el archivo creado
                        Archivos.GuardadoTransaccion("000001");                                                                                                     // reiniciamos a 1 el contador de transacciones.        

                    }

                    string datos;

                    string ntransaccion;

                    if (txtDNI.Text != "" && txtPassword.Text != "")
                    {
                        ntransaccion = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                      //leemos el numero de transaccion desde el archivo.  

                        datos = "Inicio Transaccion";
                        Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion, datos, nombrearchivo);                                                                                // guardamos en el archivo el inicio de la transaccion


                        bool flag = Validaciones.ValidarQueSeaDni(this.txtDNI.Text, out string dni);                                                                         // validamos que el dni sea un numero entre 7 y 8 caracteres.

                        Archivos.EscribeArchivoLogs(DateTime.Now, "S", ntransaccion, dni, nombrearchivo);                                                                       // Escribe en el archivo que cargo el usuario de forma correcta.

                        string pass = txtPassword.Text;

                        if (flag == true)
                        {
                            if (Validaciones.UsuarioExiste(this.usuarios, dni))                                                                                              // validamos que el usuario exista, si existe se inicia el form sesion iniciada.
                            {
                                if (Validaciones.PassExiste(this.usuarios, dni, pass))
                                {
                                    // DESBLOQUEO DE GAVETAS ///

                                    string message = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><startexchg/>";
                                    string mensaje = Connect("127.0.0.1", message);
                                    var msjcompl = SEGURIDAD.Seguridad.GetInstancia().Controller2(mensaje);
                                    int CodigoError = Convert.ToInt32(msjcompl.Item1);
                                    string MensError = msjcompl.Item2;
                                    if (CodigoError != 0)
                                    {
                                        string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                                        datos = "Error Desbloqueo De Cartuchos";
                                        Archivos.EscribeArchivoLogs(DateTime.Now, "F", data, datos, nombrearchivo);
                                        SEGURIDAD.Log.GetInstancia().CrearLogErrorDisp(MensError);
                                        FormError error = new FormError();
                                        error.ShowDialog();
                                    }




                                    SesionIniciada se = new SesionIniciada(client);
                                    se.CargarDatos(this.usuarios, dni);                                                                                                          // se cargan los datos en el form sesion iniciada.
                                    se.ShowDialog();
                                    this.lblError.Text = "";
                                    this.txtDNI.Text = "";
                                    txtPassword.Text = "";

                                }
                                else
                                {
                                    lblError.Text = "CONTRASEÑA INCORRECTA.";
                                    datos = dni + " Contraseña incorrecta.";
                                    string esi = "E";
                                    string esi2 = "I";
                                    EscribirLogsyTransaccion(DateTime.Now, esi, ntransaccion, datos, esi2);

                                }

                            }
                            else
                            {
                                this.lblError.Text = "ESTE DNI NO ESTA REGISTRADO.";
                                datos = dni + " No registrado.";
                                string esi = "E";
                                string esi2 = "I";
                                EscribirLogsyTransaccion(DateTime.Now, esi, ntransaccion, datos, esi2);

                            }
                        }
                        else
                        {
                            this.lblError.Text = "DNI INCORRECTO.";
                        }
                    }
                    else
                    {
                        lblError.Text = "DEBE COMPLETAR TODOS LOS CAMPOS.";
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

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                try
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\Logs\\" + nombrearchivo) != true)                                                          // cuando apretamos el boton ingresar,preguntamos si el archivo con el nombre de la fecha del dia existe.
                    {

                        TextWriter txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Logs\\" + nombrearchivo);                                       // si no existe el archivo, lo creamos en la carpeta logs de nombre la fecha de hoy.
                        txtWriter.Close();                                                                                                                          // cerramos el archivo creado
                        Archivos.GuardadoTransaccion("000001");                                                                                                     // reiniciamos a 1 el contador de transacciones.        

                    }

                    string datos;

                    string ntransaccion;

                    if (txtDNI.Text != "" && txtPassword.Text != "")
                    {
                        ntransaccion = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                      //leemos el numero de transaccion desde el archivo.  

                        datos = "Inicio Transaccion";
                        Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion, datos, nombrearchivo);                                                                                // guardamos en el archivo el inicio de la transaccion


                        bool flag = Validaciones.ValidarQueSeaDni(this.txtDNI.Text, out string dni);                                                                         // validamos que el dni sea un numero entre 7 y 8 caracteres.

                        Archivos.EscribeArchivoLogs(DateTime.Now, "S", ntransaccion, dni, nombrearchivo);                                                                       // Escribe en el archivo que cargo el usuario de forma correcta.

                        string pass = txtPassword.Text;

                        if (flag == true)
                        {
                            if (Validaciones.UsuarioExiste(this.usuarios, dni))                                                                                              // validamos que el usuario exista, si existe se inicia el form sesion iniciada.
                            {
                                if (Validaciones.PassExiste(this.usuarios, dni, pass))
                                {
                                    // DESBLOQUEO DE GAVETAS ///

                                    string message = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><startexchg/>";
                                    string mensaje = Connect("127.0.0.1", message);
                                    var msjcompl = SEGURIDAD.Seguridad.GetInstancia().Controller2(mensaje);
                                    int CodigoError = Convert.ToInt32(msjcompl.Item1);
                                    string MensError = msjcompl.Item2;
                                    if (CodigoError != 0)
                                    {
                                        string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                                        datos = "Error Desbloqueo De Cartuchos";
                                        Archivos.EscribeArchivoLogs(DateTime.Now, "F", data, datos, nombrearchivo);
                                        SEGURIDAD.Log.GetInstancia().CrearLogErrorDisp(MensError);
                                        FormError error = new FormError();
                                        error.ShowDialog();
                                    }




                                    SesionIniciada se = new SesionIniciada(client);
                                    se.CargarDatos(this.usuarios, dni);                                                                                                          // se cargan los datos en el form sesion iniciada.
                                    se.ShowDialog();
                                    this.lblError.Text = "";
                                    this.txtDNI.Text = "";
                                    txtPassword.Text = "";

                                }
                                else
                                {
                                    lblError.Text = "CONTRASEÑA INCORRECTA.";
                                    datos = dni + " Contraseña incorrecta.";
                                    string esi = "E";
                                    string esi2 = "I";
                                    EscribirLogsyTransaccion(DateTime.Now, esi, ntransaccion, datos, esi2);

                                }

                            }
                            else
                            {
                                this.lblError.Text = "ESTE DNI NO ESTA REGISTRADO.";
                                datos = dni + " No registrado.";
                                string esi = "E";
                                string esi2 = "I";
                                EscribirLogsyTransaccion(DateTime.Now, esi, ntransaccion, datos, esi2);

                            }
                        }
                        else
                        {
                            this.lblError.Text = "DNI INCORRECTO.";
                        }
                    }
                    else
                    {
                        lblError.Text = "DEBE COMPLETAR TODOS LOS CAMPOS.";
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
}
