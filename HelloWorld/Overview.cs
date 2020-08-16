using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HelloWorld
{
    class Overview
    {
        static public void SalesOverview(ArrayList salesLabel, ArrayList selectedDateRange)
        {
            string startRange = selectedDateRange[0].ToString();
            string endRange = selectedDateRange[1].ToString();
            Label LtrPetrol = (salesLabel[0] as Label);
            Label PkrPetrol = (salesLabel[1] as Label);
            Label LtrDiesel = (salesLabel[2] as Label);
            Label PkrDiesel = (salesLabel[3] as Label);
            double totalLtrPetrol = 0;
            double totalPkrPetrol = 0;
            double totalLtrDiesel = 0;
            double totalPkrDiesel = 0;

            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "Select totalPKR, totalLTR from petrol where date >"+startRange+" and date < "+endRange+"; ";

            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                totalLtrPetrol = totalLtrPetrol + reader.GetDouble(0);
                totalPkrPetrol = totalPkrPetrol + reader.GetDouble(1);
            }
            reader.Close();

            sqlite_cmd.CommandText = "Select totalLTR, totalPKR from diesel where date >" + startRange + " and date < " + endRange + "; ";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                totalLtrDiesel = totalLtrDiesel + reader.GetDouble(0);
                totalPkrDiesel = totalPkrDiesel + reader.GetDouble(1);
            }
            reader.Close();
            GlobalFunctions.CloseConnection();

            LtrPetrol.Content = totalLtrPetrol.ToString();
            PkrPetrol.Content = totalPkrPetrol.ToString();
            LtrDiesel.Content = totalLtrDiesel.ToString();
            PkrDiesel.Content = totalPkrDiesel.ToString();

        }


        static public void FuelTank(ArrayList salesLabel)
        {
            Label fuelTankPetrol = (salesLabel[0] as Label);
            Label fuelTankDiesel = (salesLabel[1] as Label);
            double totalPetrolinTank = 0;
            double totalDIeselinTank = 0;

            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "Select LTRUsable from ddPetrol WHERE LTRUsable > 0; ";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                totalPetrolinTank = totalPetrolinTank + (reader.GetDouble(0));
            }
            fuelTankPetrol.Content = totalPetrolinTank.ToString();
            reader.Close();
            sqlite_cmd.CommandText = "Select LTRUsable from ddDiesel WHERE LTRUsable > 0;";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                totalDIeselinTank = totalDIeselinTank + reader.GetDouble(0);
            }
            fuelTankDiesel.Content = totalDIeselinTank.ToString();
            reader.Close();
            GlobalFunctions.CloseConnection();


        }


        static public void TotalProfit(ArrayList profitLabels, ArrayList dateRange)
        {

            string startRange = dateRange[0].ToString();
            string endRange = dateRange[1].ToString(); 
            Label totalProfitPetrolLabel = (profitLabels[0] as Label);
            Label totalProfitDieselLabel = (profitLabels[1] as Label);
            Label totalExpensesLabel = (profitLabels[2] as Label);
            Label netProfitLabel =    (profitLabels[3] as Label);
            double PetrolProfit = 0;
            double DieselProfit = 0;
            double netProfit = 0;
            double totalExpenses = 0;
            double grossProfit = 0;
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "SELECT totalPKR, totalcost, discount from petrol where date > "+startRange+" and date < "+endRange+"; ";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                PetrolProfit = PetrolProfit + (reader.GetDouble(0) - reader.GetDouble(1)-reader.GetDouble(2));
            }
            reader.Close();
            sqlite_cmd.CommandText = "SELECT totalPKR, totalcost, discount from diesel where date > " + startRange + " and date < " + endRange + "; ";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                DieselProfit = DieselProfit + (reader.GetDouble(0) - reader.GetDouble(1)-reader.GetDouble(2));
            }
            reader.Close();

            sqlite_cmd.CommandText = "SELECT total FROM expenses where date > " + startRange + " and date < " + endRange + "; ";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                totalExpenses = totalExpenses + reader.GetDouble(0);
            }
            reader.Close();

            grossProfit = PetrolProfit + DieselProfit;
            netProfit = grossProfit - totalExpenses;
            totalProfitPetrolLabel.Content = PetrolProfit;
            totalProfitDieselLabel.Content = DieselProfit;
            totalExpensesLabel.Content = totalExpenses;
            netProfitLabel.Content = netProfit;

            GlobalFunctions.CloseConnection();



        }

    }
}
