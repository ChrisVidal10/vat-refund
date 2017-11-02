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
                    workSheet.Cells[1, "N"] = "Precio /kg";
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
                
                //---------------Hoja 2-------------------------

                excel.Sheets.Add();
                Microsoft.Office.Interop.Excel._Worksheet workSheet2 = excel.ActiveSheet;
                workSheet2.Name = "Ajustes";

                //Ajuste
                workSheet2.Cells[1, "A"] = "COE Ajuste";
                workSheet2.Cells[1, "B"] = "COE Original";
                workSheet2.Cells[1, "C"] = "Tipo de operacion";
                workSheet2.Cells[1, "D"] = "Estado";


                // ------------------------------------------------
                // Recorro las listas para poder pegar la informacion
                // ------------------------------------------------
                row = 2; // start row (in row 1 are header cells)
                foreach (wslpg.LpgAjusteConsReturnType aju in ListaAjuste)
                {
                    workSheet2.Cells[row, "A"] = aju.ajusteUnificado.coe;
                    workSheet2.Cells[row, "B"] = aju.ajusteUnificado.coeAjustado;
                    workSheet2.Cells[row, "C"] = aju.ajusteUnificado.codTipoOperacion;
                    workSheet2.Cells[row, "D"] = aju.ajusteUnificado.estado;

                    row++;
                }

                // Apply some predefined styles for data to look nicely :)

                workSheet.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);

                workSheet2.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);

                
                // Define filename
                string fileName = string.Format(@"{0}\ExcelGranos.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

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
            
        }
    }
}

