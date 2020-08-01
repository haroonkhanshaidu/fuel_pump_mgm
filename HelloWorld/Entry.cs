using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace HelloWorld
{
    class Entry : MainWindow
    {
       public Entry(double total)
        {

        }

        static public int InsertEntry(object sender, EventArgs e, string table, Dictionary<string, double> dict , DatePicker datePicker)
        {
            DateTime dateTime = datePicker.SelectedDate.Value;
            string date = GlobalFunctions.epochTimeParam(dateTime);
            if (dateFound(date,table))
            {
                return -1;
            }


            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "insert into " + table + " (opening1,closing1,opening2,closing2,rate,testing,discount,totalPKR,totalLTR,date) " +
                "values ('" + dict["n1opening"] + "','" + dict["n1closing"] + "','" + dict["n2opening"] + "','" + dict["n2closing"] + "'," +
                "'" + dict["rate"] + "','" + dict["testing"] + "','" + dict["discount"] + "','" + dict["totalPkrs"] + "','" + dict["totalLtrs"] + "','" + date + "')";

            

           
            return sqlite_cmd.ExecuteNonQuery();

        }

        static public string getLastEntry(string table,string column)
        {
            
            string date="28378687445";


            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "SELECT " + column + " FROM " + table + " ORDER BY id DESC limit 1";

            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {

                date = reader.GetValue(0).ToString();
            }

            return date;
        }

        static public Boolean dateFound(string date,string table)
        {


            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "select date from " + table + " where date =" + date;

            reader = sqlite_cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            return false;

            
        }

    }
        
}
