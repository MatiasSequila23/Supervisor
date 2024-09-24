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
using ENTIDAD;
using NEGOCIO;

namespace Supervisor
{

    public partial class IniciarSesion : Form
    {
        int cont = 0;

       TcpClient client = new TcpClient();

        Usuarios usu = new Usuarios();


        string nombrearchivo = DateTime.Now.ToString("dd MMMM yyyy") + ".csv";                                                                                  // declaramos una variable string nombrearchivo, que le vamos a guardar la fecha y le agregamos la extension del archivo. Para que generemos el nombre del archivo Logs por fecha.

        public IniciarSesion(Usuarios obj, TcpClient cliente)
        {
            this.client = cliente;
            this.usu = obj;
            InitializeComponent();

        }

        private void IniciarSesion_Load(object sender, EventArgs e)
        {
          
            Cursor.Hide();                                                                                                                                       // escondemos el cursor, para que no se vea en la pantalla tactil
            lblError.Text = "";                                                                                                                                  // lblError lo ponemos en vacio
            lblError2.Text = "";                                                                                                                                 // lblError2 lo ponemos en vacio
            timer1.Enabled = true;
                                                                                                                                        // habilitamos el timer1 que es el de la hor
        }
      

 
        /// ///////////////////////////////////  BOTONES QUE ESCRIBEN EN LOS TEXTBOX CON BOTONERA EN PANTALLA //////////////////////////////////////////////////////
       


        private void button1_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";

            if (this.txtPassword.Text.Length < 4)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "1";
                    }
                
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";

