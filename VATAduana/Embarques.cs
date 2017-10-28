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
        List <List<wconsdeclaracion.DetalladaSubitemRta>> listaSubItems = new List<List<wconsdeclaracion.DetalladaSubitemRta>>();

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
                                            //ver listaItem 
                                            // Thread.Sleep(2000);    
                                        }
                                        ExcelService.ExcelCreateEmbarques(consultaResponse, listaCaratulas, listaEstados, listaItems, listaSubItems, asociaciones);
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
                            List<string> listaIdentificadoresFallidos = new List<string>();
                            //Lista donde se guardan identificadores que tienen una falla en sus cargas para despues sacarlos de la lista principal
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
                                    List<wconsdeclaracion.DetalladaSubitemRta> auxListaSubItems = new List<wconsdeclaracion.DetalladaSubitemRta>();
                                    try
                                    {
                                        auxCaratula = _MoaEmb.consultarCaratula(infoTicket[0], infoTicket[1], TextBoxCUIT.Text, identificador);
                                        auxEstado = _MoaEmb.consultarEstado(infoTicket[0], infoTicket[1], TextBoxCUIT.Text, identificador);
                                        auxItems = _MoaEmb.consultarItems(infoTicket[0], infoTicket[1], TextBoxCUIT.Text, identificador);

                                        foreach (var item in auxItems.Items)
                                        {                     
                                            auxSubItems = _MoaEmb.consultarSubItems(infoTicket[0], infoTicket[1], TextBoxCUIT.Text, identificador, item.IdentificadorItem);
                                            if (auxSubItems.Subitems != null)
                                            {
                                                Asociacion auxAsociacion = new Asociacion(identificador,item.IdentificadorItem,auxSubItems.Subitems[0].SufijoValor);
                                                //La posicion 0 de Subitems porque todas las busquedas que hicimos traen un subitem por consulta, si hubiera mas esa posicon deberia variar
                                                //Auxiliar que usamos para ir guardando en la lista de asociaciones, lo llenamos con los valores actuales
                                                auxListaSubItems.Add(auxSubItems);
                                                asociaciones.Add(auxAsociacion);
                                                //Cuando se agrega un subItem se guarda la asociacion para despues imprimirla en el excel
                                            }
                                        }


                                        //Thread.Sleep(2000);
                                        //Funciona sin la espera, se le podria poner igual por las dudas 
                                    }
                                    catch (Exception ex)
                                    {
                                        listaIdentificadoresFallidos.Add(identificador);
                                        MetroMessageBox.Show(this, ex.Message + " El id que fallo es: " + identificador, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //Se saldria por aca en caso de que falle la conexion o un error similar
                                        //Queremos agarrar el error asi sacamos el identificador de la lista y continua el foreach
                                        //Esos errores se podrian guardar en un log para avisale al usuario
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
                                consultaResponse.Remove(identificador);
                            }                            

                            //Sacamos de la lista los identificadores que fallaron
                            ExcelService.ExcelCreateEmbarques(consultaResponse, listaCaratulas, listaEstados, listaItems, listaSubItems, asociaciones);
                            Cursor.Current = Cursors.Default;
                            MetroMessageBox.Show(this, "Excel creado exitosamente", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                            new Menu().Show();
                            //Por ahora volvemos a mostrar el menu desde de completar un excel
                            //Metodo que envie el objeto y haga las demas consulta con su for
                            //Metodo que arme y llame el excel
                            //Termino de llenarse el excel, cierro la app?
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
    }
}
