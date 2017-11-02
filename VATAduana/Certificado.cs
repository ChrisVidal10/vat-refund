using MetroFramework.Forms;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VATAduana
{
    public partial class formularioCertificado : MetroForm
    {
        public string Cuit { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public string servicio { get; set; }

        LoginTicket loginTicket = new LoginTicket();     

        public string token { get; set; }
        public string sign { get; set; }

        public formularioCertificado()
        {
            InitializeComponent();
        }

        private void metroButtonBuscar_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Certificate.
            OpenFileDialog dialogoCertificado = new OpenFileDialog();
            dialogoCertificado.Filter = "Certificate File|*.pfx";
            dialogoCertificado.Title = "Select a Security Certificate File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CRT file was selected, open it.
            if (dialogoCertificado.ShowDialog() == DialogResult.OK)
            {
                // Assign the Path into textbox             
                textBoxCertificado.Text = dialogoCertificado.FileName;
            }
        }

        private void metroButtonAceptar_Click(object sender, EventArgs e)
        {

            if (textBoxPass.Text != "")
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (loginTicket.ObtenerLoginTicketResponse(Cuit, this.servicio, textBoxCertificado.Text, textBoxPass.Text))
                    {
                        this.token = loginTicket.Token;
                        this.sign = loginTicket.Sign;
                        this.Close();
                    }
                }
                catch (Exception ee)
                {
                    this.Close();
                    MetroMessageBox.Show(this, ee.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    //Cursor.Current = Cursors.Default;
                }

            }
            else
            {
                MetroMessageBox.Show(this, "Complete el formulario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void metroButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
