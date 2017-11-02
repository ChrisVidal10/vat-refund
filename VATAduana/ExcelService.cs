using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace VATAduana
{
    class ExcelService
    {

        public static void ExcelCreateEmbarques(List<string> destinaciones, List<wconsdeclaracion.DeclaracionDetalladaMOACaratula> caratula, List<wconsdeclaracion.DeclaracionDetalladaMOAEstado> estado, List<wconsdeclaracion.DetalladaItemsRta> items, List<List<wconsdeclaracion.SubItemMOADeclaracionDetalladaSubitem>> subItems, List<Asociacion> lstAsociaciones)
        {
            // Load Excel application
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            // Create empty workbook
            excel.Workbooks.Add();

            // Create Worksheet from active sheet
            Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;
            workSheet.Name = "Caratula y Estado";

            // I created Application and Worksheet objects before try/catch,
            // so that i can close them in finnaly block.
            // It's IMPORTANT to release these COM objects!!
            try
            {
                // ------------------------------------------------
                // Creation of header cells
                // ------------------------------------------------
                //Caratula
                workSheet.Cells[1, "A"] = "Destinacion";
                workSheet.Cells[1, "B"] = "Canal seleccion";
                workSheet.Cells[1, "C"] = "Proveedor destinatario";
                workSheet.Cells[1, "D"] = "Monto fob total";
                workSheet.Cells[1, "E"] = "Moneda fob total";
                workSheet.Cells[1, "F"] = "Monto flete total";
                workSheet.Cells[1, "G"] = "Codigo moneda flete";
                workSheet.Cells[1, "H"] = "Monto seguro total";
                workSheet.Cells[1, "I"] = "Codigo moneda seguro";
                workSheet.Cells[1, "J"] = "Identificador manifiesto";
                workSheet.Cells[1, "K"] = "Codigo documento transporte";
                workSheet.Cells[1, "L"] = "CUit agente de transporte";
                workSheet.Cells[1, "M"] = "Fecha ventcimiento temporal";
                workSheet.Cells[1, "N"] = "Fecha arribo mercaderia";
                workSheet.Cells[1, "O"] = "Total bultos";
                workSheet.Cells[1, "P"] = "Peso bruto";
                workSheet.Cells[1, "Q"] = "Peso guia";
                workSheet.Cells[1, "R"] = "Motivo anulacion";

                //Estado
                workSheet.Cells[1, "S"] = "Fecha presentacion";
                workSheet.Cells[1, "T"] = "Fecha autorizacion retiro";
                workSheet.Cells[1, "U"] = "Fecha salida";
                workSheet.Cells[1, "V"] = "Fecha Cancelacion";
                workSheet.Cells[1, "W"] = "Fecha oficializacion permiso embarque original";
                workSheet.Cells[1, "X"] = "Fecha presentacion permiso embarque original";
                workSheet.Cells[1, "Y"] = "Fecha precumplido";
                workSheet.Cells[1, "Z"] = "Fecha ventcimiento embarque";
                workSheet.Cells[1, "AA"] = "Indicador precumplido";
                workSheet.Cells[1, "AB"] = "Indicador cumplido";
                workSheet.Cells[1, "AC"] = "Indicador control unidades conforme";

                // ------------------------------------------------
                // Recorro las listas para poder pegar la informacion
                // ------------------------------------------------
                int row = 2; // start row (in row 1 are header cells)
                foreach (string dest in destinaciones)
                {
                    workSheet.Cells[row, "A"] = dest;
                    row++;
                }

                row = 2;
                foreach (wconsdeclaracion.DeclaracionDetalladaMOACaratula car in caratula)
                {
                    workSheet.Cells[row, "B"] = car.CanalSeleccion;
                    workSheet.Cells[row, "C"] = car.NombreProveedorDestinatario;
                    workSheet.Cells[row, "D"] = car.MontoFobTotal;
                    workSheet.Cells[row, "E"] = car.CodigoMonedaFobTotal;
                    workSheet.Cells[row, "F"] = car.MontoFleteTotal;
                    workSheet.Cells[row, "G"] = car.CodigoMonedaFlete;
                    workSheet.Cells[row, "H"] = car.MontoSeguroTotal;
                    workSheet.Cells[row, "I"] = car.CodigoMonedaSeguro;
                    workSheet.Cells[row, "J"] = car.IdentificadorManifiesto;
                    workSheet.Cells[row, "K"] = car.CodigoDocumentoTransporte;
                    workSheet.Cells[row, "L"] = car.CuitAgenteDeTransporte;
                    workSheet.Cells[row, "M"] = car.FechaVencimientoTemporal;
                    workSheet.Cells[row, "N"] = car.FechaArriboMercaderia;
                    workSheet.Cells[row, "O"] = car.TotalBultos;
                    workSheet.Cells[row, "P"] = car.PesoBruto;
                    workSheet.Cells[row, "Q"] = car.PesoGuia;
                    workSheet.Cells[row, "R"] = car.MotivoAnulacion;
                    row++;
                }

                row = 2;
                foreach (wconsdeclaracion.DeclaracionDetalladaMOAEstado est in estado)
                {
                    workSheet.Cells[row, "S"] = est.FechaPresentacion;
                    workSheet.Cells[row, "T"] = est.FechaAutorizacionRetiro;
                    workSheet.Cells[row, "U"] = est.FechaSalida;
                    workSheet.Cells[row, "V"] = est.FechaCancelacion;
                    workSheet.Cells[row, "W"] = est.FechaOficializacionPermisoEmbarqueOriginal;
                    workSheet.Cells[row, "X"] = est.FechaPresentacionPermisoEmbarqueOriginal;
                    workSheet.Cells[row, "Y"] = est.FechaPrecumplido;
                    workSheet.Cells[row, "Z"] = est.FechaVencimientoEmbarque;
                    workSheet.Cells[row, "AA"] = est.IndicadorPrecumplido;
                    workSheet.Cells[row, "AB"] = est.IndicadorCumplido;
                    workSheet.Cells[row, "AC"] = est.IndicadorControlUnidadesConforme;
                    row++;
                }

                //------------------------Hoja 2----------------------------------

                excel.Sheets.Add();
                //Agrego una hoja 
                // Create Worksheet2 from active sheet, osea segunda hoja
                Microsoft.Office.Interop.Excel._Worksheet workSheet2 = excel.ActiveSheet;
                workSheet2.Name = "Items";

                workSheet2.Cells[1, "A"] = "Destinacion";
                workSheet2.Cells[1, "B"] = "Identificador Item";
                workSheet2.Cells[1, "C"] = "Cantidad Estadistica";
                workSheet2.Cells[1, "D"] = "Cantidad Unidad Declarada";
                workSheet2.Cells[1, "E"] = "Codigo Pais Origen";
                workSheet2.Cells[1, "F"] = "Codigo Pais Procedencia";
                workSheet2.Cells[1, "G"] = "Codigo Unidad Declarada";
                workSheet2.Cells[1, "H"] = "Codigo Unidad Estadistica";
                workSheet2.Cells[1, "I"] = "Estado Uso Mercaderia";
                workSheet2.Cells[1, "J"] = "Codigo Acuerdo"; 
                workSheet2.Cells[1, "K"] = "Monto Ajuste Deducir Dolar";
                workSheet2.Cells[1, "L"] = "Monto Ajuste Incluir Dolar";
                workSheet2.Cells[1, "M"] = "Monto Fob Divisa";
                workSheet2.Cells[1, "N"] = "Monto Insumos Importacion A Consumo";
                workSheet2.Cells[1, "O"] = "Monto Insumos Importacion Temporal";
                workSheet2.Cells[1, "P"] = "Monto Unitario";
                workSheet2.Cells[1, "Q"] = "PosicionArancelariaSIM";

                row = 2;
                int index = 0;
                //Indice para recorrer la lista de declaraciones

                foreach (var listaItems in items)
                //Para cada lista de items que tenemos guardadas en items    
                {
                    foreach (var item in listaItems.Items)
                    //Vamos guardando cada item todos con el mismo identificador
                    {
                        workSheet2.Cells[row, "A"] = destinaciones[index];
                        workSheet2.Cells[row, "B"] = item.IdentificadorItem;
                        workSheet2.Cells[row, "C"] = item.CantidadEstadistica;
                        workSheet2.Cells[row, "D"] = item.CantidadUnidadDeclarada;
                        workSheet2.Cells[row, "E"] = item.CodigoPaisOrigen;
                        workSheet2.Cells[row, "F"] = item.CodigoPaisProcedencia;
                        workSheet2.Cells[row, "G"] = item.CodigoUnidadDeclarada;
                        workSheet2.Cells[row, "H"] = item.CodigoUnidadEstadistica;
                        workSheet2.Cells[row, "I"] = item.EstadoUsoMercaderia;
                        workSheet2.Cells[row, "J"] = item.CodigoAcuerdo;
                        workSheet2.Cells[row, "K"] = item.MontoAjusteDeducirDolar;
                        workSheet2.Cells[row, "L"] = item.MontoAjusteIncluirDolar;
                        workSheet2.Cells[row, "M"] = item.MontoFobDivisa;
                        workSheet2.Cells[row, "N"] = item.MontoInsumosImportacionAConsumo;
                        workSheet2.Cells[row, "O"] = item.MontoInsumosImportacionTemporal;
                        workSheet2.Cells[row, "P"] = item.MontoUnitario;
                        workSheet2.Cells[row, "Q"] = item.PosicionArancelariaSIM;
                        row++;
                    }
                    index++;
                    //Vamos avanzando el index para tener el siguiente identificador 
                }

                //------------------------Hoja 3----------------------------------

                excel.Sheets.Add();
                //Agrego una hoja 
                // Create Worksheet2 from active sheet, osea segunda hoja
                Microsoft.Office.Interop.Excel._Worksheet workSheet3 = excel.ActiveSheet;
                workSheet3.Name = "SubItems";

                workSheet3.Cells[1, "A"] = "Destinacion";
                workSheet3.Cells[1, "B"] = "Identificador Item";
                workSheet3.Cells[1, "C"] = "Sufijo Valor SubItem";
                workSheet3.Cells[1, "D"] = "Numero SubItem";
                workSheet3.Cells[1, "E"] = "Cantidad Declarada"; 
                workSheet3.Cells[1, "F"] = "Cantidad Estadistica";
                workSheet3.Cells[1, "G"] = "Codigo Unidad Declarada";
                workSheet3.Cells[1, "H"] = "Codigo Unidad Estadistica";
                workSheet3.Cells[1, "I"] = "Monto Fob Dolar";               
                workSheet3.Cells[1, "J"] = "Precio Unitario";
                

                row = 2;
                //Indice para recorrer la lista de declaraciones
                int identificador = 0;


                foreach (var listaSbItem in subItems)
                //Cada posicion tiene una lista de subItems asociada a un identificador 
                {
                    foreach (var sbItem in listaSbItem)
                    //Cada posicion tiene un subItem de ese identificador 
                    {
                        Asociacion auxAsociacion = new Asociacion();
                        //Auxiliar de asociacion para poder llamar a la funcion que nos devuelve el id de item
                        workSheet3.Cells[row, "A"] = destinaciones[identificador];
                        workSheet3.Cells[row, "B"] = auxAsociacion.devolverIdentificadorItem(lstAsociaciones, destinaciones[identificador], sbItem.SufijoValor); ;
                        //Llamamos a la funcion para tener el id de item asociado a ese subitem
                        workSheet3.Cells[row, "C"] = sbItem.SufijoValor;
                        workSheet3.Cells[row, "D"] = sbItem.NumeroSubItem;
                        workSheet3.Cells[row, "E"] = sbItem.CantidadDeclarada;
                        workSheet3.Cells[row, "F"] = sbItem.CantidadEstadistica;
                        workSheet3.Cells[row, "G"] = sbItem.CodigoUnidadDeclarada;
                        workSheet3.Cells[row, "H"] = sbItem.CodigoUnidadEstadistica;
                        workSheet3.Cells[row, "I"] = sbItem.MontoFobDolar;
                        workSheet3.Cells[row, "J"] = sbItem.PrecioUnitario;
                        row++;
                    }
                    identificador++;
                }


                //foreach (var listasSbItem in subItems)
                ////Cada posicion tiene listas de subItems asociadas a un identificador 
                //{
                //    foreach (var lstSbItem in listasSbItem)
                //    //Cada posicion tiene una lista de subItems
                //    {
                //        foreach (var sbItem in lstSbItem.s Subitems)
                //        //Cada posicion tiene un subItem que tenemos que cargar
                //        {
                //            if (lstSbItem.Subitems != null)
                //            {
                //                Asociacion auxAsociacion = new Asociacion();    
                //                //Auxiliar de asociacion para poder llamar a la funcion que nos devuelve el id de item
                //                workSheet3.Cells[row, "A"] = destinaciones[identificador];
                //                workSheet3.Cells[row, "B"] = auxAsociacion.devolverIdentificadorItem(lstAsociaciones, destinaciones[identificador] , sbItem.SufijoValor); ;
                //                //Llamamos a la funcion para tener el id de item asociado a ese subitem
                //                workSheet3.Cells[row, "C"] = sbItem.SufijoValor;
                //                workSheet3.Cells[row, "D"] = sbItem.NumeroSubItem;
                //                workSheet3.Cells[row, "E"] = sbItem.CantidadDeclarada;
                //                workSheet3.Cells[row, "F"] = sbItem.CantidadEstadistica;
                //                workSheet3.Cells[row, "G"] = sbItem.CodigoUnidadDeclarada;
                //                workSheet3.Cells[row, "H"] = sbItem.CodigoUnidadEstadistica;
                //                workSheet3.Cells[row, "I"] = sbItem.MontoFobDolar;
                //                workSheet3.Cells[row, "J"] = sbItem.PrecioUnitario;
                //                row++;
                //            }
                //        }
                //    }
                //    identificador++;
                //    //Incrementa destinacion porque se completo el recorrido de ese identificador
                //}



                // Apply some predefined styles for data to look nicely :)
                workSheet.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);
                workSheet2.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);
                workSheet3.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);

                // Define filename
                string fileName = string.Format(@"{0}\ExcelAduana.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

                // Save this data as a file
                workSheet.SaveAs(fileName);
            }
            catch (Exception exception)
            {
                //MessageBox.Show("Exception","There was a PROBLEM saving Excel file!\n" + exception.Message,MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("***Hubo un problema crando archivo de excel " + exception.Message);
            }
            finally
            {
                // Quit Excel application
                excel.Quit();

                // Release COM objects (very important!)
                if (excel != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

                if (workSheet != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);

                // Empty variables
                excel = null;
                workSheet = null;

                // Force garbage collector cleaning
                GC.Collect();
            }
        }

        
    }
}

