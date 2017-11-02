namespace VATAduana
{
    partial class Granos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Granos));
            this.listBoxCoes = new System.Windows.Forms.ListBox();
            this.textBoxCoe = new System.Windows.Forms.TextBox();
            this.textBoxCUIT = new System.Windows.Forms.TextBox();
            this.metroButtonCancelar = new MetroFramework.Controls.MetroButton();
            this.metroButtonAceptar = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroButtonAdd = new MetroFramework.Controls.MetroButton();
            this.metroButtonEliminar = new MetroFramework.Controls.MetroButton();
            this.metroRadioButtonLpg = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButtonLsg = new MetroFramework.Controls.MetroRadioButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroButtonCSV = new MetroFramework.Controls.MetroButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxCoes
            // 
            this.listBoxCoes.CausesValidation = false;
            this.listBoxCoes.FormattingEnabled = true;
            this.listBoxCoes.Location = new System.Drawing.Point(199, 171);
            this.listBoxCoes.Name = "listBoxCoes";
            this.listBoxCoes.Size = new System.Drawing.Size(293, 134);
            this.listBoxCoes.TabIndex = 0;
            // 
            // textBoxCoe
            // 
            this.textBoxCoe.Location = new System.Drawing.Point(184, 106);
            this.textBoxCoe.Name = "textBoxCoe";
            this.textBoxCoe.Size = new System.Drawing.Size(168, 20);
            this.textBoxCoe.TabIndex = 2;
            // 
            // textBoxCUIT
            // 
            this.textBoxCUIT.Location = new System.Drawing.Point(184, 72);
            this.textBoxCUIT.MaxLength = 11;
            this.textBoxCUIT.Name = "textBoxCUIT";
            this.textBoxCUIT.Size = new System.Drawing.Size(168, 20);
            this.textBoxCUIT.TabIndex = 1;
            this.textBoxCUIT.Text = "20223803491";
            // 
            // metroButtonCancelar
            // 
            this.metroButtonCancelar.Location = new System.Drawing.Point(385, 333);
            this.metroButtonCancelar.Name = "metroButtonCancelar";
            this.metroButtonCancelar.Size = new System.Drawing.Size(88, 30);
            this.metroButtonCancelar.TabIndex = 9;
            this.metroButtonCancelar.Text = "Cancelar";
            this.metroButtonCancelar.UseSelectable = true;
            this.metroButtonCancelar.Click += new System.EventHandler(this.metroButtonCancelar_Click);
            // 
            // metroButtonAceptar
            // 
            this.metroButtonAceptar.Location = new System.Drawing.Point(199, 333);
            this.metroButtonAceptar.Name = "metroButtonAceptar";
            this.metroButtonAceptar.Size = new System.Drawing.Size(88, 30);
            this.metroButtonAceptar.TabIndex = 8;
            this.metroButtonAceptar.Text = "Aceptar";
            this.metroButtonAceptar.UseSelectable = true;
            this.metroButtonAceptar.Click += new System.EventHandler(this.metroButtonAceptar_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(49, 72);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(110, 19);
            this.metroLabel1.TabIndex = 11;
            this.metroLabel1.Text = "CUIT Exportador:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(107, 105);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(39, 19);
            this.metroLabel2.TabIndex = 12;
            this.metroLabel2.Text = "COE:";
            // 
            // metroButtonAdd
            // 
            this.metroButtonAdd.Location = new System.Drawing.Point(49, 171);
            this.metroButtonAdd.Name = "metroButtonAdd";
            this.metroButtonAdd.Size = new System.Drawing.Size(97, 34);
            this.metroButtonAdd.TabIndex = 6;
            this.metroButtonAdd.Text = "Añadir";
            this.metroButtonAdd.UseSelectable = true;
            this.metroButtonAdd.Click += new System.EventHandler(this.metroButtonAdd_Click);
            // 
            // metroButtonEliminar
            // 
            this.metroButtonEliminar.Location = new System.Drawing.Point(49, 215);
            this.metroButtonEliminar.Name = "metroButtonEliminar";
            this.metroButtonEliminar.Size = new System.Drawing.Size(97, 35);
            this.metroButtonEliminar.TabIndex = 7;
            this.metroButtonEliminar.Text = "Eliminar";
            this.metroButtonEliminar.UseSelectable = true;
            this.metroButtonEliminar.Click += new System.EventHandler(this.metroButtonEliminar_Click);
            // 
            // metroRadioButtonLpg
            // 
            this.metroRadioButtonLpg.AutoSize = true;
            this.metroRadioButtonLpg.Location = new System.Drawing.Point(3, 17);
            this.metroRadioButtonLpg.Name = "metroRadioButtonLpg";
            this.metroRadioButtonLpg.Size = new System.Drawing.Size(89, 15);
            this.metroRadioButtonLpg.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroRadioButtonLpg.TabIndex = 3;
            this.metroRadioButtonLpg.Text = "Liq. Primaria";
            this.metroRadioButtonLpg.UseSelectable = true;
            // 
            // metroRadioButtonLsg
            // 
            this.metroRadioButtonLsg.AutoSize = true;
            this.metroRadioButtonLsg.Location = new System.Drawing.Point(3, 38);
            this.metroRadioButtonLsg.Name = "metroRadioButtonLsg";
            this.metroRadioButtonLsg.Size = new System.Drawing.Size(103, 15);
            this.metroRadioButtonLsg.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroRadioButtonLsg.TabIndex = 4;
            this.metroRadioButtonLsg.Text = "Liq. Secundaria";
            this.metroRadioButtonLsg.UseSelectable = true;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroRadioButtonLsg);
            this.metroPanel1.Controls.Add(this.metroRadioButtonLpg);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(385, 63);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(120, 93);
            this.metroPanel1.TabIndex = 18;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroButtonCSV
            // 
            this.metroButtonCSV.Location = new System.Drawing.Point(49, 265);
            this.metroButtonCSV.Name = "metroButtonCSV";
            this.metroButtonCSV.Size = new System.Drawing.Size(97, 35);
            this.metroButtonCSV.TabIndex = 19;
            this.metroButtonCSV.Text = "Importar CSV";
            this.metroButtonCSV.UseSelectable = true;
            this.metroButtonCSV.Click += new System.EventHandler(this.metroButtonCSV_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialogCSV";
            // 
            // Granos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(528, 386);
            this.Controls.Add(this.metroButtonCSV);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.metroButtonEliminar);
            this.Controls.Add(this.metroButtonAdd);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroButtonAceptar);
            this.Controls.Add(this.metroButtonCancelar);
            this.Controls.Add(this.textBoxCUIT);
            this.Controls.Add(this.textBoxCoe);
            this.Controls.Add(this.listBoxCoes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Granos";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Consulta de Granos";
            this.Load += new System.EventHandler(this.Granos_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxCoes;
        private System.Windows.Forms.TextBox textBoxCoe;
        private System.Windows.Forms.TextBox textBoxCUIT;
        private MetroFramework.Controls.MetroButton metroButtonCancelar;
        private MetroFramework.Controls.MetroButton metroButtonAceptar;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton metroButtonAdd;
        private MetroFramework.Controls.MetroButton metroButtonEliminar;
        private MetroFramework.Controls.MetroRadioButton metroRadioButtonLpg;
        private MetroFramework.Controls.MetroRadioButton metroRadioButtonLsg;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton metroButtonCSV;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}