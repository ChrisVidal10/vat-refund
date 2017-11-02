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
        List<wconsdeclaracion.DetalladaItemsRta> listaItems = new List<wconsdeclaracion.DetalladaItemsRta>();
        List<List<wconsdeclaracion.SubItemMOADeclaracionDetalladaSubitem>> listaSubItems = new List<List<wconsdeclaracion.SubItemMOADeclaracionDetalladaSubitem>>();

        wconsdeclaracion.DeclaracionDetalladaMOACaratula auxCaratula;
        wconsdeclaracion.DeclaracionDetalladaMOAEstado auxEstado;
        wconsdeclaracion.DetalladaItemsRta auxItems;
        wconsdeclaracion.DetalladaSubitemRta auxSubItems;

        List<Asociacion> asociaciones = new List<Asociacion>();
        


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
            Cursor.Current = Cursors.WaitCursor;
            //A partir del click arranca el procesamiento, cambiamos el puntero para que el usuario lo entienda
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
                                cargarExcel(frm.token, frm.sign, radio);
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
                            String[] infoTicket = Data.selectInfoTicket(TextBoxCUIT.Text);
                            string auxToken = infoTicket[0];
                            string auxSign = infoTicket[1];
                            cargarExcel(auxToken, auxSign, radio);                           
                        }
                        catch (Exception ex)
                        {
                            MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MetroMessageBox.Show(this, "Elija un tipo de operación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MetroMessageBox.Show(this, "Ingrese correctamente el CUIT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Cursor.Current = Cursors.Default;
        }

        private void metroButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void metroLabel5_Click(object sender, EventArgs e)
        {
        }

        private void cargarExcel(string token, string sign, string radio)
        {
            try
            {
                List<List<string>> listaIdentificadoresFallidos = new List<List<string>>();
                //Lista donde se guardan identificadores que tienen una falla en sus cargas para despues sacarlos de la lista principal
                if (!_MoaEmb.dummyServers())
                {
                    MetroMessageBox.Show(this, "El servicio web no esta disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    consultaResponse = _MoaEmb.consultarDetalle(token, sign, TextBoxCUIT.Text, dateTimeInicio.Value, dateTimeFin.Value, radio);
                    foreach (var identificador in consultaResponse)
                    {
                        List<wconsdeclaracion.SubItemMOADeclaracionDetalladaSubitem> auxListaSubItems = new List<wconsdeclaracion.SubItemMOADeclaracionDetalladaSubitem>();
                        try
                        {
                            auxCaratula = _MoaEmb.consultarCaratula(token, sign, TextBoxCUIT.Text, identificador);
                            auxEstado = _MoaEmb.consultarEstado(token, sign, TextBoxCUIT.Text, identificador);
                            auxItems = _MoaEmb.consultarItems(token, sign, TextBoxCUIT.Text, identificador);
                            foreach (var item in auxItems.Items)
                            {                        
                                auxSubItems = _MoaEmb.consultarSubItems(token, sign, TextBoxCUIT.Text, identificador, item.IdentificadorItem);
                                if (auxSubItems.Subitems != null)
                                {
                                    foreach (var subItem in auxSubItems.Subitems)
                                    {
                                        Asociacion auxAsociacion = new Asociacion(identificador, item.IdentificadorItem, subItem.SufijoValor);
                                        auxListaSubItems.Add(subItem);
                                        //Auxiliar que usamos para ir guardando en la lista de asociaciones, lo llenamos con los valores actuales
                                        asociaciones.Add(auxAsociacion);
                                        //Cuando se agrega un subItem se guarda la asociacion para despues imprimirla en el excel
                                    }
                                }                                                            
                            }
                        }
                        catch (Exception ex)
                        {
                            listaIdentificadoresFallidos.Add(new List<string> { identificador, ex.Message.ToString() });



                            //Se saldria por aca en caso de que falle la conexion o un error similar
                            //Queremos agarrar el error asi sacamos el identificador de la lista y continua el foreach
                            //Se los quiere sacar asi no se imprimen en el excel final
                        }
                        if (auxCaratula != null && auxEstado != null && auxItems != null && auxListaSubItems != null)
                        //Van a ser null en caso de que ocurra un error en la carga de alguna
                        {
                            listaCaratulas.Add(auxCaratula);
                            listaEstados.Add(auxEstado);
                            listaItems.Add(auxItems);
                            listaSubItems.Add(auxListaSubItems);
                            auxEstado = null;
                            auxCaratula = null;
                            auxItems = null;
                            auxListaSubItems = null;
                            //Si no estan null se agregan y se las vuelve al estado null para hacer este mismo chequeo
                        }
                    }
                }
                foreach (var identificador in listaIdentificadoresFallidos)
                {
                    consultaResponse.Remove(identificador[0]);
                }
                //Sacamos de la lista los identificadores que fallaron
                ELog.createTxtErrores(listaIdentificadoresFallidos, "aduana");               
                ExcelService.ExcelCreateEmbarques(consultaResponse, listaCaratulas, listaEstados, listaItems, listaSubItems, asociaciones);
                //Cargamos el excel
                Cursor.Current = Cursors.Default;
                //Volvemos el cursos a su estado original
                MetroMessageBox.Show(this, "Excel creado exitosamente", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                new Menu().Show();
                //Por ahora volvemos a mostrar el menu despues de completar un excel, se podria tmb cerrar
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
}
