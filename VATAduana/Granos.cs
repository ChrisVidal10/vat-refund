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
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualBasic.FileIO;
using System.Threading;

namespace VATAduana
{
    public partial class Granos : MetroForm
    {

        List<string> _coes = new List<string>();
        LiqGranos _LiqGranos = new LiqGranos();    
        string service = "wslpg";
        object objConsulta = new object();
        List<wslpg.LpgLiqConsReturnType> ListaLiquidacionesPri = new List<wslpg.LpgLiqConsReturnType>();
        wslpg.LpgLiqConsReturnType auxLiquidacionesPri;
        List<wslpg.LsgConsultaReturnType> ListaLiquidacionesSec = new List<wslpg.LsgConsultaReturnType>();
        wslpg.LsgConsultaReturnType auxLiquidacionesSec;
        List<wslpg.LpgAjusteConsReturnType> ListaLiquidacionesAjus = new List<wslpg.LpgAjusteConsReturnType>();
        wslpg.LpgAjusteConsReturnType auxLiquidacionesAjus;
        List<string> erroresCoe = new List<string>();
        List<List<string>> listaErroresCoe = new List<List<string>>();

        public Granos()
        {
            InitializeComponent();
            listBoxCoes.DataSource = _coes;
            metroButtonEliminar.Enabled = false;
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void metroButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void metroButtonAceptar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Int64 numeric;
            bool parsed = Int64.TryParse(textBoxCUIT.Text, out numeric);
         

            if (textBoxCUIT.Text.Length == textBoxCUIT.MaxLength && parsed)
            {
                if (listBoxCoes.Items.Count != 0)
                {
                    if (metroRadioButtonLpg.Checked || metroRadioButtonLsg.Checked)
                    {
                        try
                        {
                            if (_LiqGranos.dummyServers())
                            {
                                if (!Data.VerificarTokenActivo(textBoxCUIT.Text))
                                {
                                    try
                                    {
                                        formularioCertificado frm = new formularioCertificado();
                                        frm.Cuit = textBoxCUIT.Text;
                                        frm.servicio = this.service;
                                        _LiqGranos.consultarGranos(frm.token, frm.sign, Convert.ToInt64(textBoxCUIT.Text));
                                        _LiqGranos.consultarPuertos(frm.token, frm.sign, Convert.ToInt64(textBoxCUIT.Text));
                                        _LiqGranos.consultarActividades(frm.token, frm.sign, Convert.ToInt64(textBoxCUIT.Text));

                                        if (frm.ShowDialog() != DialogResult.No)
                                        {
                                            consultaCoe(frm.token, frm.sign);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    this.Close();
                                }
                                else
                                {
                                    String[] infoTicket = Data.selectInfoTicket(textBoxCUIT.Text);                               
                                    consultaCoe(infoTicket[0],infoTicket[1]);
                                    this.Close();
                                }
                            }
                            else
                            {
                                MetroMessageBox.Show(this, "El servicio web no esta disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Elija un tipo de consulta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MetroMessageBox.Show(this, "Ingrese al menos un COE", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
            {                
                MetroMessageBox.Show(this, "Ingrese un CUIT valido", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void serializarXML(Type typeObj, object objASerializar, string name)
        {
            XmlSerializer xs = new XmlSerializer(typeObj);
            TextWriter txtWriter = new StreamWriter(string.Format(@"{0}\Excel{1}.xml", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),name));
            xs.Serialize(txtWriter, objASerializar);
            txtWriter.Close();
        }

        private void metroButtonAdd_Click(object sender, EventArgs e)
        {
            long _coe = new long();
            if (!string.IsNullOrWhiteSpace(textBoxCoe.Text) && Int64.TryParse(textBoxCoe.Text, out _coe))
            {
                if (listBoxCoes.FindStringExact(textBoxCoe.Text) == ListBox.NoMatches)
                {
                    // The Add button was clicked.
                    _coes.Add(textBoxCoe.Text);

                    // Change the DataSource.
                    listBoxCoes.DataSource = null;
                    listBoxCoes.DataSource = _coes;

                    metroButtonEliminar.Enabled = true;
                    textBoxCoe.ResetText();
                }
                else
                {
                    MetroMessageBox.Show(this,"COE ya se encuentra agregado en la lista", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MetroMessageBox.Show(this, "Ingrese un COE válido", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void metroButtonEliminar_Click(object sender, EventArgs e)
        {
            // The Remove button was clicked.
            int selectedIndex = listBoxCoes.SelectedIndex;

            try
            {
                // Remove the item in the List.
                _coes.RemoveAt(selectedIndex);
            }
            catch (Exception)
            {
                
                MetroMessageBox.Show(this, "Elija un coe para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            listBoxCoes.DataSource = null;
            listBoxCoes.DataSource = _coes;

            if (listBoxCoes.Items.Count == 0)
            {
                metroButtonEliminar.Enabled = false;
            }
        }

        private void Granos_Load(object sender, EventArgs e)
        {
            Data.Conexion();
        }

        private void metroButtonCSV_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Certificate.
            OpenFileDialog dialogoCertificado = new OpenFileDialog();
            dialogoCertificado.Filter = "Archivo CSV|*.csv";
            dialogoCertificado.Title = "Elija un archivo CSV";
            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CRT file was selected, open it.
            if (dialogoCertificado.ShowDialog() == DialogResult.OK)
            {
                // Assign the Path into textbox             
                Console.WriteLine(dialogoCertificado.FileName);
                readCSV(dialogoCertificado.FileName);
            }
        }

        private void readCSV(string path)
        {
            listBoxCoes.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {                
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;
                List<string> _coesCSV = new List<string>();
                try
                {
                    while (!csvParser.EndOfData)
                    {
                        string[] fields = csvParser.ReadFields();
                        foreach (string item in fields)
                        {
                            if (!_coesCSV.Any(s => s.Contains(item)))
                            {
                                _coesCSV.Add(item);
                            }
                        }
                    }
                    _coes = _coesCSV;
                    listBoxCoes.DataSource = null;
                    listBoxCoes.DataSource = _coes;
                    if (listBoxCoes.Items.Count == 0)
                    {
                        metroButtonEliminar.Enabled = false;
                    }
                    else
                    {
                        metroButtonEliminar.Enabled = true;
                    }
                    MetroMessageBox.Show(this, "Lista cargada exitosamente, se eliminaron duplicados", "Carga exitosa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception)
                {
                    MetroMessageBox.Show(this, "Error al importar CSV", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    listBoxCoes.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }

            }
          
        }

        private void consultaCoe(string token, string sign)
        {
            _LiqGranos.consultarGranos(token, sign, Convert.ToInt64(textBoxCUIT.Text));
            _LiqGranos.consultarPuertos(token, sign, Convert.ToInt64(textBoxCUIT.Text));
            _LiqGranos.consultarActividades(token, sign, Convert.ToInt64(textBoxCUIT.Text));

            if (metroRadioButtonLpg.Checked)
            {
                foreach (object item in listBoxCoes.Items)
                {
                    try
                    {
                        auxLiquidacionesAjus = _LiqGranos.consultarAjusXCoe(token, sign, Convert.ToInt64(textBoxCUIT.Text), Convert.ToInt64(item.ToString()));
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        listaErroresCoe.Add(new List<string> {item.ToString(), ex.Message.ToString()});               
                    }
                    try
                    {
                        auxLiquidacionesPri = _LiqGranos.consultarLiqXCoe(token, sign, Convert.ToInt64(textBoxCUIT.Text), Convert.ToInt64(item.ToString()));
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        listaErroresCoe.Add(new List<string> { item.ToString(), ex.Message.ToString() });
                    }
                    if (auxLiquidacionesAjus != null && auxLiquidacionesAjus.errores == null)
                    {
                        ListaLiquidacionesAjus.Add(auxLiquidacionesAjus);
                        auxLiquidacionesAjus = null;
                    }
                    if (auxLiquidacionesPri != null && auxLiquidacionesPri.errores == null)
                    {
                        ListaLiquidacionesPri.Add(auxLiquidacionesPri);
                        auxLiquidacionesPri = null;
                    }
                }
                try
                {
                    ELog.createTxtErrores(listaErroresCoe, "granos");
                    ExcelServiceGranos.ExcelCreateLiqPrimarias(ListaLiquidacionesPri, ListaLiquidacionesAjus, _LiqGranos.Codigos, _LiqGranos.Puertos, _LiqGranos.Actividades);
                    MetroMessageBox.Show(this, "Excel creado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Cursor.Current = Cursors.Default;
                
            }
            else
            {
                if (metroRadioButtonLsg.Checked)
                {
                    foreach (object item in listBoxCoes.Items)
                    {
                        try
                        {
                            auxLiquidacionesAjus = _LiqGranos.consultarAjusXCoe(token, sign, Convert.ToInt64(textBoxCUIT.Text), Convert.ToInt64(item.ToString()));
                            Thread.Sleep(1000);
                        }
                        catch (Exception ex)
                        {
                            listaErroresCoe.Add(new List<string> { item.ToString(), ex.Message.ToString() });
                        }
                        try
                        {
                            auxLiquidacionesSec = _LiqGranos.consultarLsgXCoe(token, sign, Convert.ToInt64(textBoxCUIT.Text), Convert.ToInt64(item.ToString()));
                            Thread.Sleep(1000);
                        }
                        catch (Exception ex)
                        {
                            listaErroresCoe.Add(new List<string> { item.ToString(), ex.Message.ToString() });
                        }
                        if (auxLiquidacionesAjus != null && auxLiquidacionesAjus.errores == null)
                        {
                            ListaLiquidacionesAjus.Add(auxLiquidacionesAjus);
                            auxLiquidacionesAjus = null;
                        }
                        if (auxLiquidacionesSec != null && auxLiquidacionesSec.errores == null)
                        {
                            ListaLiquidacionesSec.Add(auxLiquidacionesSec);
                            auxLiquidacionesSec = null;
                        }
                    }
                    try
                    {
                        ELog.createTxtErrores(listaErroresCoe, "granos");
                        ExcelServiceGranos.ExcelCreateLiqSecundarias(ListaLiquidacionesSec, ListaLiquidacionesAjus, _LiqGranos.Codigos, _LiqGranos.Puertos, _LiqGranos.Actividades);
                        MetroMessageBox.Show(this, "Excel creado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
            }
        }
}
