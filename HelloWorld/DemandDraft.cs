using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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
       
        static public void petrolEntry(object sender, EventArgs e, ArrayList textBoxes, DatePicker datepicker)
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
            string PetrolRef = (textBoxes[0] as TextBox).Text;
            double petrolPriceCalculated = 0;
            double petrolPKR = double.Parse((textBoxes[1] as TextBox).Text);
            double petrolLTR = double.Parse((textBoxes[2] as TextBox).Text);
            petrolPriceCalculated = petrolPKR / petrolLTR;
            petrolPriceCalculated = Math.Round(petrolPriceCalculated, 2, MidpointRounding.AwayFromZero);
            if (petrolPriceCalculated < 40)
            {
                (textBoxes[1] as TextBox).Background=Brushes.Red;
                (textBoxes[2] as TextBox).Background=Brushes.Red;
                return;
            }

            string query = "insert into ddPetrol (petrolRef,petrolPKR,petrolLTR,petrolPrice,date) values ('" + PetrolRef + "','" + petrolPKR + "','" + petrolLTR + "','" + petrolPriceCalculated + "','" + date + "')";
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = new SqlCommand(query, GlobalFunctions.Connect());
            adapter.InsertCommand.ExecuteNonQuery();
            (textBoxes[0] as TextBox).Text = "";
            (textBoxes[1] as TextBox).Text = "";
            (textBoxes[2] as TextBox).Text = "";
        }


        static public void dieselEntry(object sender, EventArgs e, ArrayList textBoxes, DatePicker datepicker)
        {
            DateTime dateTime = datepicker.SelectedDate.Value;
            string date = GlobalFunctions.epochTimeParam(dateTime);
            foreach (TextBox textBox in textBoxes)
            {
                if (textBox.Text.Length < 1 || textBox.Text == "0")
                {
                    textBox.Background = Brushes.Red;
                    return;
                }
            }

            double dieselPriceCalculated = 0;
            double dieselPKR = double.Parse((textBoxes[0] as TextBox).Text);
            double dieselLTR = double.Parse((textBoxes[1] as TextBox).Text);
            dieselPriceCalculated = dieselPKR / dieselLTR;
            dieselPriceCalculated = Math.Round(dieselPriceCalculated, 2, MidpointRounding.AwayFromZero);
            if (dieselPriceCalculated < 10)
            {
                (textBoxes[0] as TextBox).Background = Brushes.Red;
                (textBoxes[1] as TextBox).Background = Brushes.Red;
                return;
            }

            string query = "insert into ddDiesel (dieselPKR,dieselLTR,dieselPrice,date) values ('" + dieselPKR + "','" + dieselLTR + "','" + dieselPriceCalculated + "','" + date + "')";
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = new SqlCommand(query, GlobalFunctions.Connect());
            adapter.InsertCommand.ExecuteNonQuery();
            (textBoxes[0] as TextBox).Text = "";
            (textBoxes[1] as TextBox).Text = "";
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
