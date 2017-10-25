using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SQLite;
using MetroFramework.Forms;
using MetroFramework;
using Newtonsoft.Json;
using System.Threading;

namespace VATAduana
{
    public partial class formularioMOA : MetroForm
    {
        MoaEmbarques _MoaEmb = new MoaEmbarques();
        string service = "wconsdeclaracion";
        List<string> consultaResponse = new List<string>();
        List<wconsdeclaracion.DeclaracionDetalladaMOACaratula> listaCaratulas = new List<wconsdeclaracion.DeclaracionDetalladaMOACaratula>();
        List<wconsdeclaracion.DeclaracionDetalladaMOAEstado> listaEstados = new List<wconsdeclaracion.DeclaracionDetalladaMOAEstado>();

        public formularioMOA()
        {
            InitializeComponent();
        }

        private void cancelar_Click(object sender, EventArgs e)
        {

        }

        private void aceptar_Click(object sender, EventArgs e)
        {            
           
        }

        private void formularioMOA_Load(object sender, EventArgs e)
        {
            Data.Conexion();
        }

        private void metroButtonAceptar_Click(object sender, EventArgs e)
        {
            string radio;
            Int64 numeric;
            bool parsed = Int64.TryParse(TextBoxCUIT.Text, out numeric);

            if (metroRadioButtonExpo.Checked)
            {
                radio = "E";
            }else if (metroRadioButtonImpo.Checked)
            {
                radio = "I";
            }
            else
            {
                radio = null;
            }

            if (TextBoxCUIT.Text.Length == TextBoxCUIT.MaxLength && parsed)
            {
                if (radio == "E" || radio == "I")
                {
                    if (!Data.VerificarTokenActivo(TextBoxCUIT.Text))
                    {
                        //Si esta expirado lo hace desde cero, crea toda la estructura para hacer el pedido al service de autenticacion
                        try
                        {
                            formularioCertificado frm = new formularioCertificado();
                            frm.Cuit = TextBoxCUIT.Text;
                            frm.inicio = dateTimeInicio.Value;
                            frm.fin = dateTimeFin.Value;
                            frm.servicio = this.service;

                            if (frm.ShowDialog() != DialogResult.No)
                            {
                                try
                                {
                                    if (!_MoaEmb.dummyServers())
                                    {
                                        MetroMessageBox.Show(this, "El servicio web no esta disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            consultaResponse = _MoaEmb.consultarDetalle(frm.token, frm.sign, TextBoxCUIT.Text, dateTimeInicio.Value, dateTimeFin.Value, radio);
                                        }
                                        catch (Exception ex)
                                        {

                                            MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        
                                        foreach (var identificador in consultaResponse)
                                        {
                                            listaCaratulas.Add(_MoaEmb.consultarCaratula(frm.token, frm.sign, TextBoxCUIT.Text, identificador));
                                            listaEstados.Add(_MoaEmb.consultarEstado(frm.token, frm.sign, TextBoxCUIT.Text, identificador));
                                            Thread.Sleep(2000);
                                        }
                                        ExcelService.ExcelCreateEmbarques(consultaResponse, listaCaratulas, listaEstados);
                                        MetroMessageBox.Show(this, "Excel creado exitosamente", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //Metodo que envie el objeto y haga las demas consulta con su for
                                        //metodo que arme y llame el excel
                                        //Termino de llenarse el excel, cierro la app?
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                //SACAR
                                Console.WriteLine("Cerro Dialogo");
                            }

                        }
                        catch (Exception ex)
                        {
                            MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        //Si esta activo busca en la bd el ultimo token y sign y solo hace la peticion al service del negocio
                        //Llamo al WSN
                        try
                        {
                            if (!_MoaEmb.dummyServers())
                            {
                                MetroMessageBox.Show(this, "El servicio web no esta disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                String[] infoTicket = Data.selectInfoTicket(TextBoxCUIT.Text);                           
                                consultaResponse = _MoaEmb.consultarDetalle(infoTicket[0], infoTicket[1], TextBoxCUIT.Text, dateTimeInicio.Value, dateTimeFin.Value, radio);

                                foreach (var identificador in consultaResponse)
                                {
                                    listaCaratulas.Add(_MoaEmb.consultarCaratula(infoTicket[0], infoTicket[1], TextBoxCUIT.Text, identificador));
                                    listaEstados.Add(_MoaEmb.consultarEstado(infoTicket[0], infoTicket[1], TextBoxCUIT.Text, identificador));
                                    Thread.Sleep(2000);
                                }
                                ExcelService.ExcelCreateEmbarques(consultaResponse, listaCaratulas, listaEstados);
                                MetroMessageBox.Show(this, "Excel creado exitosamente", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //Metodo que envie el objeto y haga las demas consulta con su for
                                //metodo que arme y llame el excel
                                //Termino de llenarse el excel, cierro la app?
                            }
                        }
                        catch (Exception ex)
                        {
                            MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                }
                else
                {
                    MetroMessageBox.Show(this, "Escoja el tipo de operación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MetroMessageBox.Show(this, "Ingrese correctamente el CUIT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        
        }

        private void metroButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void metroLabel5_Click(object sender, EventArgs e)
        {

        }
    }
}
