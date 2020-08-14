using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloWorld
{
    class Overview
    {
        static public void Sales()
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "Select totalPKR, totalLTR from petrol ORDER BY id DESC LIMIT 30; ";

            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                MessageBox.Show(reader.GetString(0));
                MessageBox.Show(reader.GetString(1));
            }
            reader.Close();
        }
    }
}
