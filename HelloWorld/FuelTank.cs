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
        static public Boolean FuelDeduction(double fuelLtr,string table)
        {
            double fuelToDeduct = fuelLtr;
            double fuelInTank = 0;
            string fuelRef = "";


            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "Select Reference, LTRUsable from " + table + "";

            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                double usableFuel = reader.GetDouble(1);
                if (usableFuel > 0)
                {
                    fuelRef = reader.GetValue(0).ToString();
                    fuelInTank = reader.GetDouble(1);
                    break;
                }
            }

            reader.Close();

            if (fuelInTank <= 0)
            {
                MessageBox.Show("Not enough fuel in Tank");
                return false;
            }
            if (fuelInTank >= fuelToDeduct)
            {
                fuelInTank = fuelInTank - fuelToDeduct;
                fuelTankUpdate(fuelRef, fuelInTank,table);
            }
            else
            {
                double difference = fuelInTank - fuelToDeduct;
                difference = difference * -1;
                fuelInTank = 0;
                fuelTankUpdate(fuelRef, fuelInTank,table);
                FuelDeduction(difference,table);
            }

            GlobalFunctions.CloseConnection();
            return true;


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
