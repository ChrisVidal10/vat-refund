namespace VATAduana
{
    partial class Menu
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
            //base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.metroTileLpg = new MetroFramework.Controls.MetroTile();
            this.metroTileAduana = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // metroTileLpg
            // 
            this.metroTileLpg.ActiveControl = null;
            this.metroTileLpg.Location = new System.Drawing.Point(62, 89);
            this.metroTileLpg.Name = "metroTileLpg";
            this.metroTileLpg.PaintTileCount = false;
            this.metroTileLpg.Size = new System.Drawing.Size(170, 68);
            this.metroTileLpg.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroTileLpg.TabIndex = 0;
            this.metroTileLpg.Text = "Granos";
            this.metroTileLpg.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTileLpg.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTileLpg.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.metroTileLpg.UseSelectable = true;
            this.metroTileLpg.Click += new System.EventHandler(this.metroTileLpg_Click);
            // 
            // metroTileAduana
            // 
            this.metroTileAduana.ActiveControl = null;
            this.metroTileAduana.Location = new System.Drawing.Point(62, 179);
            this.metroTileAduana.Name = "metroTileAduana";
            this.metroTileAduana.PaintTileCount = false;
            this.metroTileAduana.Size = new System.Drawing.Size(170, 68);
            this.metroTileAduana.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroTileAduana.TabIndex = 1;
            this.metroTileAduana.Text = "Aduana (Beta)";
            this.metroTileAduana.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTileAduana.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTileAduana.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.metroTileAduana.UseSelectable = true;
            this.metroTileAduana.Click += new System.EventHandler(this.metroTileAduana_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.metroTileAduana);
            this.Controls.Add(this.metroTileLpg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Menu (Web Services)";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTile metroTileLpg;
        private MetroFramework.Controls.MetroTile metroTileAduana;
    }
}