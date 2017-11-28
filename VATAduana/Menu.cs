using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;

namespace VATAduana
{
    public partial class Menu : MetroForm
    {
        public Menu()
        {
            InitializeComponent();
            this.FormClosing += Menu_FormClosing;
        }

        void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //In case windows is trying to shut down, don't hold the process up
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                Application.Exit();
            }
            
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Assume that X has been clicked and act accordingly.
                // Confirm user wants to close
                switch (MetroMessageBox.Show(this,"¿Está seguro que desea salir?", "VAT Refund", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    //Stay on this form
                    case DialogResult.No:
                        this.Show();
                        break;
                    default:
                        Application.Exit();
                        break;
                }
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void metroTileLpg_Click(object sender, EventArgs e)
        {
            Granos formGranos = new Granos();
            this.Hide();
            if (formGranos.ShowDialog() == DialogResult.No)
            {
                formGranos.Close();
                this.Show();
            }
            
        }

        private void metroTileAduana_Click(object sender, EventArgs e)
        {
            formularioMOA formGranos = new formularioMOA();
            this.Hide();
            if (formGranos.ShowDialog() == DialogResult.No)
            {
                this.Show();                
            }
        }
    }
}
