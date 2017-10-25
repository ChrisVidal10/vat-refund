namespace VATAduana
{
    partial class formularioCertificado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formularioCertificado));
            this.textBoxCertificado = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.metroButtonBuscar = new MetroFramework.Controls.MetroButton();
            this.metroButtonAceptar = new MetroFramework.Controls.MetroButton();
            this.metroButtonCancelar = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // textBoxCertificado
            // 
            this.textBoxCertificado.Enabled = false;
            this.textBoxCertificado.Location = new System.Drawing.Point(104, 83);
            this.textBoxCertificado.Name = "textBoxCertificado";
            this.textBoxCertificado.Size = new System.Drawing.Size(148, 20);
            this.textBoxCertificado.TabIndex = 5;
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(104, 121);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.PasswordChar = '*';
            this.textBoxPass.Size = new System.Drawing.Size(148, 20);
            this.textBoxPass.TabIndex = 2;
            // 
            // metroButtonBuscar
            // 
            this.metroButtonBuscar.Location = new System.Drawing.Point(258, 75);
            this.metroButtonBuscar.Name = "metroButtonBuscar";
            this.metroButtonBuscar.Size = new System.Drawing.Size(97, 34);
            this.metroButtonBuscar.TabIndex = 8;
            this.metroButtonBuscar.Text = "Buscar";
            this.metroButtonBuscar.UseSelectable = true;
            this.metroButtonBuscar.Click += new System.EventHandler(this.metroButtonBuscar_Click);
            // 
            // metroButtonAceptar
            // 
            this.metroButtonAceptar.Location = new System.Drawing.Point(62, 164);
            this.metroButtonAceptar.Name = "metroButtonAceptar";
            this.metroButtonAceptar.Size = new System.Drawing.Size(88, 30);
            this.metroButtonAceptar.TabIndex = 9;
            this.metroButtonAceptar.Text = "Aceptar";
            this.metroButtonAceptar.UseSelectable = true;
            this.metroButtonAceptar.Click += new System.EventHandler(this.metroButtonAceptar_Click);
            // 
            // metroButtonCancelar
            // 
            this.metroButtonCancelar.Location = new System.Drawing.Point(202, 164);
            this.metroButtonCancelar.Name = "metroButtonCancelar";
            this.metroButtonCancelar.Size = new System.Drawing.Size(88, 30);
            this.metroButtonCancelar.TabIndex = 10;
            this.metroButtonCancelar.Text = "Cancelar";
            this.metroButtonCancelar.UseSelectable = true;
            this.metroButtonCancelar.Click += new System.EventHandler(this.metroButtonCancelar_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(22, 83);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(76, 19);
            this.metroLabel1.TabIndex = 11;
            this.metroLabel1.Text = "Certificado:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(22, 121);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(78, 19);
            this.metroLabel2.TabIndex = 12;
            this.metroLabel2.Text = "Contraseña:";
            // 
            // formularioCertificado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 228);
            this.ControlBox = false;
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroButtonCancelar);
            this.Controls.Add(this.metroButtonAceptar);
            this.Controls.Add(this.metroButtonBuscar);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.textBoxCertificado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "formularioCertificado";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Certificado Digital";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxCertificado;
        private System.Windows.Forms.TextBox textBoxPass;
        private MetroFramework.Controls.MetroButton metroButtonBuscar;
        private MetroFramework.Controls.MetroButton metroButtonAceptar;
        private MetroFramework.Controls.MetroButton metroButtonCancelar;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
    }
}