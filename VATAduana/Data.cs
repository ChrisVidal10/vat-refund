using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;

namespace VATAduana
{
    class Data
    {
        private static SQLiteConnection con;

        public static void Conexion()
        {
            try
            {
                con = new SQLiteConnection("Data Source=authbd.db;Version=3;New=False;Compress=True");
                con.Open();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void Desconexion()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        public static bool VerificarTokenActivo(string cuit)
        {
            double diferenciaFechas = 0;

            try
            {
                string cadenaSQL = "SELECT GENERATIONTIME FROM AUTHPETICION WHERE SOURCE = '" + cuit + "' ORDER BY ID DESC LIMIT 1";
                SQLiteCommand command = new SQLiteCommand(cadenaSQL, con);
                SQLiteDataReader datar = command.ExecuteReader();
                datar.Read();
                if (!datar.HasRows)
                {
                    return false;
                }
                TimeSpan ts = DateTime.Now - datar.GetDateTime(0);
                diferenciaFechas = ts.TotalHours;
                command.Dispose();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (diferenciaFechas >= 12)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void insertPeticion(string source, int globalID, string generationTime, string expirationTime, string uniqueid, string token, string sign, string service)
        {
            try
            {
                string cadenaSQL = "insert into authpeticion(source, globalID, generationtime, expirationtime, service, uniqueid, token, sign) values('" + source + "','" + globalID + "','" + generationTime + "','" + expirationTime + "','" + service + "','" + uniqueid + "','" + token + "','" + sign + "')";
                Console.WriteLine(cadenaSQL);
                SQLiteCommand command = new SQLiteCommand(cadenaSQL, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static int selectGLobalId(string cuit)
        {
            int idpeticion = 0;

            try
            {
                string cadenaSQL = "SELECT count(*) FROM AUTHPETICION WHERE SOURCE = '" + cuit + "' ORDER BY ID DESC LIMIT 1";
                SQLiteCommand command = new SQLiteCommand(cadenaSQL, con);
                SQLiteDataReader datar = command.ExecuteReader();
                datar.Read();
                idpeticion = datar.GetInt32(0);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return idpeticion;
        }

        public static void insertRespuesta(string token, string sign)
        {
            int idpeticion = 0;

            try
            {
                string cadenaSQL = "SELECT ID FROM AUTHPETICION ORDER BY ID DESC LIMIT 1";
                SQLiteCommand command = new SQLiteCommand(cadenaSQL, con);
                SQLiteDataReader datar = command.ExecuteReader();
                datar.Read();
                idpeticion = datar.GetInt32(0);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                string cadenaSQL = "insert into authpeticion(token, sign, fkidpeticion) values(" + token + "," + sign + "," + idpeticion + ")";
                SQLiteCommand command = new SQLiteCommand(cadenaSQL, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string[] selectInfoTicket(string cuit)
        {
            string[] idpeticion = new string[2];

            try
            {
                string cadenaSQL = "SELECT TOKEN, SIGN FROM AUTHPETICION WHERE SOURCE = '" + cuit + "' ORDER BY GLOBALID DESC LIMIT 1";
                SQLiteCommand command = new SQLiteCommand(cadenaSQL, con);
                SQLiteDataReader datar = command.ExecuteReader();
                datar.Read();
                idpeticion[0] = datar.GetString(0);
                idpeticion[1] = datar.GetString(1);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }          
            return idpeticion;
        }
    }
}
