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
        static public Boolean FuelDeduction(double fuelLtr,string table)
        {
            double fuelToDeduct = fuelLtr;
            double fuelInTank = 0;
            string fuelRef = "";



            SqlCommand cmd;
            SqlDataReader reader;
            string query = "Select Reference, LTRUsable from "+ table +"";
            cmd = new SqlCommand(query, GlobalFunctions.Connect());
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                double usableFuel = double.Parse(reader.GetValue(1).ToString());
                if(usableFuel>0)
                {
                    fuelRef = reader.GetValue(0).ToString();
                    fuelInTank = double.Parse(reader.GetValue(1).ToString());
                    break;
                } 
            }
            if (fuelInTank < fuelToDeduct)
            {
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

            return true;


        }

        static private void fuelTankUpdate(string fuelRef,double fuelInTank,string table)
        {

            SqlDataAdapter adapter = new SqlDataAdapter();
            string query = "Update "+ table + " set LTRUsable = '" + fuelInTank + "' where Reference = '" + fuelRef + "'";
            adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand(query, GlobalFunctions.Connect());
            adapter.UpdateCommand.ExecuteNonQuery();
        }


       

    }
}
