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
using ENTIDAD;
using NEGOCIO;

namespace Supervisor
{ 
    public partial class SesionIniciada : Form
    {
        public bool bloqueo = false;

        public static int BanderaError;

        TcpClient client = new TcpClient();

        public static SesionIniciada ini;

        private int counter = 240;                                                        
        
        private List<Supervisores> usuarios; 
        private string seleccionado;
        string dni;

        Supervisores usu = new Supervisores(null,null,null,null);

        string nombrearchivo = DateTime.Now.ToString("dd MMMM yyyy") + ".csv";

        public SesionIniciada( TcpClient cliente)
        {
            this.client = cliente;

            InitializeComponent();

            SesionIniciada.ini = this;
        }



        private void SesionIniciada_Load(object sender, EventArgs e)
        {
            timer3.Enabled = true;
          

            Cursor.Hide();                                                                                                                         // escondemos el cursor para que no se vea en la pantalla tactil
            timer1.Enabled = true;                                                                                                                           // habilitamos el timer1 que es el de la hora

            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString(" dd MMMM yyyy ");

            panelBalanceo.Visible = false;                                                                                                                   // mostramos el panel de balanceo cuando inicia el form   
            panelBlanqueo.Visible = false;                                                                                                                  // ocultamos el panel de blanqueo cuando inicia el form

            lineBalanceo.Visible = false;                                                                                                                    //  mostramos este panel que es la linea que aparece bajo el boton de balanco
            lineBlanqueo.Visible = false;                                                                                                                   //  ocultamos este panel que es la linea que aparece bajo el boton de balanco


           
            lblError.Text = "";

            txtNombre.Text = "";
            txtLegajo.Text = "";

            timer2 = new System.Windows.Forms.Timer();
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Interval = 1000; // 1 second
            timer2.Start();
            txtCountDown.Text = counter.ToString();

        }

        public void CargarDatos(List<Supervisores> nomina, string dni)
        {
            this.usuarios = nomina;                                                                                                                            // se recibe la lista de usuarios
            this.seleccionado = dni;                                                                                                                           // se recibe el dni que se escribio en el textbox para iniciar sesion
            this.llenaDatos();                                                                                                                                 // llamammos a la funcion llenar datos
        }


        public void llenaDatos()
        {
            string ntransaccion = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                    // lee el numero de transaccion desde el archivo.

            foreach (Supervisores item in this.usuarios)                                                                                                       // se busca dentro del foreach que el dni del textbox sea igual a alguno de la lista.
            {
                if (item.Dni == this.seleccionado)                                                                                                             // si esta dentro de la lista, se muestra en un label y se bloquean o no botones segun sea necesario.
                {
                    this.txtDni.Text = item.Dni;
                    dni = item.Dni;


                    string datos = dni + ";Supervisor; Clave OK";

                    Archivos.EscribeArchivoLogs(DateTime.Now, "E", ntransaccion, datos, nombrearchivo);                                                                       // Escribe en el archivo que cargo el usuario de forma correcta.


                    //Seteamos los botones segun el tipo de acceso que tenga cada dni.

                    if (item.Balanceo == "1")
                    {
                        picBtnBalanceo.Visible = true;
                    }
                    else
                    {
                        picBtnBalanceo.Visible = false;
                    }

                    if (item.Clave == "1.")
                    {
                        picBtnBlanqueo.Visible = true;
                    }
                    else
                    {
                        picBtnBlanqueo.Visible = false;
                    }

                    break;
                }
            }
        }

       

      

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            timer2.Dispose();                                                                                                                          // liberamos el timer2, que es el de la cuenta regresiva
            this.Close();                                                                                                                              // cerramos el form

            string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                   //leemos el numero de transaccion desde el archivo.                      
            
            string datos = "Fin Transaccion";
            Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                                                               // Escribe en el archivo fin de transaccion
            bool flag = int.TryParse(data, out int idTrucha);                                                                                          // lo que guardo data, lo pasamos a int para sumar.
            idTrucha++;                                                                                                                                // sumamos uno a idtrucha, que sera el proximo id a guardar.
            string id_transaccion = idTrucha.ToString("D6");                                                                                           // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
            Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                 // borramos la ultima fila de transaccion
            Archivos.GuardadoTransaccion(id_transaccion);                                                                                              // la volvemos a crear con el nuevo numero de transaccion.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString(" dd MMMM yyyy ");
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";                                                                                                                    // ponemos el label lblError como vacio.

