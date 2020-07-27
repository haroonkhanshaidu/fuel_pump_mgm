using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWorld
{
    class FuelTank
    {
        static public void petrolTankIn(double petrol, double price)
        {
            double petrolInQuantity = petrol;
            double newPrice = price;
            double previousPrice = 0;
            double totalFuel = 0;
            double previousFuel = 0;


            SqlCommand cmd;
            SqlDataReader reader;
            string query = "Select previousTotal, newTotal from petrolFuelTank";
            cmd = new SqlCommand(query, GlobalFunctions.Connect());
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                previousFuel = double.Parse(reader.GetValue(0).ToString());
                totalFuel = double.Parse(reader.GetValue(1).ToString());
                MessageBox.Show(reader.GetValue(0).ToString());
            }
            previousPrice = newPrice;
            previousFuel = previousFuel + totalFuel;
            totalFuel = totalFuel + petrolInQuantity;

            query = "insert into petrolFuelTank (petrolIn,petrolOut,previousTotal,newTotal,previousPrice,newPrice)  values('" + petrolInQuantity + "','0','" + previousFuel + "','" +totalFuel  + "','" + previousPrice + "','"+ newPrice + "')";
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = new SqlCommand(query, GlobalFunctions.Connect());
            adapter.InsertCommand.ExecuteNonQuery();


        }

    }
}
