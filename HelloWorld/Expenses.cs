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
using System.Windows.Navigation;

namespace HelloWorld
{
    class Expenses
    {
       

        static public void calculate(object sender, EventArgs e, ArrayList list)
        {
            double totalExpense = 0;
            int count = 0;
            (list[list.Count - 1] as TextBox).Text = totalExpense.ToString();
            foreach (TextBox textBox in list)
            {
                textBox.Background = Brushes.Transparent;
                count++;
                if (count == list.Count)
                    break;
                if (textBox.Text.Length > 0)
                {
                    totalExpense = totalExpense + double.Parse(textBox.Text);
                    (list[list.Count-1] as TextBox).Text = totalExpense.ToString();
                }
                
            }
        }



        static public void saveExpenseData(object sender, EventArgs e, ArrayList list,DatePicker datepicker)
        {

            DateTime dateTime = datepicker.SelectedDate.Value;
            string date = GlobalFunctions.epochTimeParam(dateTime);

            if ((list[list.Count - 1] as TextBox).Text.Length < 1 || (list[list.Count - 1] as TextBox).Text == "0")
            {
                foreach (TextBox textbox in list)
                {
                    textbox.Background = Brushes.Red;
                }
                return;
            }

            string query;
            string textboxesValues = "";
            foreach (TextBox textBox in list)
            {
                double value = 0;
                if (textBox.Text.Length > 0)
                {
                    value = double.Parse(textBox.Text);
                }
                textboxesValues = textboxesValues + "'" + value + "',";
            }


            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "insert into expenses (salareis,electricity,maintenance,other,mianSahib,total,date) values (" + textboxesValues + "" + date + ")";
            sqlite_cmd.ExecuteNonQuery();
            GlobalFunctions.CloseConnection();


            TextBox withdrawBox = (list[list.Count - 2] as TextBox);
            if (withdrawBox.Text.Length > 0)
            {
                ownerDepositWithdrawCalculator(withdrawBox, "withdraw",date);
            }

            foreach (TextBox textBox in list)
            {
                 textBox.Text="";
            }
        }


        static public void ownerDeposit(object sender, EventArgs e, TextBox depositbox, DatePicker datepicker)
        {
            DateTime dateTime = datepicker.SelectedDate.Value;
            string date = GlobalFunctions.epochTimeParam(dateTime);
            if (depositbox.Text.Length > 0)
            {
                ownerDepositWithdrawCalculator(depositbox, "deposit",date);
                depositbox.Text = "";
            }
            else
                depositbox.Background = Brushes.Red;
        }

        static private void ownerDepositWithdrawCalculator(TextBox textBox, string status, string date)
        {

            double total = 0;
            double deposit = 0;
            double withdraw = 0;


            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "Select (total) from ownerAmount";

            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                total = double.Parse(reader.GetValue(0).ToString());
            }


            if (status == "deposit")
                deposit = double.Parse(textBox.Text);
            else
                withdraw = double.Parse(textBox.Text);

            total = (total + deposit) - withdraw;

            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "insert into ownerAmount (depost,withdrawal,total,date) values ('" + deposit + "','" + withdraw + "','" + total + "','" + date + "')";
            sqlite_cmd.ExecuteNonQuery();
            GlobalFunctions.CloseConnection();


        }

        static public void depositBoxClear(object sender, EventArgs e)
        {
            (sender as TextBox).Background = Brushes.Transparent;
        }

    }
}
