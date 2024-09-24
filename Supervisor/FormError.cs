using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supervisor
{
    public partial class FormError : Form
    {
        string captcha = "";

        public FormError()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FormCollection formulariosApp = Application.OpenForms;

            foreach (Form f in formulariosApp)
            {
                if (f.Name != "Inicio")                                                               // cuando el nombre del formulario sea distinto de IniciarSesion, que es el form inicial de la app, se van a cerrar.
                {
                    f.Close();
                }
            }
        }

        private void FormError_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            captcha = "";

            captcha += "1";                                                                                                                                       // agregamos el valor del boton al string captcha.

            if (captcha.Length == 4)                                                                                                                              // nos preguntamos si el captcha tiene la longitud de 4 caracteres.
            {
                if (captcha == "1234")                                                                                                                            // nos preguntamos si el captcha es igual a 1234 que seria el orden como se apretan los botones.
                {
                    FormCollection formulariosApp = Application.OpenForms;

                    foreach (Form f in formulariosApp)
                    {
                        if (f.Name != "Inicio")                                                               // cuando el nombre del formulario sea distinto de IniciarSesion, que es el form inicial de la app, se van a cerrar.
                        {
                            f.Close();
                        }
                    }                                                                                                                        // abrimos el form supervisor.
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
                    FormCollection formulariosApp = Application.OpenForms;

                    foreach (Form f in formulariosApp)
                    {
                        if (f.Name != "Inicio")                                                               // cuando el nombre del formulario sea distinto de IniciarSesion, que es el form inicial de la app, se van a cerrar.
                        {
                            f.Close();
                        }
                    }                                                                                                                          // abrimos el form supervisor.
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
                    FormCollection formulariosApp = Application.OpenForms;

                    foreach (Form f in formulariosApp)
                    {
                        if (f.Name != "Inicio")                                                               // cuando el nombre del formulario sea distinto de IniciarSesion, que es el form inicial de la app, se van a cerrar.
                        {
                            f.Close();
                        }
                    }
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
                    FormCollection formulariosApp = Application.OpenForms;

                    foreach (Form f in formulariosApp)
                    {
                        if (f.Name != "Inicio")                                                               // cuando el nombre del formulario sea distinto de IniciarSesion, que es el form inicial de la app, se van a cerrar.
                        {
                            f.Close();
                        }
                    }                                                                                                                        // abrimos el form supervisor.
                }
                else
                {
                    captcha = "";                                                                                                                                 // si el captcha no es 1234, restablecemos la variable a vacio para ingresar de nuevo.
                }
            }
        }
    }
}
