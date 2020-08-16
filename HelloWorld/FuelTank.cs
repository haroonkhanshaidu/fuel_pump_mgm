using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWorld
{
    class FuelTank
    {
        public static double usedFuelTotalCost = 0;
        
        static public double FuelDeduction(double fuelLtr,string table)
        {
            double usedFuelPerLTRPrice = 0;
            double fuelToDeduct = fuelLtr;
            double fuelInTank = 0;
            double totalUsableFuel = 0;
            string fuelRef = "";


            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "SELECT LTRUsable FROM " + table + "  WHERE LTRUsable > 0;";

            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                totalUsableFuel = totalUsableFuel + reader.GetDouble(0);
                
            }
            if (fuelToDeduct > totalUsableFuel)
            {
                MessageBox.Show("There is not enough Fuel in Tank");
                return 0;
            }
            reader.Close();

            sqlite_cmd.CommandText = "SELECT Reference,LTRUsable, PerLTRPRice FROM "+table+"  WHERE LTRUsable > 0 limit 1;";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                    fuelRef = reader.GetValue(0).ToString();
                    fuelInTank = reader.GetDouble(1);
                    usedFuelPerLTRPrice = reader.GetDouble(2);
                    break;
             
            }

            reader.Close();

            if (fuelInTank <= 0)
            {
                MessageBox.Show("The Fuel tank is empty!");
                return 0;
            }
            if (fuelInTank >= fuelToDeduct)
            {
                fuelInTank = fuelInTank - fuelToDeduct;
                fuelTankUpdate(fuelRef, fuelInTank,table);
                usedFuelTotalCost = usedFuelTotalCost + (usedFuelPerLTRPrice * fuelToDeduct);
            }
            else
            {
                double difference = fuelInTank - fuelToDeduct;
                difference = difference * -1;
                usedFuelTotalCost = usedFuelTotalCost + (usedFuelPerLTRPrice * fuelInTank);
                fuelInTank = 0;
                fuelTankUpdate(fuelRef, fuelInTank,table);
                FuelDeduction(difference,table);
            }

            GlobalFunctions.CloseConnection();
            reader.Close();
       
            return usedFuelTotalCost;


        }

        static private void fuelTankUpdate(string fuelRef,double fuelInTank,string table)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "Update " + table + " set LTRUsable = '" + fuelInTank + "' where Reference = '" + fuelRef + "'";
            sqlite_cmd.ExecuteNonQuery();
            GlobalFunctions.CloseConnection();



        }


       

    }
}