                Usuarios usu = new Usuarios();                                                                                                        // declaramos un usuario usu

                if (txtBuscarDni.Text != "")                                                                                                           // preguntamos si el txtBuscarDni es distinto de vacio. Para validar que hayamos ingresado algo en el textbox
                {
                    usu = negUsuarios.BuscarUsuario(txtBuscarDni.Text);                                                                                 // llamamos a la funcion buscar usuario y enviamos el dni. La funcion BuscarUsuario nos trae el usuario con ese dni de la base de datos.
                    if (usu != null)                                                                                                                    // preguntamos si usu es distinto de null.
                    {

                        txtLegajo.Text = usu.legajo;                                                                                                    // si es distinto de null, en el txtLegajo mostramos el legajo del usuario  
                        txtNombre.Text = usu.Nombre;                                                                              // en el txtNombre mostramos el nombre y apellido del usuario.
                    }
                    else
                    {
                        lblError.Text = "Usuario inexistente.";                                                                                         // si usu es null, mostramos por el lblError que el usuario es inexistente
                    }
                }
                else
                {
                    lblError.Text = "Complete todos los campos.";                                                                                       // si el if de la linea 234, es false, quiere decir que no se ingreso nada en el textbox, mostramos en el lblError complete todos los campos.
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "")                                                                                           // preguntamos si el txtNombre es distinto de vacio. Asi sabemos si trajo o no algun usuario desde la base
            {

                Usuarios usu = new Usuarios();                                                                                  // creamos un usuario usu nuevo

                string dni = txtBuscarDni.Text;

                string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");        //leemos el numero de transaccion desde el archivo.                      
                string datos = dni + " a modificar contraseña.";
                Archivos.EscribeArchivoLogs(DateTime.Now, "S", data, datos, nombrearchivo);


                if (txtBuscarDni.Text != "")                                                                                         // nos preguntamos si el txtDni es distinto de vacio
                {
                    usu = negUsuarios.BuscarUsuario(dni);                                                                      // buscamos el usuario que queremos modificar el offset y le enviamos como parametro el dni.

                    if (usu != null)                                                                                            // nos preguntamos si el usuario es distinto de null
                    {
                        if (negUsuarios.ModificarOffset(usu.legajo) == 1)                                                           // si es distinto de null, va a enviar el dni para setear el offset a 0000 en la BD y asi poder restablecer la contraseña.
                        {
                            lblError.Text = "CONTRASEÑA RESTABLECIDA.";

                            string datos2 = dni + " contraseña modificada. Offset 0000";
                            Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos2, nombrearchivo);                                      // escribimos en el archivo que la contraseña fue modificada.

                        }
                        else
                        {
                            lblError.Text = "Error al modificar offset.";

                            string datos2 = dni + " ERROR AL MODIFICAR CONTRASEÑA";
                            Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                        // escribimos en el archivo que hubo un error al modificar la contraseña.
                        }
                    }
                    else
                    {
                        lblError.Text = "DNI INCORRECTO O INEXISTENTE.";

                        string datos2 = dni + " DNI inexistente";
                        Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                            // escribimos en el archivo que el dni es inexistente.
                    }

                }
                else
                {
                    lblError.Text = "DEBE INGRESAR UN DNI.";
                }

