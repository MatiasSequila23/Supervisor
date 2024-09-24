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
    public partial class Iniciando : Form
    {
        int time = 0;
        public Iniciando()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                  
           if (time == 100)
           {
              timer1.Enabled = false;
              this.Close();
           }
           else
           {
                progressBar1.Maximum = 100;
                if (progressBar1.Value < 100)
                {
                   progressBar1.Value++;
                    txtPor.Text = progressBar1.Value.ToString() + "%";
                }
                //txtPor.Text = time.ToString() + "%";
                time++;
           }
           
        }

        private void Iniciando_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
