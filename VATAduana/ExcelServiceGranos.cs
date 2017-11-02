using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace VATAduana
{
    class ExcelServiceGranos
    {

        public static void ExcelCreateLiqPrimarias (List<wslpg.LpgLiqConsReturnType> listaLiquidaciones, List<wslpg.LpgAjusteConsReturnType> ListaAjuste, Dictionary<string,string> codigos, Dictionary<string, string> puertos, Dictionary<string, string> actividades)
        {
            // Load Excel application
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            // Create empty workbook
            excel.Workbooks.Add();

            int row = 2;

            // Create Worksheet from active sheet
            Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;
            workSheet.Name = "Primarias";

                // I created Application and Worksheet objects before try/catch,
                // so that i can close them in finnaly block.
                // It's IMPORTANT to release these COM objects!!
                try
                {
                    // ------------------------------------------------
                    // Creation of header cells
                    // ------------------------------------------------
                    //Liquidacion Primaria
                    workSheet.Cells[1, "A"] = "Fecha";
                    workSheet.Cells[1, "B"] = "Liquidacion";
                    workSheet.Cells[1, "C"] = "Tipo de operacion";
                    workSheet.Cells[1, "D"] = "Actividad";
                    workSheet.Cells[1, "E"] = "COE";
                    workSheet.Cells[1, "F"] = "Certificados";
                    workSheet.Cells[1, "G"] = "Comprador CUIT";
                    workSheet.Cells[1, "H"] = "Vendedor CUIT";
                    workSheet.Cells[1, "I"] = "¿Emitio corredor?";
                    workSheet.Cells[1, "J"] = "Corredor CUIT";
                    workSheet.Cells[1, "K"] = "Grano";
                    workSheet.Cells[1, "L"] = "Puerto";
                    workSheet.Cells[1, "M"] = "Cantidad (peso neto)";
                    workSheet.Cells[1, "N"] = "Precio /U. peso";
                    workSheet.Cells[1, "O"] = "Subtotal";
                    workSheet.Cells[1, "P"] = "% Alicuota IVA";
                    workSheet.Cells[1, "Q"] = "Importe IVA";
                    workSheet.Cells[1, "R"] = "Datos Adicionales";


                    // ------------------------------------------------
                    // Recorro las listas para poder pegar la informacion
                    // ------------------------------------------------
                    row = 2; // start row (in row 1 are header cells)
                    foreach (wslpg.LpgLiqConsReturnType liq in listaLiquidaciones)
                    {
                        workSheet.Cells[row, "A"] = liq.autorizacion.fechaLiquidacion;
                        workSheet.Cells[row, "B"] = "Primaria";
                        workSheet.Cells[row, "C"] = liq.autorizacion.codTipoOperacion;
                        workSheet.Cells[row, "D"] = (actividades.Count == 0) ? liq.liquidacion.nroActComprador.ToString() : string.Format(@"{0} - {1}", liq.liquidacion.nroActComprador, actividades[liq.liquidacion.nroActComprador.ToString()]);///liq.liquidacion.nroActComprador;
                        workSheet.Cells[row, "E"] = liq.autorizacion.coe;
                        foreach (var certi in liq.liquidacion.certificados)
                        {
                            workSheet.Cells[row, "F"] = string.Format(@"{0}{2} {1}", workSheet.Cells[row, "F"].Value, certi.nroCertificadoDeposito, (workSheet.Cells[row, "F"].Value == null) ? null : ",");
                        }
                        workSheet.Cells[row, "G"] = liq.liquidacion.cuitComprador;
                        workSheet.Cells[row, "H"] = liq.liquidacion.cuitVendedor;
                        workSheet.Cells[row, "I"] = (liq.liquidacion.liquidaCorredor == wslpg.LpgSiNoType.N) ? "NO" : "SI";
                        workSheet.Cells[row, "J"] = (liq.liquidacion.liquidaCorredor == wslpg.LpgSiNoType.N) ? null : liq.liquidacion.cuitCorredor.ToString();
                        workSheet.Cells[row, "K"] = (codigos.Count == 0) ? liq.liquidacion.codGrano.ToString() : string.Format(@"{0} - {1}", liq.liquidacion.codGrano, codigos[liq.liquidacion.codGrano.ToString()]);
                        workSheet.Cells[row, "L"] = (puertos.Count == 0) ? liq.liquidacion.codPuerto.ToString() : string.Format(@"{0} - {1}", liq.liquidacion.codPuerto, puertos[liq.liquidacion.codPuerto.ToString()]);
                        workSheet.Cells[row, "M"] = liq.autorizacion.totalPesoNeto;
                        workSheet.Cells[row, "N"] = Math.Round(Convert.ToDecimal(liq.autorizacion.subTotal) / Convert.ToDecimal(liq.autorizacion.totalPesoNeto), 2);
                        workSheet.Cells[row, "O"] = liq.autorizacion.subTotal;
                        workSheet.Cells[row, "P"] = liq.liquidacion.alicIvaOperacion;
                        workSheet.Cells[row, "Q"] = liq.autorizacion.importeIva;
                        workSheet.Cells[row, "R"] = liq.liquidacion.datosAdicionales;
                        row++;
                    }

                //LLamo a segunda cracion de hoja ajustes
                HojaAjustes(excel,ListaAjuste);

                // Apply some predefined styles for data to look nicely :)
                workSheet.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);
           
                // Define filename
                string fileName = string.Format(@"{0}\ExcelGranosPrimarias.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

                // Save this data as a file
                workSheet.SaveAs(fileName);
            }
            catch (Exception exception)
            {               
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

        public static void ExcelCreateLiqSecundarias(List<wslpg.LsgConsultaReturnType> listaLiquidaciones, List<wslpg.LpgAjusteConsReturnType> ListaAjuste, Dictionary<string, string> codigos, Dictionary<string, string> puertos, Dictionary<string, string> actividades)
        {
            // Load Excel application
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            // Create empty workbook
            excel.Workbooks.Add();

            int row = 2;

            // Create Worksheet from active sheet
            Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;
            workSheet.Name = "Secundarias";

            // I created Application and Worksheet objects before try/catch,
            // so that i can close them in finnaly block.
            // It's IMPORTANT to release these COM objects!!
            try
            {
                // ------------------------------------------------
                // Creation of header cells
                // ------------------------------------------------
                //Liquidacion Primaria
                workSheet.Cells[1, "A"] = "Fecha";
                workSheet.Cells[1, "B"] = "Liquidacion";
                workSheet.Cells[1, "C"] = "Actividad vendedor";
                workSheet.Cells[1, "D"] = "COE";
                workSheet.Cells[1, "E"] = "Comprador CUIT";
                workSheet.Cells[1, "F"] = "Vendedor CUIT";
                workSheet.Cells[1, "G"] = "¿Emitio corredor?";
                workSheet.Cells[1, "H"] = "Corredor CUIT";
                workSheet.Cells[1, "I"] = "Grano";
                workSheet.Cells[1, "J"] = "Puerto";
                workSheet.Cells[1, "K"] = "Cantidad (peso neto en Tn)";
                workSheet.Cells[1, "L"] = "Precio /U. peso";
                workSheet.Cells[1, "M"] = "Subtotal";
                workSheet.Cells[1, "N"] = "% Alicuota IVA";
                workSheet.Cells[1, "O"] = "Importe IVA";
                workSheet.Cells[1, "P"] = "Datos Adicionales";


                // ------------------------------------------------
                // Recorro las listas para poder pegar la informacion
                // ------------------------------------------------
                row = 2; // start row (in row 1 are header cells)
                foreach (wslpg.LsgConsultaReturnType liq in listaLiquidaciones)
                {
                    workSheet.Cells[row, "A"] = liq.liquidaciones[0].autorizacion.fechaLiquidacion;
                    workSheet.Cells[row, "B"] = "Secundaria";
                    workSheet.Cells[row, "C"] = (actividades.Count == 0) ? liq.liquidaciones[0].liquidacion.nroActVendedor.ToString() : string.Format(@"{0} - {1}", liq.liquidaciones[0].liquidacion.nroActVendedor, actividades[liq.liquidaciones[0].liquidacion.nroActVendedor.ToString()]);
                    workSheet.Cells[row, "D"] = liq.liquidaciones[0].autorizacion.coe;
                    workSheet.Cells[row, "E"] = liq.liquidaciones[0].liquidacion.cuitComprador;
                    workSheet.Cells[row, "F"] = liq.liquidaciones[0].liquidacion.cuitVendedor;
                    workSheet.Cells[row, "G"] = (liq.liquidaciones[0].liquidacion.liquidaCorredor == wslpg.LpgSiNoType.N) ? "NO" : "SI";
                    workSheet.Cells[row, "H"] = (liq.liquidaciones[0].liquidacion.liquidaCorredor == wslpg.LpgSiNoType.N) ? null : liq.liquidaciones[0].liquidacion.cuitCorredor.ToString();
                    workSheet.Cells[row, "I"] = (codigos.Count == 0) ? liq.liquidaciones[0].liquidacion.codGrano.ToString() : string.Format(@"{0} - {1}", liq.liquidaciones[0].liquidacion.codGrano, codigos[liq.liquidaciones[0].liquidacion.codGrano.ToString()]);
                    workSheet.Cells[row, "J"] = (puertos.Count == 0) ? liq.liquidaciones[0].liquidacion.codPuerto.ToString() : string.Format(@"{0} - {1}", liq.liquidaciones[0].liquidacion.codPuerto, puertos[liq.liquidaciones[0].liquidacion.codPuerto.ToString()]);
                    workSheet.Cells[row, "K"] = liq.liquidaciones[0].liquidacion.pesoNetoEnTn;
                    workSheet.Cells[row, "L"] = liq.liquidaciones[0].liquidacion.precioOperacionTn;
                    workSheet.Cells[row, "M"] = liq.liquidaciones[0].autorizacion.subtotal;
                    workSheet.Cells[row, "N"] = liq.liquidaciones[0].liquidacion.alicuotaIvaOperacion;
                    workSheet.Cells[row, "O"] = liq.liquidaciones[0].autorizacion.importeIva;
                    workSheet.Cells[row, "P"] = liq.liquidaciones[0].liquidacion.datosAdicionales;
                    row++;
                }

                //LLamo a segunda cracion de hoja ajustes
                HojaAjustes(excel, ListaAjuste);

                // Apply some predefined styles for data to look nicely :)
                workSheet.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);

                // Define filename
                string fileName = string.Format(@"{0}\ExcelGranosSecundarias.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

                // Save this data as a file
                workSheet.SaveAs(fileName);
            }
            catch (Exception exception)
            {
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

        public static void HojaAjustes(Microsoft.Office.Interop.Excel.Application excel, List<wslpg.LpgAjusteConsReturnType> ListaAjuste)
        {
            int row = 3;
            //---------------Hoja 2-------------------------

            excel.Sheets.Add();
            Microsoft.Office.Interop.Excel._Worksheet workSheet2 = excel.ActiveSheet;
            workSheet2.Name = "Ajustes";


            //Ajustes
            workSheet2.Cells[1, "F"] = "Ajuste Credito";
            workSheet2.Cells[1, "L"] = "Ajuste Debito";
            //Unicos
            workSheet2.Cells[2, "A"] = "COE Ajuste";
            workSheet2.Cells[2, "B"] = "COE Original";
            workSheet2.Cells[2, "C"] = "Tipo de operacion";
            workSheet2.Cells[2, "D"] = "Estado";
            workSheet2.Cells[2, "E"] = "Contrato";
            //ajuste credito
            workSheet2.Cells[2, "F"] = "Fecha liquidacion";
            workSheet2.Cells[2, "G"] = "Precio operacion";
            workSheet2.Cells[2, "H"] = "Subtotal";
            workSheet2.Cells[2, "I"] = "Importe IVA";
            workSheet2.Cells[2, "J"] = "Operacion con IVA";
            workSheet2.Cells[2, "K"] = "Total peso neto";
            //ajuste debito
            workSheet2.Cells[2, "L"] = "Fecha liquidacion";
            workSheet2.Cells[2, "M"] = "Precio operacion";
            workSheet2.Cells[2, "N"] = "Subtotal";
            workSheet2.Cells[2, "O"] = "Importe IVA";
            workSheet2.Cells[2, "P"] = "Operacion con IVA";
            workSheet2.Cells[2, "Q"] = "Total peso neto";
            //Totales unificados
            workSheet2.Cells[2, "R"] = "Sub total debito/credito";
            workSheet2.Cells[2, "S"] = "Sub total general";
            workSheet2.Cells[2, "T"] = "IVA 10.5";
            workSheet2.Cells[2, "U"] = "IVA 21";
            workSheet2.Cells[2, "v"] = "Importe neto";

            // ------------------------------------------------
            // Recorro las listas para poder pegar la informacion
            // ------------------------------------------------
            // start row (in row 1 are header cells)
            foreach (wslpg.LpgAjusteConsReturnType aju in ListaAjuste)
            {
                workSheet2.Cells[row, "A"] = aju.ajusteUnificado.coe;
                workSheet2.Cells[row, "B"] = aju.ajusteUnificado.coeAjustado;
                workSheet2.Cells[row, "C"] = aju.ajusteUnificado.codTipoOperacion;
                workSheet2.Cells[row, "D"] = aju.ajusteUnificado.estado;
                workSheet2.Cells[row, "E"] = aju.ajusteUnificado.nroContrato;
                //ajuste credito
                workSheet2.Cells[row, "F"] = aju.ajusteUnificado.ajusteCredito.fechaLiquidacion;
                workSheet2.Cells[row, "G"] = aju.ajusteUnificado.ajusteCredito.precioOperacion;
                workSheet2.Cells[row, "H"] = aju.ajusteUnificado.ajusteCredito.subTotal;
                workSheet2.Cells[row, "I"] = aju.ajusteUnificado.ajusteCredito.importeIva;
                workSheet2.Cells[row, "J"] = aju.ajusteUnificado.ajusteCredito.operacionConIva;
                workSheet2.Cells[row, "K"] = aju.ajusteUnificado.ajusteCredito.totalPesoNeto;
                //ajuste debito
                workSheet2.Cells[row, "L"] = aju.ajusteUnificado.ajusteDebito.fechaLiquidacion;
                workSheet2.Cells[row, "M"] = aju.ajusteUnificado.ajusteDebito.precioOperacion;
                workSheet2.Cells[row, "N"] = aju.ajusteUnificado.ajusteDebito.subTotal;
                workSheet2.Cells[row, "O"] = aju.ajusteUnificado.ajusteDebito.importeIva;
                workSheet2.Cells[row, "P"] = aju.ajusteUnificado.ajusteDebito.operacionConIva;
                workSheet2.Cells[row, "Q"] = aju.ajusteUnificado.ajusteDebito.totalPesoNeto;
                //totales unificados
                workSheet2.Cells[row, "R"] = aju.ajusteUnificado.totalesUnificados.subTotalDebCred;
                workSheet2.Cells[row, "S"] = aju.ajusteUnificado.totalesUnificados.subTotalGeneral;
                workSheet2.Cells[row, "T"] = aju.ajusteUnificado.totalesUnificados.iva105;
                workSheet2.Cells[row, "U"] = aju.ajusteUnificado.totalesUnificados.iva21;
                workSheet2.Cells[row, "V"] = aju.ajusteUnificado.totalesUnificados.importeNeto;
                //aumento un a fila mas                
                row++;
            }

            workSheet2.Range["F1:K1"].Merge(true);
            workSheet2.Range["L1:Q1"].Merge(true);
            workSheet2.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);
            workSheet2.Range["A2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);
        }
    }
}

