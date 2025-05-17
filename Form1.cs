using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serger_winforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Settings button - menu

            pingerPanel.Visible = false;
            settingsPanel.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Settings - submit button

            // tady nastavit vsechny hodnoty do configu
            ping.Enabled = true;
            ping.Interval = Convert.ToInt32(pingDelay);
        }

        private void ping_Tick(object sender, EventArgs e)
        {
            // ping timer

            // tady pingovat
            // final hodnotu poslat do:
            // pingResult.Text = .....
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Pinger button - menu
            
            settingsPanel.Visible = false;
            pingerPanel.Visible = true;
        }
    }
}
