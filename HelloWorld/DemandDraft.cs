using System;
using System.Collections;
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
    class DemandDraft
    {
       
        static public void FuelDemandDraftEntry(object sender, EventArgs e, ArrayList textBoxes, DatePicker datepicker,string table)
        {

            DateTime dateTime = datepicker.SelectedDate.Value;
            string date = GlobalFunctions.epochTimeParam(dateTime);
            foreach (TextBox textBox in textBoxes)
            {
                if (textBox.Text.Length < 1||textBox.Text=="0")
                {
                    textBox.Background = Brushes.Red;
                    return;
                }
            }
            string Reference = (textBoxes[0] as TextBox).Text;
            double priceCalculated = 0;
            double totalPKR = double.Parse((textBoxes[1] as TextBox).Text);
            double totalLTR = double.Parse((textBoxes[2] as TextBox).Text);
            priceCalculated = totalPKR / totalLTR;
            priceCalculated = Math.Round(priceCalculated, 2, MidpointRounding.AwayFromZero);
            if (priceCalculated < 40)
            {
                (textBoxes[1] as TextBox).Background=Brushes.Red;
                (textBoxes[2] as TextBox).Background=Brushes.Red;
                return;
            }


            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "insert into " + table + " (Reference,TotalPKR,LTRUsable,TotalLTR,PerLTRPrice,date) values ('" + Reference + "','" + totalPKR + "','" + totalLTR + "','" + totalLTR + "','" + priceCalculated + "','" + date + "')";
            sqlite_cmd.ExecuteNonQuery();

            (textBoxes[0] as TextBox).Text = "";
            (textBoxes[1] as TextBox).Text = "";
            (textBoxes[2] as TextBox).Text = "";

            GlobalFunctions.CloseConnection();
        }


        static public void BoxesBackgroundClear(object sender, EventArgs e, ArrayList textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                
                    textBox.Background = Brushes.Transparent;
                
            }
        }
    }
}
