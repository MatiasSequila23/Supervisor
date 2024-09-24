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
    public partial class SesionCliente : Form
    {
        public static int dispenso;

        public static string Mensaje;
        string cod = "Codigo de Error: ";
        string ms = "  Mensaje: ";

        // variables matias

        int denominacion1 = 0;
        int denominacion2 = 0;
        int CantD2Finall = 0;
        int ContadorF = 0;
        int contador = 0;
        int AB = 80;
        int CantD1Final = 0;
        int CantD2Final = 0;
        int Total = 0;
        int CantD1Inicio = 0;
        int aux = 0;
        int CantD2Inicio = 0;
        int difd1d2 = 0;
        int montoentrega = 0;
        // variables matias

        int cant_gav_1 = 0;
        int cant_gav_2 = 0;

        TcpClient client = new TcpClient();



        private int counter = 120;                                                                                               // variable int para el tiempo de timer para la cuenta regresiva.

        Billetes bill = new Billetes();

        List<Billetes> lista = new List<Billetes>();

        Usuarios usu = new Usuarios();

        string nombrearchivo = DateTime.Now.ToString("dd MMMM yyyy") + ".csv";

        public SesionCliente(Usuarios obj, TcpClient cliente)                                                                                      // recibimos de iniciar sesion el obj con el usuario
        {
            this.client = cliente;
            this.usu = obj;                                                                                                     // asignamos el objeto recibido al objeto usu.
            InitializeComponent();
        }

        private void SesionCliente_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
            txtSinDinero.Visible = false;
            txtError2.Text = "";
            txtMonto.Text = "";
            txtPuedeRet.Text = "";
            lblPuede.Text = "PARA SALIR DEBE RETIRAR TODO \n       EL MONTO DISPONIBLE";
            lblPuede.Visible = false;

            try
            {

                usu = negUsuarios.BuscarUsuario(usu.legajo);

                int importetemporal = usu.Importe;
                var msjcompl = SEGURIDAD.Seguridad.GetInstancia().PuedeRetirar(importetemporal);
                int Monto = msjcompl.Item1;
                int denom = msjcompl.Item2;
                int gavHabilitadas = msjcompl.Item3;


                if (Monto >= denom && gavHabilitadas != 0)
                {
                    ///////AGREGADO      23-06 ////////
                    btnSalir.Visible = false;
                    //////////////////
                    txtPuedeRet.Text = "$" + Monto.ToString();
                    lblPuede.Visible = true;

                    //pictureBox2.Select();

                    txtDni.Text = usu.legajo;                                                                                              // mostramos en el txtDni el dni del usuario
                    txtNombre.Text = usu.Nombre.ToUpper();                                             // mostramos en el txtNombre el nombre del usuario pasado a mayusculas
                    txtMonto.Text = "$" + usu.Importe.ToString() + ".-";                                                                // mostramos el importe que puede retirar el usuario  


                    panel1.Visible = false;                                                                                             // seteamos el panel1 en visible = flase para que no se muestre.

                    //Cursor.Hide();                                                                                                      // ocultamos el mouse para la pantalla tactil
                    // ocultamos el label que marca lo que retiro el usuario

                    timer1.Enabled = true;                                                                                              // habilitamos el timer de la hora

                    lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
                    lblFecha.Text = DateTime.Now.ToString(" dd MMMM yyyy ");

                    timer2 = new System.Windows.Forms.Timer();                                                                          // timer para el tiempo que expira la sesion (sacado de internet)
                    timer2.Tick += new EventHandler(timer2_Tick);
                    timer2.Interval = 1000; // 1 second
                    timer2.Start();
                    txtCountDown.Text = counter.ToString();                                                                             // mostramos en el label lblCountDouwn el tiempo que queda.

                }
                else
                {
                    if (Monto >= denom && gavHabilitadas == 0)
                    {
                        ///////AGREGADO      23-06 ////////
                        btnSalir.Visible = true;
                        //////////////////
                        lblPuede.Visible = false;
                        txtPuedeRet.Text = "$" + Monto.ToString(); ;
                        txtSinDinero.Visible = true;
                        btnRetirar.Enabled = true;
                        //pictureBox2.Select();

                        txtDni.Text = usu.legajo;                                                                                              // mostramos en el txtDni el dni del usuario
                        txtNombre.Text = usu.Nombre.ToUpper();                                             // mostramos en el txtNombre el nombre del usuario pasado a mayusculas
                        txtMonto.Text = "$" + usu.Importe.ToString() + ".-";                                                                // mostramos el importe que puede retirar el usuario  


                        panel1.Visible = false;                                                                                             // seteamos el panel1 en visible = flase para que no se muestre.

                        //Cursor.Hide();                                                                                                      // ocultamos el mouse para la pantalla tactil
                        // ocultamos el label que marca lo que retiro el usuario

                        timer1.Enabled = true;                                                                                              // habilitamos el timer de la hora

                        lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
                        lblFecha.Text = DateTime.Now.ToString(" dd MMMM yyyy ");

                        timer2 = new System.Windows.Forms.Timer();                                                                          // timer para el tiempo que expira la sesion (sacado de internet)
                        timer2.Tick += new EventHandler(timer2_Tick);
                        timer2.Interval = 1000; // 1 second
                        timer2.Start();
                        txtCountDown.Text = counter.ToString();
                    }
                    else
                    {
                        ///////AGREGADO      23-06 ////////
                        btnSalir.Visible = true;
                        //////////////////
                        lblPuede.Visible = false;
                        txtPuedeRet.Text = "$" + Monto.ToString(); ;
                        txtSinDinero.Visible = false;
                        btnRetirar.Enabled = true;
                        txtDni.Text = usu.legajo;                                                                                              // mostramos en el txtDni el dni del usuario
                        txtNombre.Text = usu.Nombre.ToUpper();                                             // mostramos en el txtNombre el nombre del usuario pasado a mayusculas
                        txtMonto.Text = "$" + usu.Importe.ToString() + ".-";                                                                // mostramos el importe que puede retirar el usuario  


                        panel1.Visible = false;                                                                                             // seteamos el panel1 en visible = flase para que no se muestre.

                        //Cursor.Hide();                                              
                        timer1.Enabled = true;                                                                                              // habilitamos el timer de la hora

                        lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
                        lblFecha.Text = DateTime.Now.ToString(" dd MMMM yyyy ");

                        timer2 = new System.Windows.Forms.Timer();                                                                          // timer para el tiempo que expira la sesion (sacado de internet)
                        timer2.Tick += new EventHandler(timer2_Tick);
                        timer2.Interval = 1000; // 1 second
                        timer2.Start();
                        txtCountDown.Text = counter.ToString();
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
            timer2.Dispose();                                                                                                                   // liberamos recursos del timer2

            FormCollection formulariosApp = Application.OpenForms;

            foreach (Form f in formulariosApp)
            {
                if (f.Name != "Inicio")                                                               // cuando el nombre del formulario sea distinto de IniciarSesion, que es el form inicial de la app, se van a cerrar.
                {
                    f.Close();
                }
            }

            string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                             //leemos el numero de transaccion desde el archivo.                      

            string datos = "Fin Transaccion";
            Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                                         // Escribe en el archivo fin de transaccion
            bool flag = int.TryParse(data, out int idTrucha);                                                                                   // lo que guardo data, lo pasamos a int para sumar.
            idTrucha++;                                                                                                                         // sumamos uno a idtrucha, que sera el proximo id a guardar.
            string id_transaccion = idTrucha.ToString("D6");                                                                                    // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
            Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                          // borramos la ultima fila de transaccion
            Archivos.GuardadoTransaccion(id_transaccion);                                                                                       // volvemos a crear la linea de transaccion con el nuevo numero
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString(" dd MMMM yyyy ");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            counter--;                                                                                                                             // en cada tick del timer, descontamos uno a counter, que van a ser los segundos.


            try
            {
                if (counter == 0)                                                                                                                  // nos preguntamos si counter es igual a 0, si es si, quiere decir que se acabo la sesion.
                {

                    string datos = "";                                                                                                                 // escribimos en el log que se cerro la sesion por error de tiemeout
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

                    timer2.Dispose();                                                                                                                   // liberamos el timer2

                    ErrorTimeout error = new ErrorTimeout();                                                                                            // instanciamos el form que muestra el mensaje de error
                    error.ShowDialog();                                                                                                                 // abrimos el form de mensaje de error. Este se cierra tocando cualquier parte de la pantalla.

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

        public static BalanceoArchivos EscribeArchivo(string monto1, string monto2, string dispensado1, string dispensado2)
        {

            DateTime diaHora = DateTime.Now;

            BalanceoArchivos d = new BalanceoArchivos(monto1, monto2, dispensado1, dispensado2);                                                     // se instancia el archivo balanceo, y  se envian los parametros. En balanceo archivos se elimina la ultima linea del archivo, asi se reescribe despues.
            Archivos.GenerarCSV(Directory.GetCurrentDirectory() + "\\utils\\balanceo.csv", d.CadenaParaGuardar());                                   // se genera el archivo guardado, con la cadena de caracteres que se va a guradar.

            return d;
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
                    FormError frm = new FormError();
                    frm.ShowDialog();
                }
            }
            else
            {
                timer2.Stop();
                FormError error = new FormError();
                error.ShowDialog();
            }
            return responseData;
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            try
            {
                //txtPuedeRet.Text = "PUEDE RETIRAR: $";

                usu = negUsuarios.BuscarUsuario(usu.legajo);
                int importetemporal = usu.Importe;
                int importe = usu.Importe;                                                                                          // declaramos variable int importe y le asignamos el importe que puede retirar el usuario traido desde la base de datos.                                                          
                int IMPORTETOTAL = usu.Importe;
                int gavhabilitadas = 0;

                int resto = 0;

                string letra = "";
                string letrag1 = "";
                string letrag2 = "";
                string posg1 = "";
                string posg2 = "";

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

                archivo.Close();                                                                                                    // cerramos el archivo que estamos leyendo de las gavetas

                if (posg2 == "1")
                {
                    denominacion1 = denominacion2;
                }

                if (gavhabilitadas == 2)
                {

                    if (denominacion1 != 0 && denominacion2 != 0)
                    {

                        /////////////////////////////////// buscamos la denominaciones de los billetes /////////////////////////


                        if (denominacion1 == 500 & denominacion2 == 200)
                        {
                            
                            int cambio = denominacion1;
                            denominacion1 = denominacion2;
                            denominacion2 = cambio;
                            CantD2Finall = 0;
                            ContadorF = 0;
                            contador = 0;
                            AB = 99999;
                            CantD1Final = 0;
                            CantD2Final = 0;
                            Total = Convert.ToInt32(usu.Importe);

                            CantD2Inicio = aux / denominacion2;
                            difd1d2 = denominacion1 / denominacion2;
                            resto = Total % denominacion1;
                            if (resto != 0)
                            {
                                Total = Total - 500;
                                if (Total >= 0)
                                {
                                    CantD2Inicio = 1;
                                }
                                else
                                {
                                    CantD2Inicio = 0;
                                    Total = Total + 500;
                                }
                            }
                            CantD1Inicio = Total / denominacion1;
                            CantD2Final = CantD2Inicio;


                            CantD1Final = CantD1Inicio;
                            CantD2Finall = CantD2Inicio;

                            for (int i = CantD1Inicio; i >= 1; i = i - 5)
                            {

                                int auxiliar = Math.Abs(i - CantD2Final);
                                if (AB > auxiliar)
                                {
                                    AB = auxiliar;
                                    CantD1Final = i;
                                    CantD2Finall = (2 * contador) + CantD2Inicio;
                                    ContadorF = contador;

                                }
                                contador++;
                                CantD2Final = (contador * 2) + CantD2Inicio;
                            }
                            montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;
                            int EntregaMax = 40;
                            int CantTotBilletes = CantD1Final + CantD2Finall;
                            if (EntregaMax < CantTotBilletes)
                            {
                                int al = CantTotBilletes - EntregaMax;
                                int r = al % 2;
                                int b = al / 2;

                                if (r == 1)
                                {
                                    b += 1;
                                }

                                CantD1Final = CantD1Final - b;
                                int rest = al % 2;
                                CantD2Finall = (CantD2Finall - b) + (rest);
                                montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;
                            }

                            if (montoentrega == 300)
                            {
                                montoentrega = 200;
                                cant_gav_1 = 0;
                                cant_gav_2 = 1;
                            }
                            else
                            {
                                cant_gav_1 = CantD2Finall;
                                cant_gav_2 = CantD1Final;
                            }

                            

                            int gavaux = denominacion1;
                            denominacion1 = denominacion2;
                            denominacion2 = gavaux;
                        }
                        else
                        {
                            if (denominacion1 == 200 & denominacion2 == 500)
                            {
                                CantD2Finall = 0;
                                ContadorF = 0;
                                contador = 0;
                                AB = 99999;
                                CantD1Final = 0;
                                CantD2Final = 0;
                                Total = Convert.ToInt32(usu.Importe);

                                CantD2Inicio = aux / denominacion2;
                                difd1d2 = denominacion1 / denominacion2;
                                resto = Total % denominacion1;
                                if (resto != 0)
                                {
                                    Total = Total - 500;
                                    if (Total >= 0)
                                    {
                                        CantD2Inicio = 1;
                                    }
                                    else
                                    {
                                        CantD2Inicio = 0;
                                        Total = Total + 500;
                                    }
                                }
                                CantD1Inicio = Total / denominacion1;
                                CantD2Final = CantD2Inicio;


                                CantD1Final = CantD1Inicio;
                                CantD2Finall = CantD2Inicio;
                                for (int i = CantD1Inicio; i >= 1; i = i - 5)
                                {

                                    int auxiliar = Math.Abs(i - CantD2Final);
                                    if (AB > auxiliar)
                                    {
                                        AB = auxiliar;
                                        CantD1Final = i;
                                        CantD2Finall = (2 * contador) + CantD2Inicio;
                                        ContadorF = contador;

                                    }
                                    contador++;
                                    CantD2Final = (contador * 2) + CantD2Inicio;
                                }
                                montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;
                                int EntregaMax = 40;
                                int CantTotBilletes = CantD1Final + CantD2Finall;
                                if (EntregaMax < CantTotBilletes)
                                {
                                    int al = CantTotBilletes - EntregaMax;
                                    int r = al % 2;
                                    int b = al / 2;

                                    if (r == 1)
                                    {
                                        b += 1;
                                    }

                                    CantD1Final = CantD1Final - b;
                                    int rest = al % 2;
                                    CantD2Finall = (CantD2Finall - b) + (rest);
                                    montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;
                                }

                                if (montoentrega == 300)
                                {
                                    montoentrega = 200;
                                    cant_gav_1 = 1;
                                    cant_gav_2 = 0;
                                }
                                else
                                {
                                    cant_gav_1 = CantD1Final;
                                    cant_gav_2 = CantD2Finall;
                                }

                                if (montoentrega<200)
                                {
                                    montoentrega = 0;
                                }

                            }
                            else
                            {
                                if (denominacion1 < denominacion2)
                                {
                                    int aux = denominacion1;
                                    denominacion1 = denominacion2;
                                    denominacion2 = aux;


                                    CantD2Finall = 0;
                                    ContadorF = 0;
                                    contador = 0;
                                    AB = 99999;
                                    CantD1Final = 0;
                                    CantD2Final = 0;
                                    Total = Convert.ToInt32(usu.Importe);
                                    CantD1Inicio = Total / denominacion1;
                                    aux = Total % denominacion1;
                                    CantD2Inicio = aux / denominacion2;
                                    difd1d2 = denominacion1 / denominacion2;

                                    for (int b = CantD1Inicio; b >= 0; b--)
                                    {

                                        int auxiliar = Math.Abs(b - CantD2Final);
                                        if (AB > auxiliar)
                                        {
                                            AB = auxiliar;
                                            CantD1Final = b;
                                            CantD2Finall = (contador * difd1d2) + CantD2Inicio;
                                            ContadorF = contador;

                                        }
                                        contador++;
                                        CantD2Final = (contador * difd1d2) + CantD2Inicio;

                                    }
                                    montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;
                                    int EntregaMax = 40;
                                    int CantTotBilletes = CantD1Final + CantD2Finall;
                                    if (EntregaMax < CantTotBilletes)
                                    {
                                        int al = CantTotBilletes - EntregaMax;
                                        int r = al % 2;
                                        int b = al / 2;

                                        if (r == 1)
                                        {
                                            b += 1;
                                        }

                                        CantD1Final = CantD1Final - b;
                                        int rest = al % 2;
                                        CantD2Finall = (CantD2Finall - b) + (rest);
                                        montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;
                                    }

                                    cant_gav_1 = CantD2Finall;
                                    cant_gav_2 = CantD1Final;

                                }
                                else
                                {
                                    CantD2Finall = 0;
                                    ContadorF = 0;
                                    contador = 0;
                                    AB = 99999;
                                    CantD1Final = 0;
                                    CantD2Final = 0;
                                    Total = Convert.ToInt32(usu.Importe);
                                    CantD1Inicio = Total / denominacion1;
                                    aux = Total % denominacion1;
                                    CantD2Inicio = aux / denominacion2;
                                    difd1d2 = denominacion1 / denominacion2;

                                    for (int b = CantD1Inicio; b >= 0; b--)
                                    {

                                        int auxiliar = Math.Abs(b - CantD2Final);
                                        if (AB > auxiliar)
                                        {
                                            AB = auxiliar;
                                            CantD1Final = b;
                                            CantD2Finall = (contador * difd1d2) + CantD2Inicio;
                                            ContadorF = contador;

                                        }
                                        contador++;
                                        CantD2Final = (contador * difd1d2) + CantD2Inicio;

                                    }
                                    montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;
                                    int EntregaMax = 40;
                                    int CantTotBilletes = CantD1Final + CantD2Finall;
                                    if (EntregaMax < CantTotBilletes)
                                    {
                                        int al = CantTotBilletes - EntregaMax;
                                        int r = al % 2;
                                        int b = al / 2;

                                        if (r == 1)
                                        {
                                            b += 1;
                                        }

                                        CantD1Final = CantD1Final - b;
                                        int rest = al % 2;
                                        CantD2Finall = (CantD2Finall - b) + (rest);
                                        montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;
                                    }

                                    cant_gav_1 = CantD1Final;
                                    cant_gav_2 = CantD2Finall;

                                }
                            }

                        }

                        string datos = usu.legajo + ";" + letrag1 + cant_gav_1 + ";" + letrag2 + cant_gav_2;                                          // si resto es igual a 0, guardamos en datos el dni, cantidad de billetes por gavetas y la gaveta.                                                    

                        string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\balanceo.csv");                          // guardamos en data lo que leo del archivo balanceo. La unica linea que tiene.                                                     

                        int dispAnterior1 = Convert.ToInt32(data.Substring(16, 8));                                                                   // guardamos en dispAnterior1 lo que habia dispensado hasta el momento en la gaveta 1                                                                           
                        int dispAnterior2 = Convert.ToInt32(data.Substring(24, 8));                                                                   // guardamos en dispAnterior2 lo que habia dispensado hasta el momento en la gaveta 2                                                      

                        int dispe1 = dispAnterior1 + cant_gav_1;                                                                                      // guardo en dispe1 la suma entre lo dispensado anterior de gaveta 1 y lo que tiene que dispensar ahora.                                                                       
                        int dispe2 = dispAnterior2 + cant_gav_2;                                                                                      // guardo en dispe2 la suma entre lo dispensado anterior de gaveta 2 y lo que tiene que dispensar ahora.                                                               

                        string dispensado1 = dispe1.ToString("D8");                                                                                   // guardo en dispensado1 el valor de dispe1 convertido a string y agregandole los 0 necesarios para el archivo.                                                
                        string dispensado2 = dispe2.ToString("D8");                                                                                   // guardo en dispensado2 el valor de dispe2 convertido a string y agregandole los 0 necesarios para el archivo.                                                

                        int inicial1 = Convert.ToInt32(data.Substring(0, 8));                                                                         // guardo en inicial1 la cantidad de billetes iniciales en la gaveta, pasado a int para que elimine los 0 que tiene el archivo.                                                     
                        int inicial2 = Convert.ToInt32(data.Substring(8, 8));                                                                         // guardo en inicial2 la cantidad de billetes iniciales en la gaveta, pasado a int para que elimine los 0 que tiene el archivo.                                                      


                        int haybill1 = inicial1 - dispAnterior1;                                                                                      // guardo en haybill1 la resta de inicial1 menos lo dispensado anterior, para saber si tiene billetes disponible.                                                      
                        int haybill2 = inicial2 - dispAnterior2;                                                                                      // guardo en haybill2 la resta de inicial2 menos lo dispensado anterior, para saber si tiene billetes disponible.                                      



                        if (montoentrega == 0)
                        {
                            
                            txtError2.Text = "No se puede entregar esa cantidad de dinero.";

                            data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                     //leemos el numero de transaccion desde el archivo.                      

                            datos = "Cantidad no disponible";
                            Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                                         // Escribe en el archivo fin de transaccion
                            bool flag = int.TryParse(data, out int idTrucha);                                                                                   // lo que guardo data, lo pasamos a int para sumar.
                            idTrucha++;                                                                                                                         // sumamos uno a idtrucha, que sera el proximo id a guardar.
                            string id_transaccion = idTrucha.ToString("D6");                                                                                    // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                            Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                          // borramos la ultima fila de transaccion
                            Archivos.GuardadoTransaccion(id_transaccion);                                                                                       // volvemos a crear la linea de transaccion con el nuevo numero


                        }
                        else
                        {
                            string fech = DateTime.Now.ToString("dd MMMM yyyy");
                            string hor = DateTime.Now.ToString(" HH mm ss"); string ruta = Directory.GetCurrentDirectory() + @"\\ReporteError\\Reporte" + fech + hor + ".csv";

                            if (haybill1 >= cant_gav_1 && haybill2 >= cant_gav_2)                                                                       // preguntamos si haybill1 es mayor o igual a g1 y lo mismo con g2                                                    
                            {

                                // si es si, actualizamos el archivo de balanceo con los nuevos datos de inicial ( siempre el mismo hasta que no se inserten billetes en gavetas) y los dispensados.                                                    

                                string ntransaccion = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");          // buscamos el numero de transaccion actual.                                                        
                                Archivos.EscribeArchivoLogs(DateTime.Now, "S", ntransaccion, datos, nombrearchivo);                                      // escribimos en el archivo logs la fecha, el tipo de operacion, el numero de transaccion y los datos creados en la linea 123.                                                    

                                DateTime fecha = DateTime.Now;


                                string rut = "";
                                ///////////////AGREGADO:  //////////////////////////////////
                                if (negUsuarios.DescontarRetiro(usu.legajo, montoentrega, fecha))
                                {

                                    string rout = SEGURIDAD.Log.GetInstancia().LogReembolsoCritico(ntransaccion, montoentrega, usu.legajo, usu.Nombre);
                                    timer2.Enabled = false;
                                    string msg = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><dispense><hooper1> " + cant_gav_1 + " </hooper1><hooper2> " + cant_gav_2 + " </hooper2></dispense>";
                                    Dispensar disp = new Dispensar(client, msg, inicial1, dispensado1, inicial2, dispensado2, montoentrega);
                                    disp.ShowDialog();

                                    var msjcompl = SEGURIDAD.Seguridad.GetInstancia().Controller2(Mensaje);
                                    int CodigoError = Convert.ToInt32(msjcompl.Item1);
                                    string MensError = msjcompl.Item2;
                                    if (CodigoError != 0)
                                    {

                                        SEGURIDAD.Log.GetInstancia().CrearLogErrorDisp(MensError);
                                        rut = SEGURIDAD.Log.GetInstancia().CrearDocRespaldo(ntransaccion, montoentrega, usu.legajo, usu.Nombre);
                                        if (negUsuarios.RestaurarMonto(usu.legajo, importetemporal, fecha))
                                        {
                                            SEGURIDAD.Log.GetInstancia().ComplDocrespaldo(rut);
                                            if (Mensaje.Contains("Empty Cassette"))
                                            {
                                                datos = "Terminal sin dinero";
                                                Archivos.EscribeArchivoLogs(DateTime.Now, "F", data, datos, nombrearchivo);
                                                int i = 0;
                                                string gav = "";
                                                TextReader txtReader = File.OpenText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                                TextWriter txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv");
                                                while (true)
                                                {
                                                    string line = txtReader.ReadLine();
                                                    if (line != null)
                                                    {
                                                        gav = line.Substring(0, 1);

                                                        string den = line.Substring(1, 5);
                                                        if (line.StartsWith(gav))
                                                        {
                                                            line = gav + den + "0";
                                                            txtWriter.WriteLine(line);
                                                        }
                                                        else
                                                        {
                                                            txtWriter.WriteLine(line);
                                                        }
                                                    }
                                                    else { break; }

                                                    i++;
                                                }
                                                txtReader.Close();
                                                txtWriter.Close();
                                                File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                                File.Copy(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv", Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                                File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv");

                                                panel1.Visible = true;

                                                string ntransaccion2 = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                                                datos = "Terminal sin dinero para entregar.";
                                                Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion2, datos, nombrearchivo);
                                            }
                                            else
                                            {
                                                datos = "Error en Dispensador";
                                                Archivos.EscribeArchivoLogs(DateTime.Now, "F", data, datos, nombrearchivo);
                                                FormError error = new FormError();
                                                error.ShowDialog();
                                            }
                                        }

                                    }
                                    if (File.Exists(rout))
                                    {
                                        File.Delete(rout);
                                    }
                                }
                                else
                                {
                                    FormError error = new FormError();
                                    error.ShowDialog();
                                }

                                if(dispenso != 0)
                                {
                                    timer2.Enabled = true;
                                }
                                else
                                {
                                    timer2.Enabled = false;
                                }

                                                                
                            }
                            else
                            {


                                if (haybill1 < cant_gav_1 || haybill2 < cant_gav_2)                                                                      //// HACEMOS LO MISMO PERO AHORA SI LAS DOS GAVETAS SON MENORES Y NO PUEDEN ENTREGAR BILLETES. SE DESHABILITAN LAS DOS Y EL TERMINAL YA NO PUEDE ENTREGAR DINERO.
                                {
                                    int i = 0;
                                    string gav = "";
                                    TextReader txtReader = File.OpenText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                    TextWriter txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv");
                                    while (true)
                                    {
                                        string line = txtReader.ReadLine();
                                        if (line != null)
                                        {
                                            gav = line.Substring(0, 1);

                                            string den = line.Substring(1, 5);
                                            if (line.StartsWith(gav))
                                            {
                                                line = gav + den + "0";
                                                txtWriter.WriteLine(line);
                                            }
                                            else
                                            {
                                                txtWriter.WriteLine(line);
                                            }
                                        }
                                        else { break; }

                                        i++;
                                    }
                                    txtReader.Close();
                                    txtWriter.Close();
                                    File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                    File.Copy(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv", Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                    File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv");

                                    panel1.Visible = true;

                                    string ntransaccion2 = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                                    datos = "Terminal sin dinero para entregar.";
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion2, datos, nombrearchivo);

                                }
                            }
                        }
                    }
                    else
                    {
                        panel1.Visible = true;
                    }
                }
                else
                {
                    if (gavhabilitadas == 1)
                    {

                        int imp = 0;
                        int rest = 0;

                        rest = usu.Importe % denominacion1;
                        imp = usu.Importe - rest;

                        cant_gav_1 = imp / denominacion1;

                        int EntregaMax = 40;
                        //int CantTotBilletes = CantD1Final + CantD2Finall;
                        if (EntregaMax < cant_gav_1)
                        {
                            cant_gav_1 = EntregaMax;
                            //montoentrega = cant_gav_1 * denominacion1;
                        }

                        string datos = usu.legajo + ";" + letrag1 + cant_gav_1;                                          // si resto es igual a 0, guardamos en datos el dni, cantidad de billetes por gavetas y la gaveta.                                                    

                        string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\balanceo.csv");                          // guardamos en data lo que leo del archivo balanceo. La unica linea que tiene.                                                     

                        int dispAnterior1 = Convert.ToInt32(data.Substring(16, 8));                                                                   // guardamos en dispAnterior1 lo que habia dispensado hasta el momento en la gaveta 1                                                                                                                                                         

                        int dispe1 = dispAnterior1 + cant_gav_1;                                                                                      // guardo en dispe1 la suma entre lo dispensado anterior de gaveta 1 y lo que tiene que dispensar ahora.                                                                       

                        string dispensado1 = dispe1.ToString("D8");                                                                                   // guardo en dispensado1 el valor de dispe1 convertido a string y agregandole los 0 necesarios para el archivo.                                                

                        int inicial1 = Convert.ToInt32(data.Substring(0, 8));                                                                         // guardo en inicial1 la cantidad de billetes iniciales en la gaveta, pasado a int para que elimine los 0 que tiene el archivo.                                                     

                        int haybill1 = inicial1 - dispAnterior1;                                                                                      // guardo en haybill1 la resta de inicial1 menos lo dispensado anterior, para saber si tiene billetes disponible.                                                      

                        montoentrega = cant_gav_1 * denominacion1;


                        /////////////////////////////////////////////////////////////
                        ///

                        if (montoentrega == 0)
                        {
                            
                            txtError2.Text = "No se puede entregar esa cantidad de dinero.";

                            data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                     //leemos el numero de transaccion desde el archivo.                      

                            datos = "Cantidad no disponible";
                            Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                                         // Escribe en el archivo fin de transaccion
                            bool flag = int.TryParse(data, out int idTrucha);                                                                                   // lo que guardo data, lo pasamos a int para sumar.
                            idTrucha++;                                                                                                                         // sumamos uno a idtrucha, que sera el proximo id a guardar.
                            string id_transaccion = idTrucha.ToString("D6");                                                                                    // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                            Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                          // borramos la ultima fila de transaccion
                            Archivos.GuardadoTransaccion(id_transaccion);                                                                                       // volvemos a crear la linea de transaccion con el nuevo numero


                        }
                        else
                        {
                            data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                            string fech = DateTime.Now.ToString("dd MMMM yyyy");
                            string hor = DateTime.Now.ToString(" HH mm ss"); string ruta = Directory.GetCurrentDirectory() + @"\\ReporteError\\Reporte" + fech + hor + ".csv";

                            if (haybill1 >= cant_gav_1)                                                                                                                                           // preguntamos si haybill1 es mayor o igual a g1 y lo mismo con g2                                                    
                            {                                                                                                                                                                   // si es si, actualizamos el archivo de balanceo con los nuevos datos de inicial ( siempre el mismo hasta que no se inserten billetes en gavetas) y los dispensados.                                                    

                                string ntransaccion = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                   // buscamos el numero de transaccion actual.                                                        
                                Archivos.EscribeArchivoLogs(DateTime.Now, "S", ntransaccion, datos, nombrearchivo);                                                                               // escribimos en el archivo logs la fecha, el tipo de operacion, el numero de transaccion y los datos creados en la linea 123.                                                    

                                DateTime fecha = DateTime.Now;


                                string rut = "";
                                ///////////////AGREGADO:  //////////////////////////////////
                                ///////////////AGREGADO:  //////////////////////////////////
                                if (negUsuarios.DescontarRetiro(usu.legajo, montoentrega, fecha))
                                {

                                    string rout = SEGURIDAD.Log.GetInstancia().LogReembolsoCritico(ntransaccion, montoentrega, usu.legajo, usu.Nombre);
                                    timer2.Enabled = false;

                                    string msg = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><dispense><hooper1> " + cant_gav_1 + " </hooper1><hooper2> " + cant_gav_2 + " </hooper2></dispense>";
                                    Dispensar disp = new Dispensar(client, msg, inicial1, dispensado1, 0, "0", montoentrega);
                                    disp.ShowDialog();

                                    var msjcompl = SEGURIDAD.Seguridad.GetInstancia().Controller2(Mensaje);
                                    int CodigoError = Convert.ToInt32(msjcompl.Item1);
                                    string MensError = msjcompl.Item2;
                                    if (CodigoError != 0)
                                    {

                                        SEGURIDAD.Log.GetInstancia().CrearLogErrorDisp(MensError);
                                        rut = SEGURIDAD.Log.GetInstancia().CrearDocRespaldo(ntransaccion, montoentrega, usu.legajo, usu.Nombre);
                                        if (negUsuarios.RestaurarMonto(usu.legajo, importetemporal, fecha))
                                        {
                                            SEGURIDAD.Log.GetInstancia().ComplDocrespaldo(rut);
                                            if (Mensaje.Contains("Empty Cassette"))
                                            {
                                                datos = "Terminal sin dinero";
                                                Archivos.EscribeArchivoLogs(DateTime.Now, "F", data, datos, nombrearchivo);
                                                int i = 0;
                                                string gav = "";
                                                TextReader txtReader = File.OpenText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                                TextWriter txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv");
                                                while (true)
                                                {
                                                    string line = txtReader.ReadLine();
                                                    if (line != null)
                                                    {
                                                        gav = line.Substring(0, 1);

                                                        string den = line.Substring(1, 5);
                                                        if (line.StartsWith(gav))
                                                        {
                                                            line = gav + den + "0";
                                                            txtWriter.WriteLine(line);
                                                        }
                                                        else
                                                        {
                                                            txtWriter.WriteLine(line);
                                                        }
                                                    }
                                                    else { break; }

                                                    i++;
                                                }
                                                txtReader.Close();
                                                txtWriter.Close();
                                                File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                                File.Copy(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv", Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                                File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv");

                                                panel1.Visible = true;

                                                string ntransaccion2 = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                                                datos = "Terminal sin dinero para entregar.";
                                                Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion2, datos, nombrearchivo);
                                            }
                                            else
                                            {
                                                datos = "Error en Dispensador";
                                                Archivos.EscribeArchivoLogs(DateTime.Now, "F", data, datos, nombrearchivo);
                                                FormError error = new FormError();
                                                error.ShowDialog();
                                            }
                                        }

                                    }

                                    if (File.Exists(rout))
                                    {
                                        File.Delete(rout);
                                    }
                                }
                                else
                                {
                                    FormError error = new FormError();
                                    error.ShowDialog();
                                }

                                if (dispenso != 0)
                                {
                                    timer2.Enabled = true;
                                }
                                else
                                {
                                    timer2.Enabled = false;
                                }


                            }
                            else
                            {
                                if (haybill1 <= cant_gav_1)                                                                      //// HACEMOS LO MISMO PERO AHORA SI LAS DOS GAVETAS SON MENORES Y NO PUEDEN ENTREGAR BILLETES. SE DESHABILITAN LAS DOS Y EL TERMINAL YA NO PUEDE ENTREGAR DINERO.
                                {
                                    int i = 0;
                                    string gav = "";
                                    TextReader txtReader = File.OpenText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                    TextWriter txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv");
                                    while (true)
                                    {
                                        string line = txtReader.ReadLine();
                                        if (line != null)
                                        {
                                            gav = line.Substring(0, 1);

                                            string den = line.Substring(1, 5);
                                            if (line.StartsWith(gav))
                                            {
                                                line = gav + den + "0";
                                                txtWriter.WriteLine(line);
                                            }
                                            else
                                            {
                                                txtWriter.WriteLine(line);
                                            }
                                        }
                                        else { break; }

                                        i++;
                                    }
                                    txtReader.Close();
                                    txtWriter.Close();
                                    File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                    File.Copy(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv", Directory.GetCurrentDirectory() + "\\Tablas\\Tablas.csv");
                                    File.Delete(Directory.GetCurrentDirectory() + "\\Tablas\\Tablas2.csv");

                                    panel1.Visible = true;

                                    string ntransaccion2 = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");
                                    datos = "Terminal sin dinero para entregar.";
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion2, datos, nombrearchivo);

                                }
                            }
                        }



                    }
                    else
                    {
                        panel1.Visible = true;
                    }
                }
                int DenMen = 0;
                if (gavhabilitadas == 2 && denominacion1 > denominacion2)
                {
                    DenMen = denominacion2;
                }
                else if (gavhabilitadas == 1 || gavhabilitadas == 2)
                {
                    DenMen = denominacion1;
                }


                if ((usu.Importe - montoentrega) < DenMen) ///////////////////////////////////////////   CUANDO QUEDO 200 Y LA DENOMINACION ERA 200 NO DISPENSO, TENIENDO MENOR O IGUAL. CAMBIADO A SOLO MENOR.
                {
                    ///////AGREGADO      23-06 ////////
                    btnSalir.Visible = true;
                    //////////////////
                    txtMonto.Text = "$" + (usu.Importe - montoentrega).ToString() + ".-";
                    btnRetirar.Enabled = false;
                    lblPuede.Visible = false;

                    var msjcompl = SEGURIDAD.Seguridad.GetInstancia().PuedeRetirar(usu.Importe - montoentrega);
                    int Monto = msjcompl.Item1;
                    int denom = msjcompl.Item2;
                    txtPuedeRet.Text = "$" + Monto.ToString(); 
                }
                else
                {
                    ///////AGREGADO      23-06 ////////
                    btnSalir.Visible = false;
                    //////////////////
                    txtMonto.Text = "$" + (usu.Importe - montoentrega).ToString() + ".-";
                    btnRetirar.Enabled = true;
                    lblPuede.Visible = true;

                    var msjcompl = SEGURIDAD.Seguridad.GetInstancia().PuedeRetirar(usu.Importe - montoentrega);
                    int Monto = msjcompl.Item1;
                    int denom = msjcompl.Item2;
                    txtPuedeRet.Text = "$" + Monto.ToString();
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

        private void SesionCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRetirar.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer2.Dispose();                                                                                                                   // liberamos recursos del timer2

            //this.Close();

            FormCollection formulariosApp = Application.OpenForms;

            foreach (Form f in formulariosApp)
            {
                if (f.Name != "Inicio")                                                               // cuando el nombre del formulario sea distinto de IniciarSesion, que es el form inicial de la app, se van a cerrar.
                {
                    f.Close();
                }
            }

            string data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                              //leemos el numero de transaccion desde el archivo.                      

            string datos = "Fin Transaccion";
            Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                                          // Escribe en el archivo fin de transaccion
            bool flag = int.TryParse(data, out int idTrucha);                                                                                    // lo que guardo data, lo pasamos a int para sumar.
            idTrucha++;                                                                                                                          // sumamos uno a idtrucha, que sera el proximo id a guardar.
            string id_transaccion = idTrucha.ToString("D6");                                                                                     // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
            Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                           // borramos la ultima fila de transaccion
            Archivos.GuardadoTransaccion(id_transaccion);
        }

    }

}