                BorrarLabels();
                txtBuscarDni.Text = "";
            }
            else
            {
                lblError.Text = "No hay usuario seleccionado.";
            }
        }

        private void BorrarLabels()
        {
           
            txtNombre.Text = "";
            txtLegajo.Text = "";
        }


        /// //////////////////////////////////////////////////////////////           INICIO BOTONERA BLANQUEO DE CLAVES       ///////////////////////////////////////////////
        /// 


        private void button21_Click(object sender, EventArgs e)
        {
            if (this.txtBuscarDni.Text.Length < 8)
            {
                this.txtBuscarDni.Text = this.txtBuscarDni.Text + "1";
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (this.txtBuscarDni.Text.Length < 8)
            {
                this.txtBuscarDni.Text = this.txtBuscarDni.Text + "2";
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (this.txtBuscarDni.Text.Length < 8)
            {
                this.txtBuscarDni.Text = this.txtBuscarDni.Text + "3";
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (this.txtBuscarDni.Text.Length < 8)
            {
                this.txtBuscarDni.Text = this.txtBuscarDni.Text + "4";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (this.txtBuscarDni.Text.Length < 8)
            {
                this.txtBuscarDni.Text = this.txtBuscarDni.Text + "5";
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (this.txtBuscarDni.Text.Length < 8)
            {
                this.txtBuscarDni.Text = this.txtBuscarDni.Text + "6";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (this.txtBuscarDni.Text.Length < 8)
            {
                this.txtBuscarDni.Text = this.txtBuscarDni.Text + "7";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (this.txtBuscarDni.Text.Length < 8)
            {
                this.txtBuscarDni.Text = this.txtBuscarDni.Text + "8";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (this.txtBuscarDni.Text.Length < 8)
            {
                this.txtBuscarDni.Text = this.txtBuscarDni.Text + "9";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (this.txtBuscarDni.Text.Length < 8)
            {
                this.txtBuscarDni.Text = this.txtBuscarDni.Text + "0";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string aux = this.txtBuscarDni.Text;
            if (aux.Length > 0)
            {
                this.txtBuscarDni.Text = aux.Substring(0, aux.Length - 1);
            }
            if (aux.Length == 1)
            {
                this.lblError.Text = "";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.txtBuscarDni.Text = "";
            this.lblError.Text = "";
        }



        /// //////////////////////////////////////////////////////////////           FIN BOTONERA DE BLANQUEO DE CLAVES       ///////////////////////////////////////////////
        /// 


        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {

                counter--;

                if (counter == 0 && BanderaError==0)
                {
                    string datos = "";
                    string ntransaccion2 = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                    datos = "Error Timeout - Sesión Cerrada.";
                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion2, datos, nombrearchivo);
                    datos = "Fin Transaccion.";
                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion2, datos, nombrearchivo);

                    bool flag = int.TryParse(ntransaccion2, out int idTrucha);                                                                          // lo que guardo data, lo pasamos a int para sumar.
                    idTrucha++;                                                                                                                         // sumamos uno a idtrucha, que sera el proximo id a guardar.
                    string id_transaccion = idTrucha.ToString("D6");                                                                                    // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                    Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                          // borramos la ultima fila de transaccion
                    Archivos.GuardadoTransaccion(id_transaccion);

                    timer2.Dispose();

                    ErrorTimeout error = new ErrorTimeout();
                    error.ShowDialog();
                }
                else
                {
                    if (counter==0 && BanderaError==1)
                    {
                        timer2.Enabled = false;
                        string datos = "";
                        string ntransaccion2 = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                        datos = "Fin Transaccion.";
                        Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion2, datos, nombrearchivo);
                    }
                  
                }
                txtCountDown.Text = counter.ToString();
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

        private void picBtnBalanceo_Click(object sender, EventArgs e)
        {
            lineBalanceo.Visible = true;                                                                                                              //  mostramos la linea que aparece bajo el boton de balanco
            lineBlanqueo.Visible = false;
            panelBalanceo.Visible = true;                                                                                                             // mostramos el panel que corresponde
            panelBlanqueo.Visible = false;

        }

        private void picBtnBlanqueo_Click(object sender, EventArgs e)
        {
            lineBalanceo.Visible = false;                                                                                                             //  mostramos la linea que aparece bajo el boton de balanco
            lineBlanqueo.Visible = true;
            panelBalanceo.Visible = false;
            panelBlanqueo.Visible = true;
        }

        private void picBtnRestablecer_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtNombre.Text != "")                                                                                           // preguntamos si el txtNombre es distinto de vacio. Asi sabemos si trajo o no algun usuario desde la base
                {

                    Usuarios usu = new Usuarios();                                                                                  // creamos un usuario usu nuevo

                    string dni = txtBuscarDni.Text;

                    string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");        //leemos el numero de transaccion desde el archivo.                      
                    string datos = dni + " a modificar contraseña.";
                    Archivos.EscribeArchivoLogs(DateTime.Now, "S", data, datos, nombrearchivo);


                    if (txtBuscarDni.Text != "")                                                                                         // nos preguntamos si el txtDni es distinto de vacio
                    {
                        usu = negUsuarios.BuscarUsuario(dni);                                                                      // buscamos el usuario que queremos modificar el offset y le enviamos como parametro el dni.

                        if (usu != null)                                                                                            // nos preguntamos si el usuario es distinto de null
                        {
                            if (negUsuarios.ModificarOffset(usu.legajo) == 1)                                                           // si es distinto de null, va a enviar el dni para setear el offset a 0000 en la BD y asi poder restablecer la contraseña.
                            {
                                lblError.Text = "CONTRASEÑA RESTABLECIDA.";

                                string datos2 = dni + " contraseña modificada. Offset 0000";
                                Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos2, nombrearchivo);                                      // escribimos en el archivo que la contraseña fue modificada.

                            }
                            else
                            {
                                lblError.Text = "Error al modificar offset.";

                                string datos2 = dni + " ERROR AL MODIFICAR CONTRASEÑA";
                                Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                        // escribimos en el archivo que hubo un error al modificar la contraseña.
                            }
                        }
                        else
                        {
                            lblError.Text = "DNI INCORRECTO O INEXISTENTE.";

                            string datos2 = dni + " DNI inexistente";
                            Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                            // escribimos en el archivo que el dni es inexistente.
                        }

                    }
                    else
                    {
                        lblError.Text = "DEBE INGRESAR UN DNI.";
                    }

                    BorrarLabels();
                    txtBuscarDni.Text = "";
                }
                else
                {
                    lblError.Text = "No hay usuario seleccionado.";
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

      

        private void pictureBox1_Click(object sender, EventArgs e)
        {         
                FormBalanceo bal = new FormBalanceo(txtDni.Text, client);
                bal.ShowDialog();           
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
                    BanderaError = 1;
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

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";                                                                                                                    // ponemos el label lblError como vacio.

                Usuarios usu = new Usuarios();                                                                                                        // declaramos un usuario usu

                if (txtBuscarDni.Text != "")                                                                                                           // preguntamos si el txtBuscarDni es distinto de vacio. Para validar que hayamos ingresado algo en el textbox
                {
                    usu = negUsuarios.BuscarUsuario(txtBuscarDni.Text);                                                                                 // llamamos a la funcion buscar usuario y enviamos el dni. La funcion BuscarUsuario nos trae el usuario con ese dni de la base de datos.
                    if (usu != null)                                                                                                                    // preguntamos si usu es distinto de null.
                    {

                        txtLegajo.Text = usu.legajo;                                                                                                    // si es distinto de null, en el txtLegajo mostramos el legajo del usuario  
                        txtNombre.Text = usu.Nombre;                                                                              // en el txtNombre mostramos el nombre y apellido del usuario.
                    }
                    else
                    {
                        lblError.Text = "Usuario inexistente.";                                                                                         // si usu es null, mostramos por el lblError que el usuario es inexistente
                    }
                }
                else
                {
                    lblError.Text = "Complete todos los campos.";                                                                                       // si el if de la linea 234, es false, quiere decir que no se ingreso nada en el textbox, mostramos en el lblError complete todos los campos.
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

        private void btnAtras_Click(object sender, EventArgs e)
        {

            try
            {

                if (bloqueo == true)
                {
                    timer2.Dispose();                                                                                                                          // liberamos el timer2, que es el de la cuenta regresiva
                    this.Close();                                                                                                                              // cerramos el form

                    string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                   //leemos el numero de transaccion desde el archivo.                      

                    string datos = "Fin Transaccion";
                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                                                               // Escribe en el archivo fin de transaccion
                    bool flag = int.TryParse(data, out int idTrucha);                                                                                          // lo que guardo data, lo pasamos a int para sumar.
                    idTrucha++;                                                                                                                                // sumamos uno a idtrucha, que sera el proximo id a guardar.
                    string id_transaccion = idTrucha.ToString("D6");                                                                                           // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                    Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                 // borramos la ultima fila de transaccion
                    Archivos.GuardadoTransaccion(id_transaccion);
                }
                else
                {
                    string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");

                    string msg = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><endexchg/>";
                    string msn = Connect("127.0.0.1", msg);
                    var msjcompl = SEGURIDAD.Seguridad.GetInstancia().Controller2(msn);
                    int CodigoError = Convert.ToInt32(msjcompl.Item1);
                    string MensError = msjcompl.Item2;
                    if (CodigoError != 0)
                    {
                        string datos = "Error Bloqueo De Cartuchos";
                        Archivos.EscribeArchivoLogs(DateTime.Now, "F", data, datos, nombrearchivo);
                        SEGURIDAD.Log.GetInstancia().CrearLogErrorDisp(MensError);
                        FormError error = new FormError();
                        error.ShowDialog();
                    }

                    timer2.Dispose();                                                                                                                          // liberamos el timer2, que es el de la cuenta regresiva
                    this.Close();                                                                                                                              // cerramos el form

                    data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                   //leemos el numero de transaccion desde el archivo.                      

                    string datos2 = "Fin Transaccion";
                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos2, nombrearchivo);                                                                               // Escribe en el archivo fin de transaccion
                    bool flag = int.TryParse(data, out int idTrucha);                                                                                          // lo que guardo data, lo pasamos a int para sumar.
                    idTrucha++;                                                                                                                                // sumamos uno a idtrucha, que sera el proximo id a guardar.
                    string id_transaccion = idTrucha.ToString("D6");                                                                                           // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                    Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                 // borramos la ultima fila de transaccion
                    Archivos.GuardadoTransaccion(id_transaccion);
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

        private void SesionIniciada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnIngresar.PerformClick();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            int denominacion1 = 0;
            int denominacion2 = 0;


            StreamReader archivo = File.OpenText(Directory.GetCurrentDirectory() + "\\Tablas\\Tab.csv");                     // guardamos en la variable archivo, el archivo de las tablas, para ir leyendolo.                                                 

            string linea = "";                                                                                                  // declaramos una variable linea en vacio. Para leer cada linea del archivo.                                                     
            int a = 0;                                                                                                          // variable int a en 0                                                    
            while (!archivo.EndOfStream)                                                                                        // iniciamos un ciclo while que lea el archivo hasta el final, cuando termina de leer el archivo, termina el while.                                                   
            {

                linea = archivo.ReadLine();                                                                                     // en la variable linea guarda la linea leida.                                                     


                int den = Convert.ToInt32(linea.Substring(1, 5));                                                               // guardamos la demonominacion en la variable den
                string pos = linea.Substring(6, 1);                                                                             // guardamos la posicion en la variable pos

                switch (pos)
                {
                    case "1":
                        denominacion1 = den;
                        break;

                    case "2":
                        denominacion2 = den;
                        break;
                }
            }

            archivo.Close();


            txtDeno1.Text = "$" + denominacion1.ToString();
            txtDeno2.Text = "$" + denominacion2.ToString();

            string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\balanceo.csv");

            int inicial1 = Convert.ToInt32(data.Substring(0, 8));                                                                         // guardo en inicial1 la cantidad de billetes iniciales en la gaveta, pasado a int para que elimine los 0 que tiene el archivo.                                                     
            int inicial2 = Convert.ToInt32(data.Substring(8, 8));

            int dispAnterior1 = Convert.ToInt32(data.Substring(16, 8));                                                                   // guardamos en dispAnterior1 lo que habia dispensado hasta el momento en la gaveta 1                                                                           
            int dispAnterior2 = Convert.ToInt32(data.Substring(24, 8));                                                                   // guardamos en dispAnterior2 lo que habia dispensado hasta el momento en la gaveta 2  

            int remanente1 = inicial1 - dispAnterior1;
            int remanente2 = inicial2 - dispAnterior2;



            txtInicial1.Text = inicial1.ToString();
            txtInicial2.Text = inicial2.ToString();

            txtDispensado1.Text = dispAnterior1.ToString();
            txtDispensado2.Text = dispAnterior2.ToString();

            txtRemanente.Text = remanente1.ToString();
            txtRemanente2.Text = remanente2.ToString();
        }
    }

}
