using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.Windows.Controls;
using System.Data.SqlClient;

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
           
            string query = "insert into "+table+" (opening1,closing1,opening2,closing2,rate,testing,discount,totalPKR,totalLTR,date) " +
                "values ('" + dict["n1opening"] + "','" + dict["n1closing"] + "','" + dict["n2opening"] + "','" + dict["n2closing"] + "'," +
                "'" + dict["rate"] + "','" + dict["testing"] + "','" + dict["discount"] + "','" + dict["totalPkrs"] + "','" + dict["totalLtrs"] + "','" + date + "')";
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = new SqlCommand(query, GlobalFunctions.Connect());

            return adapter.InsertCommand.ExecuteNonQuery();
            
        }

        static public string getLastEntry(string table,string column)
        {
            
            SqlCommand cmd;
            SqlDataReader reader;
            string date="";

            string query = "SELECT TOP 1 "+ column +" FROM "+ table+" ORDER BY ID DESC";
            cmd = new SqlCommand(query, GlobalFunctions.Connect());
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                date = reader.GetValue(0).ToString();
            }
            return date;
        }

        static public Boolean dateFound(DatePicker datePicker, string table)
        {

            DateTime dateTime = datePicker.SelectedDate.Value;
            string date = GlobalFunctions.epochTimeParam(dateTime);
            

            String searchQuery = "select date from " + table + " where date =" + date;
            SqlCommand cmd;
            SqlDataReader reader;


            cmd = new SqlCommand(searchQuery, GlobalFunctions.Connect());
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            return false;
        }

    }
        
}
