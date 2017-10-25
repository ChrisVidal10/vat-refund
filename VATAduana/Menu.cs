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

namespace VATAduana
{
    public partial class Menu : MetroForm
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void metroTileLpg_Click(object sender, EventArgs e)
        {
            this.Hide();
            Granos formGranos = new Granos();
            if (formGranos.ShowDialog() == DialogResult.No)
            {
                formGranos.Close();
                this.Show();
            }
            
        }

        private void metroTileAduana_Click(object sender, EventArgs e)
        {
            this.Hide();
            formularioMOA formGranos = new formularioMOA();
            if (formGranos.ShowDialog() == DialogResult.No)
            {
                this.Show();                
            }
        }
    }
}
