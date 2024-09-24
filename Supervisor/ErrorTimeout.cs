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
    public partial class ErrorTimeout : Form
    {
        public ErrorTimeout()
        {
            InitializeComponent();
        }

        private void ErrorTimeout_Click(object sender, EventArgs e)                                          // cuando se toca la pantalla, se cierran el resto de los forms y solo queda abierto el form de iniciar sesion.
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

        private void ErrorTimeout_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
        }
    }
}
