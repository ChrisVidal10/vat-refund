using System;

namespace VATAduana
{
    partial class formularioMOA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formularioMOA));
            this.dialogoCertificado = new System.Windows.Forms.OpenFileDialog();
            this.dateTimeInicio = new System.Windows.Forms.DateTimePicker();
            this.dateTimeFin = new System.Windows.Forms.DateTimePicker();
            this.TextBoxCUIT = new System.Windows.Forms.TextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroButtonAceptar = new MetroFramework.Controls.MetroButton();
            this.metroButtonCancelar = new MetroFramework.Controls.MetroButton();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroRadioButtonImpo = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButtonExpo = new MetroFramework.Controls.MetroRadioButton();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dialogoCertificado
            // 
            this.dialogoCertificado.FileName = "dialogoCertificado";
            // 
            // dateTimeInicio
            // 
            this.dateTimeInicio.Location = new System.Drawing.Point(88, 187);
            this.dateTimeInicio.MaxDate = new System.DateTime(2017, 10, 13, 0, 0, 0, 0);
            this.dateTimeInicio.Name = "dateTimeInicio";
            this.dateTimeInicio.Size = new System.Drawing.Size(148, 20);
            this.dateTimeInicio.TabIndex = 2;
            this.dateTimeInicio.Value = new System.DateTime(2017, 10, 13, 0, 0, 0, 0);
            // 
            // dateTimeFin
            // 
            this.dateTimeFin.Location = new System.Drawing.Point(88, 215);
            this.dateTimeFin.MaxDate = new System.DateTime(2017, 10, 13, 0, 0, 0, 0);
            this.dateTimeFin.Name = "dateTimeFin";
            this.dateTimeFin.Size = new System.Drawing.Size(148, 20);
            this.dateTimeFin.TabIndex = 3;
            this.dateTimeFin.Value = new System.DateTime(2017, 10, 13, 0, 0, 0, 0);
            // 
            // TextBoxCUIT
            // 
            this.TextBoxCUIT.Location = new System.Drawing.Point(89, 76);
            this.TextBoxCUIT.MaxLength = 11;
            this.TextBoxCUIT.Name = "TextBoxCUIT";
            this.TextBoxCUIT.Size = new System.Drawing.Size(172, 20);
            this.TextBoxCUIT.TabIndex = 1;
            this.TextBoxCUIT.Text = "20000000036";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 160);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(59, 19);
            this.metroLabel1.TabIndex = 11;
            this.metroLabel1.Text = "Periodo:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(42, 76);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(40, 19);
            this.metroLabel2.TabIndex = 12;
            this.metroLabel2.Text = "CUIT:";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(40, 188);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(42, 19);
            this.metroLabel3.TabIndex = 13;
            this.metroLabel3.Text = "Inicio:";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(53, 215);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(29, 19);
            this.metroLabel4.TabIndex = 14;
            this.metroLabel4.Text = "Fin:";
            // 
            // metroButtonAceptar
            // 
            this.metroButtonAceptar.Location = new System.Drawing.Point(40, 262);
            this.metroButtonAceptar.Name = "metroButtonAceptar";
            this.metroButtonAceptar.Size = new System.Drawing.Size(88, 30);
            this.metroButtonAceptar.TabIndex = 15;
            this.metroButtonAceptar.Text = "Aceptar";
            this.metroButtonAceptar.UseSelectable = true;
            this.metroButtonAceptar.Click += new System.EventHandler(this.metroButtonAceptar_Click);
            // 
            // metroButtonCancelar
            // 
            this.metroButtonCancelar.Location = new System.Drawing.Point(173, 262);
            this.metroButtonCancelar.Name = "metroButtonCancelar";
            this.metroButtonCancelar.Size = new System.Drawing.Size(88, 30);
            this.metroButtonCancelar.TabIndex = 16;
            this.metroButtonCancelar.Text = "Cancelar";
            this.metroButtonCancelar.UseSelectable = true;
            this.metroButtonCancelar.Click += new System.EventHandler(this.metroButtonCancelar_Click);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(23, 108);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(120, 19);
            this.metroLabel5.TabIndex = 17;
            this.metroLabel5.Text = "Tipo de operación:";
            this.metroLabel5.Click += new System.EventHandler(this.metroLabel5_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroRadioButtonExpo);
            this.metroPanel1.Controls.Add(this.metroRadioButtonImpo);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(149, 108);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(100, 45);
            this.metroPanel1.TabIndex = 18;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroRadioButtonImpo
            // 
            this.metroRadioButtonImpo.AutoSize = true;
            this.metroRadioButtonImpo.Location = new System.Drawing.Point(3, 4);
            this.metroRadioButtonImpo.Name = "metroRadioButtonImpo";
            this.metroRadioButtonImpo.Size = new System.Drawing.Size(88, 15);
            this.metroRadioButtonImpo.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroRadioButtonImpo.TabIndex = 2;
            this.metroRadioButtonImpo.Text = "Importación";
            this.metroRadioButtonImpo.UseSelectable = true;
            // 
            // metroRadioButtonExpo
            // 
            this.metroRadioButtonExpo.AutoSize = true;
            this.metroRadioButtonExpo.Location = new System.Drawing.Point(3, 25);
            this.metroRadioButtonExpo.Name = "metroRadioButtonExpo";
            this.metroRadioButtonExpo.Size = new System.Drawing.Size(85, 15);
            this.metroRadioButtonExpo.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroRadioButtonExpo.TabIndex = 3;
            this.metroRadioButtonExpo.Text = "Exportación";
            this.metroRadioButtonExpo.UseSelectable = true;
            // 
            // formularioMOA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 321);
            this.ControlBox = false;
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroButtonCancelar);
            this.Controls.Add(this.metroButtonAceptar);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.TextBoxCUIT);
            this.Controls.Add(this.dateTimeFin);
            this.Controls.Add(this.dateTimeInicio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "formularioMOA";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Declaraciones Detalladas";
            this.Load += new System.EventHandler(this.formularioMOA_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimeInicio;
        private System.Windows.Forms.DateTimePicker dateTimeFin;
        private System.Windows.Forms.OpenFileDialog dialogoCertificado;
        private System.Windows.Forms.TextBox TextBoxCUIT;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroButton metroButtonAceptar;
        private MetroFramework.Controls.MetroButton metroButtonCancelar;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroRadioButton metroRadioButtonExpo;
        private MetroFramework.Controls.MetroRadioButton metroRadioButtonImpo;
    }
}