            if (this.txtPassword.Text.Length < 4)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "2";
                    }
                
            
        }

     
           

        private void button3_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";

            if (this.txtPassword.Text.Length < 4)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "3";
                    }
               
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";

            if (this.txtPassword.Text.Length < 4)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "4";
                    }
                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";

            if (this.txtPassword.Text.Length < 4)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "5";
                    }
                
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";

            if (this.txtPassword.Text.Length < 4)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "6";
                    }
                
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";

            if (this.txtPassword.Text.Length < 4)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "7";
                    }
                
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";
            if (this.txtPassword.Text.Length < 4)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "8";
                    }
                
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";
            if (this.txtPassword.Text.Length < 4)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "9";
                    }
                
            
        }

        private void button0_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";
            if (this.txtPassword.Text.Length < 4)
                    {
                        this.txtPassword.Text = this.txtPassword.Text + "0";
                    }
                
           
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";


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

        private void btnBorrarTodo_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError2.Text = "";

            this.txtPassword.Text = "";
         
            this.lblError.Text = "";
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
                if (File.Exists(Directory.GetCurrentDirectory() + "\\Logs\\" + nombrearchivo) != true)                                                                // cuando apretamos el boton ingresar, preguntamos si el archivo, que tiene la fecha del dia como nombre y la extension csv existe en la direccion.
                {

                    TextWriter txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Logs\\" + nombrearchivo);                                            // si el archivo no existe, lo creamos en la carpeta logs
                    txtWriter.Close();                                                                                                                               // cerramos el archivo
                    Archivos.GuardadoTransaccion("000001");                                                                                                          // reiniciamos el contador de las transacciones a 1.

                }

                string legajo = usu.legajo;

                string datos;
                string data;
                string ntransaccion;

                string pass = txtPassword.Text;
                string offset = negUsuarios.BuscarOffset(legajo);                                                                                                        // declaramos una variable string offset y le vamos a asignar el valor del offset traido desde la base de datos, enviandole el dni

                string offsetNuevo = "";                                                                                                                              // declaramos variables que usaremos mas adelante para crear un offset nuevo si este es 0000.
                string dniOffset = "";                                                                                                                                // variable que usaremos mas adelante para comparar offset de la contraseña

                int bandera = 0;
                string finales4dni = "";                                                                                                                              // variable para tomar los 4 numeros finales del dni mas adelante

                string val = legajo.Substring(1, 4);
                if (val == pass)
                {
                    bandera = 1;
                }                                                                                                                           // variable para tomar los 4 numeros finales del dni mas adelante

                if (txtPassword.Text != "" && bandera == 0)
                {
                    if (pass.Length == 4)
                    {

                        Usuarios usu = new Usuarios();                                                                                                                // creamos un objeto usuario

                        usu = negUsuarios.BuscarUsuario(legajo);

                        if (usu != null)
                        {


                            ///////////////// Cargar en Logs.CSV ///////////////

                            ntransaccion = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                          //leemos el numero de transaccion desde el archivo.                      

                            datos = "Inicio Transaccion";
                            Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion, datos, nombrearchivo);

                            Archivos.EscribeArchivoLogs(DateTime.Now, "S", ntransaccion, usu.legajo, nombrearchivo);                                                          //Esto carga que el usuario esta intentando iniciar sesion, buscando en bd un dni.


                            if (offset == "0000")                                                                                                                              // preguntamos si offset traido de BD es igual a 0000 que es el offset que se le asignara al principio a cada usuario
                            {
                                int resultado = 0;                                                                                                                             // declaramos una variable resultado en 0

                                int tamPass = pass.Length;                                                                                                                     // le asignamos a la variable tamPass la cantidad de caracteres de la variable pass que contiene lo que se escribio en el textbox contraseña
                                string ult_4_pass = pass.Substring((tamPass - 4), 4);                                                                                          // tomamos los ultimos 4 digitos de la variable pass y se lo asignamos a la variable string ult_4_pass.


                                int tamDni = legajo.Length;                                                                                                                       //HACEMOS LO MISMO QUE ANTES PERO ESTA VEZ PARA DNI
                                string ult_4_dni = legajo.Substring((tamDni - 4), 4);

                                char[] arrayDni = new char[4];                                                                                                                 // CREAMOS DOS ARRAYS DE CHAR, UNO PARA DNI Y OTRO PARA PASS DE TAMAÑO 4.
                                char[] arrayPass = new char[4];

                                for (int i = 0; i < 4; i++)                                                                                                                    // iniciamos un ciclo de 4 para ir asignandole al array dni y pass los caraceteres de los string ult_4_pass y ult_4_dni. Para luego realizar calculos.
                                {
                                    arrayDni[i] = ult_4_dni[i];
                                    arrayPass[i] = ult_4_pass[i];
                                }

                                for (int i = 0; i < 4; i++)                                                                                                                    // iniciamos el ciclo for para pasar a las variables enteras _dni y _pass cada valor que hay en la  posicion i del array.
                                {

                                    int _dni = arrayDni[i] - '0';                                                                                                              //para pasar de char a int, es necesario al char restarle el caracter 0. 
                                    int _pass = arrayPass[i] - '0';

                                    // ESTO LO HACEMOS PARA PODER SUMAR O RESTAR DIGITO CON DIGITO Y NO TODO EL NUMERO ENTERO. EJEMPLO: SI HAY QUE RESTAR 9193 + 2901
                                    // EL RESULTADO NO SEA 6292, SINO SEA 7292. LA SUMA O RESTA SE HACE SIN ARRASTRE DE DECENAS. CALCULADO A CONTINUACION.  

                                    if (_dni < _pass)                                                                                                                          // en cada vuelta del for nos preguntamos si el numero de _dni es menor que el numero de _pass.
                                    {
                                        _dni += 10;                                                                                                                            // si llega a ser menor, le sumamos una decena, para asi podemos restar, en este caso, y nos tenemos que pedir 10 al de al lado.
                                    }

                                    resultado = _dni - _pass;                                                                                                                  // le asignamos a resultado el resultado de _dni - _pass

                                    offsetNuevo += resultado.ToString();                                                                                                       // a la variable offset nuevo le sumamos resultado. Cuando de las 4 vueltas offset nuevo nos va a quedar un numero de 4 digitos.


                                }


                                if (negUsuarios.GuardarOffset(offsetNuevo, legajo) == 1)                                                                                          // guardamos en la base de datos el nuevo offset del usuario. Ya tiene una contraseña creada.
                                {
                                    lblError.Text = "CONTRASEÑA CREADA CORRECTAMENTE.";
                                    lblError2.Text = "VUELVA A INGRESAR SU CONTASEÑA.";

                                    txtPassword.Text = "";



                                    ///////////////// Borramos el ultimo numero de transaccion para generar otro y escribimos una informacion //////////////

                                    data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                           // leemos el numero de transaccion desde el archivo.                 
                                    datos = legajo + " Nuevo offset";                                                                                                             // asignamos a datos el dni y la leyenda nuevo offset para guardar en el archivo.
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                                                // Escribe que se creo un nuevo offset.
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, "Fin transaccion.", nombrearchivo);                                                   // escribie el fin de la transaccion en el archivo.
                                    bool flag = int.TryParse(data, out int idTrucha);                                                                                          // lo que guardo data, lo pasamos a int para sumar.
                                    idTrucha++;                                                                                                                                // sumamos uno a idtrucha, que sera el proximo id a guardar.
                                    string id_transaccion = idTrucha.ToString("D6");                                                                                           // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                                    Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                 // borramos la ultima fila de transaccion
                                    Archivos.GuardadoTransaccion(id_transaccion);                                                                                              // la volvemos a crear con el nuevo numero de transaccion.

                                }
                                else
                                {
                                    lblError.Text = "ERROR AL CREAR CONTRASEÑA";
                                    lblError2.Text = "";

                                    ///////////////// Borramos el ultimo numero de transaccion para generar otro y escribimos una informacion //////////////

                                    data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                           // leemos el numero de transaccion desde el archivo.                 
                                    datos = legajo + " Error crear offset";                                                                                                       // asignamos a datos el dni y la leyenda nuevo offset para guardar en el archivo.
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                                                // Escribe que no se creo un nuevo offset.
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, "Fin transaccion.", nombrearchivo);                                                   // Mensaje de fin de transaccion
                                    bool flag = int.TryParse(data, out int idTrucha);                                                                                          // lo que guardo data, lo pasamos a int para sumar.
                                    idTrucha++;                                                                                                                                // sumamos uno a idtrucha, que sera el proximo id a guardar.
                                    string id_transaccion = idTrucha.ToString("D6");                                                                                           // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                                    Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                 // borramos la ultima fila de transaccion
                                    Archivos.GuardadoTransaccion(id_transaccion);                                                                                              // la volvemos a crear con el nuevo numero de transaccion.



                                }

                            }
                            else                                                                                                                                               // en caso de que el offset sea distinto de 0000, quiere decir que tiene una contraseña ya creada.
                            {
                                int resultado = 0;

                                string offsetPass = offset;


                                int tamPass = pass.Length;                                                                                                                     // volvemos a buscar el tamaño del dni, pass y esta vez del offset tambien.
                                string ult_4_pass = pass.Substring((tamPass - 4), 4);


                                int tamOffset = offsetPass.Length;
                                string ult_4_Offset = offsetPass.Substring((tamOffset - 4), 4);



                                int tamDni = legajo.Length;
                                finales4dni = legajo.Substring((tamDni - 4), 4);

                                char[] arrayOffset = new char[4];                                                                                                               // creamos los array de char pero esta vez de pass y offset
                                char[] arrayPass = new char[4];

                                for (int i = 0; i < 4; i++)                                                                                                                     // volvemos a asignar a los arrays los valores de la variables ult_4_pass y ult_4_offset, en este caso. Sin dni.
                                {
                                    arrayOffset[i] = ult_4_Offset[i];
                                    arrayPass[i] = ult_4_pass[i];
                                }

                                for (int i = 0; i < 4; i++)                                                                                                                     // volvemos a asignar los valores de los arrays a las variables _offset y _pass
                                {

                                    int _offset = arrayOffset[i] - '0';
                                    int _pass = arrayPass[i] - '0';

                                    resultado = _offset + _pass;                                                                                                                // en este caso sumamos _offset + _pass 
                                    if (resultado >= 10)                                                                                                                         // si el resultado es mayor que 10, le restamos 10 asi no tenemos que pasar a otro numero la decena, y nos queda el digito final del numero.
                                    {
                                        resultado -= 10;

                                    }
                                    else
                                    {

                                    }


                                    dniOffset += resultado.ToString();                                                                                                           // le sumamos a la variable dniOffset el resultado, asi forma un numero de 4 digitos al final del for.

                                }


                                string pr4dni = legajo.Substring(0, 1);

                                dniOffset = pr4dni + dniOffset;


                                if (legajo == dniOffset)                                                                                                                             // nos preguntamos si los ultimos 4 numeros del dni, son iguales a la suma del offset y los 4 numeros finales de la contraseña
                                {                                                                                                                                                 // si es si...
                                                                                                                                                                                  // buscamos el usuario en la base de datos con el dni ingresado al principio

                                    if (usu != null)
                                    {

                                        ///////////////// Cargar en Logs.CSV ///////////////


                                        datos = usu.legajo + ";" + usu.Offset + ";$" + usu.Importe + ";" + "Pin OK";
                                        Archivos.EscribeArchivoLogs(DateTime.Now, "E", ntransaccion, datos, nombrearchivo);                                                       // Escribe en el archivo que cargo el usuario de forma correcta.
                                        SesionCliente sesion = new SesionCliente(usu, client);                                                                                            // Iniciamos el form sesion enviando como parametro el objeto usuario como para tener datos y mostrarlos.
                                        sesion.ShowDialog();

                                        // dejamos los textbox en vacio.
                                        txtPassword.Text = "";
                                        lblError.Text = "";
                                        lblError2.Text = "";

                                    }
                                    else
                                    {
                                        lblError.Text = "ERROR AL INICIAR SESION.";
                                        lblError2.Text = "";
                                    }


                                }
                                else
                                {
                                    lblError.Text = "CONTRASEÑA INCORRECTA.";
                                    lblError2.Text = "";
                                    txtPassword.Text = "";

                                    datos = legajo + " Pin no Ok";

                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion, datos, nombrearchivo);                                                            // Escribe en el archivo que la contraseña es incorrecta.

                                    data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                               //leemos el numero de transaccion desde el archivo.       
                                    datos = "Fin Transaccion";
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);
                                    bool flag = int.TryParse(data, out int idTrucha);                                                                                              // lo que guardo data, lo pasamos a int para sumar.
                                    idTrucha++;                                                                                                                                    // sumamos uno a idtrucha, que sera el proximo id a guardar.
                                    string id_transaccion = idTrucha.ToString("D6");                                                                                               // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                                    Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                     // borramos la ultima fila de transaccion
                                    Archivos.GuardadoTransaccion(id_transaccion);                                                                                                  // la volvemos a crear con el nuevo numero de transaccion.


                                }
                            }
                        }
                        else
                        {
                            lblError.Text = "DNI INEXISTENTE.";
                        }
                    }
                    else
                    {
                        lblError.Text = "LA CONTRASEÑA DEBEN SER 4 CARACTERES.";
                    }

                }
                else
                {
                    if (bandera == 1)
                    {
                        lblError.Text = "CONTRASEÑA NO PERMITIDA";
                    }
                    else
                    {
                        lblError.Text = "DEBE COMPLETAR TODOS LOS CAMPOS.";
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

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IniciarSesion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnIngresar.PerformClick();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "\\Logs\\" + nombrearchivo) != true)                                                                // cuando apretamos el boton ingresar, preguntamos si el archivo, que tiene la fecha del dia como nombre y la extension csv existe en la direccion.
                {

                    TextWriter txtWriter = File.CreateText(Directory.GetCurrentDirectory() + "\\Logs\\" + nombrearchivo);                                            // si el archivo no existe, lo creamos en la carpeta logs
                    txtWriter.Close();                                                                                                                               // cerramos el archivo
                    Archivos.GuardadoTransaccion("000001");                                                                                                          // reiniciamos el contador de las transacciones a 1.

                }


               
                string legajo = usu.legajo;

                string datos;
                string data;
                string ntransaccion;

                string pass = txtPassword.Text;
                string offset = negUsuarios.BuscarOffset(legajo);                                                                                                        // declaramos una variable string offset y le vamos a asignar el valor del offset traido desde la base de datos, enviandole el dni

                string offsetNuevo = "";                                                                                                                              // declaramos variables que usaremos mas adelante para crear un offset nuevo si este es 0000.
                string dniOffset = "";                                                                                                                                // variable que usaremos mas adelante para comparar offset de la contraseña

                int bandera = 0;
                string finales4dni = "";                                                                                                                              // variable para tomar los 4 numeros finales del dni mas adelante

                string val = legajo.Substring(1, 4);
                if (val == pass)
                {
                    bandera = 1;
                }                                                                                                           // variable para tomar los 4 numeros finales del dni mas adelante

                if (txtPassword.Text != "")
                {
                    if (pass.Length == 4)
                    {

                        Usuarios usu = new Usuarios();                                                                                                                // creamos un objeto usuario

                        usu = negUsuarios.BuscarUsuario(legajo);

                        if (usu != null)
                        {


                            ///////////////// Cargar en Logs.CSV ///////////////

                            ntransaccion = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                          //leemos el numero de transaccion desde el archivo.                      

                            datos = "Inicio Transaccion";
                            Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion, datos, nombrearchivo);

                            Archivos.EscribeArchivoLogs(DateTime.Now, "S", ntransaccion, usu.legajo, nombrearchivo);                                                          //Esto carga que el usuario esta intentando iniciar sesion, buscando en bd un dni.


                            if (offset == "0000")                                                                                                                              // preguntamos si offset traido de BD es igual a 0000 que es el offset que se le asignara al principio a cada usuario
                            {
                                int resultado = 0;                                                                                                                             // declaramos una variable resultado en 0

                                int tamPass = pass.Length;                                                                                                                     // le asignamos a la variable tamPass la cantidad de caracteres de la variable pass que contiene lo que se escribio en el textbox contraseña
                                string ult_4_pass = pass.Substring((tamPass - 4), 4);                                                                                          // tomamos los ultimos 4 digitos de la variable pass y se lo asignamos a la variable string ult_4_pass.


                                int tamDni = legajo.Length;                                                                                                                       //HACEMOS LO MISMO QUE ANTES PERO ESTA VEZ PARA DNI
                                string ult_4_dni = legajo.Substring((tamDni - 4), 4);

                                char[] arrayDni = new char[4];                                                                                                                 // CREAMOS DOS ARRAYS DE CHAR, UNO PARA DNI Y OTRO PARA PASS DE TAMAÑO 4.
                                char[] arrayPass = new char[4];

                                for (int i = 0; i < 4; i++)                                                                                                                    // iniciamos un ciclo de 4 para ir asignandole al array dni y pass los caraceteres de los string ult_4_pass y ult_4_dni. Para luego realizar calculos.
                                {
                                    arrayDni[i] = ult_4_dni[i];
                                    arrayPass[i] = ult_4_pass[i];
                                }

                                for (int i = 0; i < 4; i++)                                                                                                                    // iniciamos el ciclo for para pasar a las variables enteras _dni y _pass cada valor que hay en la  posicion i del array.
                                {

                                    int _dni = arrayDni[i] - '0';                                                                                                              //para pasar de char a int, es necesario al char restarle el caracter 0. 
                                    int _pass = arrayPass[i] - '0';

                                    // ESTO LO HACEMOS PARA PODER SUMAR O RESTAR DIGITO CON DIGITO Y NO TODO EL NUMERO ENTERO. EJEMPLO: SI HAY QUE RESTAR 9193 + 2901
                                    // EL RESULTADO NO SEA 6292, SINO SEA 7292. LA SUMA O RESTA SE HACE SIN ARRASTRE DE DECENAS. CALCULADO A CONTINUACION.  

                                    if (_dni < _pass)                                                                                                                          // en cada vuelta del for nos preguntamos si el numero de _dni es menor que el numero de _pass.
                                    {
                                        _dni += 10;                                                                                                                            // si llega a ser menor, le sumamos una decena, para asi podemos restar, en este caso, y nos tenemos que pedir 10 al de al lado.
                                    }

                                    resultado = _dni - _pass;                                                                                                                  // le asignamos a resultado el resultado de _dni - _pass

                                    offsetNuevo += resultado.ToString();                                                                                                       // a la variable offset nuevo le sumamos resultado. Cuando de las 4 vueltas offset nuevo nos va a quedar un numero de 4 digitos.


                                }


                                if (negUsuarios.GuardarOffset(offsetNuevo, legajo) == 1)                                                                                          // guardamos en la base de datos el nuevo offset del usuario. Ya tiene una contraseña creada.
                                {
                                    lblError.Text = "CONTRASEÑA CREADA CORRECTAMENTE.";
                                    lblError2.Text = "VUELVA A INICIAR SESION.";

                                    txtPassword.Text = "";



                                    ///////////////// Borramos el ultimo numero de transaccion para generar otro y escribimos una informacion //////////////

                                    data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                           // leemos el numero de transaccion desde el archivo.                 
                                    datos = legajo + " Nuevo offset";                                                                                                             // asignamos a datos el dni y la leyenda nuevo offset para guardar en el archivo.
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                                                // Escribe que se creo un nuevo offset.
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, "Fin transaccion.", nombrearchivo);                                                   // escribie el fin de la transaccion en el archivo.
                                    bool flag = int.TryParse(data, out int idTrucha);                                                                                          // lo que guardo data, lo pasamos a int para sumar.
                                    idTrucha++;                                                                                                                                // sumamos uno a idtrucha, que sera el proximo id a guardar.
                                    string id_transaccion = idTrucha.ToString("D6");                                                                                           // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                                    Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                 // borramos la ultima fila de transaccion
                                    Archivos.GuardadoTransaccion(id_transaccion);                                                                                              // la volvemos a crear con el nuevo numero de transaccion.

                                }
                                else
                                {
                                    lblError.Text = "ERROR AL CREAR CONTRASEÑA";


                                    ///////////////// Borramos el ultimo numero de transaccion para generar otro y escribimos una informacion //////////////

                                    data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                           // leemos el numero de transaccion desde el archivo.                 
                                    datos = legajo + " Error crear offset";                                                                                                       // asignamos a datos el dni y la leyenda nuevo offset para guardar en el archivo.
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);                                                                // Escribe que no se creo un nuevo offset.
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, "Fin transaccion.", nombrearchivo);                                                   // Mensaje de fin de transaccion
                                    bool flag = int.TryParse(data, out int idTrucha);                                                                                          // lo que guardo data, lo pasamos a int para sumar.
                                    idTrucha++;                                                                                                                                // sumamos uno a idtrucha, que sera el proximo id a guardar.
                                    string id_transaccion = idTrucha.ToString("D6");                                                                                           // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                                    Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                 // borramos la ultima fila de transaccion
                                    Archivos.GuardadoTransaccion(id_transaccion);                                                                                              // la volvemos a crear con el nuevo numero de transaccion.



                                }

                            }
                            else                                                                                                                                               // en caso de que el offset sea distinto de 0000, quiere decir que tiene una contraseña ya creada.
                            {
                                int resultado = 0;

                                string offsetPass = offset;


                                int tamPass = pass.Length;                                                                                                                     // volvemos a buscar el tamaño del dni, pass y esta vez del offset tambien.
                                string ult_4_pass = pass.Substring((tamPass - 4), 4);


                                int tamOffset = offsetPass.Length;
                                string ult_4_Offset = offsetPass.Substring((tamOffset - 4), 4);



                                int tamDni = legajo.Length;
                                finales4dni = legajo.Substring((tamDni - 4), 4);

                                char[] arrayOffset = new char[4];                                                                                                               // creamos los array de char pero esta vez de pass y offset
                                char[] arrayPass = new char[4];

                                for (int i = 0; i < 4; i++)                                                                                                                     // volvemos a asignar a los arrays los valores de la variables ult_4_pass y ult_4_offset, en este caso. Sin dni.
                                {
                                    arrayOffset[i] = ult_4_Offset[i];
                                    arrayPass[i] = ult_4_pass[i];
                                }

                                for (int i = 0; i < 4; i++)                                                                                                                     // volvemos a asignar los valores de los arrays a las variables _offset y _pass
                                {

                                    int _offset = arrayOffset[i] - '0';
                                    int _pass = arrayPass[i] - '0';

                                    resultado = _offset + _pass;                                                                                                                // en este caso sumamos _offset + _pass 
                                    if (resultado >= 10)                                                                                                                         // si el resultado es mayor que 10, le restamos 10 asi no tenemos que pasar a otro numero la decena, y nos queda el digito final del numero.
                                    {
                                        resultado -= 10;

                                    }
                                    else
                                    {

                                    }


                                    dniOffset += resultado.ToString();                                                                                                           // le sumamos a la variable dniOffset el resultado, asi forma un numero de 4 digitos al final del for.

                                }


                                string pr4dni = legajo.Substring(0, 1);

                                dniOffset = pr4dni + dniOffset;


                                if (legajo == dniOffset)                                                                                                                             // nos preguntamos si los ultimos 4 numeros del dni, son iguales a la suma del offset y los 4 numeros finales de la contraseña
                                {                                                                                                                                                 // si es si...
                                                                                                                                                                                  // buscamos el usuario en la base de datos con el dni ingresado al principio

                                    if (usu != null)
                                    {

                                        ///////////////// Cargar en Logs.CSV ///////////////


                                        datos = usu.legajo + ";" + usu.Offset + ";$" + usu.Importe + ";" + "Pin OK";
                                        Archivos.EscribeArchivoLogs(DateTime.Now, "E", ntransaccion, datos, nombrearchivo);                                                       // Escribe en el archivo que cargo el usuario de forma correcta.
                                        SesionCliente sesion = new SesionCliente(usu, client);                                                                                            // Iniciamos el form sesion enviando como parametro el objeto usuario como para tener datos y mostrarlos.
                                        sesion.ShowDialog();

                                        // dejamos los textbox en vacio.
                                        txtPassword.Text = "";
                                        lblError.Text = "";
                                        lblError2.Text = "";

                                    }
                                    else
                                    {
                                        lblError.Text = "ERROR AL INICIAR SESION.";
                                    }


                                }
                                else
                                {
                                    lblError.Text = "CONTRASEÑA INCORRECTA.";
                                    txtPassword.Text = "";

                                    datos = legajo + " Pin no Ok";

                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", ntransaccion, datos, nombrearchivo);                                                            // Escribe en el archivo que la contraseña es incorrecta.

                                    data = Archivos.LeeUltimoRegistro(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                               //leemos el numero de transaccion desde el archivo.       
                                    datos = "Fin Transaccion";
                                    Archivos.EscribeArchivoLogs(DateTime.Now, "I", data, datos, nombrearchivo);
                                    bool flag = int.TryParse(data, out int idTrucha);                                                                                              // lo que guardo data, lo pasamos a int para sumar.
                                    idTrucha++;                                                                                                                                    // sumamos uno a idtrucha, que sera el proximo id a guardar.
                                    string id_transaccion = idTrucha.ToString("D6");                                                                                               // pasamos a string el id nuevo, y le agregamos seis 0 adelante.
                                    Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\Transaccion.csv");                                                     // borramos la ultima fila de transaccion
                                    Archivos.GuardadoTransaccion(id_transaccion);                                                                                                  // la volvemos a crear con el nuevo numero de transaccion.


                                }
                            }
                        }
                        else
                        {
                            lblError.Text = "DNI INEXISTENTE.";
                        }
                    }
                    else
                    {
                        lblError.Text = "LA CONTRASEÑA DEBEN SER 4 CARACTERES.";
                    }

                }
                else
                {

                    if (bandera == 1)
                    {
                        lblError.Text = "CONTRASEÑA NO PERMITIDA";
                    }
                    else
                    {
                        lblError.Text = "DEBE COMPLETAR TODOS LOS CAMPOS.";
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if(cont == 2)
            {
                lblError.Visible = false;
                lblError2.Visible = false;
                cont = 0;
            }
            else
            {
                lblError.Visible = true;
                lblError2.Visible = true;
                cont++;
            }
            

            
        }
    }
}
