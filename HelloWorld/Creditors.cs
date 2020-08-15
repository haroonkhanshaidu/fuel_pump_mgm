using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HelloWorld
{
    class Creditors
    {
       

        static public void creditorinsert(string table, string name, string amount, DatePicker datepicker)
        {
            DateTime dateTime = datepicker.SelectedDate.Value;
            string date = GlobalFunctions.epochTimeParam(dateTime);
            SqlDataAdapter adapter;
            string query;

            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "insert into " + table + " (creditorName,amount,date) values ('" + name + "','" + amount + "','" + date + "')";
            sqlite_cmd.ExecuteNonQuery();
            GlobalFunctions.CloseConnection ();
        }

     
        static public Dictionary<string,string> creditorGetData(string table)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
           
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM "+table;

            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {

                d.Add(reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
            }

            GlobalFunctions.CloseConnection();


            return d;

        }

     

        static public void creditorUpdate(string table, string name,string amount,DatePicker datepicker)
        {

            DateTime dateTime = datepicker.SelectedDate.Value;
            string date = GlobalFunctions.epochTimeParam(dateTime);


            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText= "Update " + table + " set amount = '" + amount + "', date = '" + date + "' where creditorName = '" + name + "'";
            sqlite_cmd.ExecuteNonQuery();
            GlobalFunctions.CloseConnection();

        }


        static public void clearAmountBox(object sender, EventArgs e)
        {

            (sender as TextBox).Background = Brushes.Transparent;

        }

       
    }
}
