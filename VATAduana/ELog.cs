using System;
using System.Collections.Generic;
using System.IO;

namespace VATAduana
{
    public class ELog
    {
        public static void createTxtErrores(List<List<string>> listaErrores, string source)
        {
            if (listaErrores.Count != 0)
            {
                string fecha = System.DateTime.Now.ToString("yyyyMMdd");
                string hora = System.DateTime.Now.ToString("HH:mm:ss");


                string fileName = (source == "granos") ? string.Format(@"{0}\LogGranos.txt", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)) : string.Format(@"{0}\LogAduana.txt", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

                // Chequeo si el archivo existe, si es asi, lo borro para que no se agregue nada a lo ya escrito.
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                StreamWriter sw = new StreamWriter(fileName, true);

                switch (source)
                {
                    case "granos":
                        foreach (List<string> error in listaErrores)
                        {
                            sw.WriteLine(string.Format(@"{0} - {1} => COE:{2} - {3}", fecha, hora, error[0], error[1]));
                            sw.Flush();
                        }

                        sw.WriteLine("");

                        foreach (List<string> error in listaErrores)
                        {
                            sw.WriteLine(error[0]);
                            sw.Flush();
                        }
                        break;

                    case "aduana":
                        foreach (List<string> error in listaErrores)
                        {
                            sw.WriteLine(string.Format(@"{0} - {1} => ID Destinación:{2} - {3}", fecha, hora, error[0], error[1]));
                            sw.Flush();
                        }
                        break;
                }

                sw.Flush();
                sw.Close();
            }
        }
    }
}
