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
        static public ArrayList SalesOverview(ArrayList salesLabel)
        {
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
            sqlite_cmd.CommandText = "Select totalPKR, totalLTR from petrol ORDER BY id DESC LIMIT 30; ";

            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                totalLtrPetrol = totalLtrPetrol + reader.GetDouble(0);
                totalPkrPetrol = totalPkrPetrol + reader.GetDouble(1);
            }
            reader.Close();

            sqlite_cmd.CommandText = "Select totalLTR, totalPKR from diesel ORDER BY id DESC LIMIT 30; ";
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
            ArrayList totalLTRofPetrolAndDieselList = new ArrayList();
            totalLTRofPetrolAndDieselList.Add(totalLtrPetrol);
            totalLTRofPetrolAndDieselList.Add(totalLtrDiesel);
            return totalLTRofPetrolAndDieselList;

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


        static public void TotalProfit(ArrayList salesLabel)
        {
            Label totalProfitPetrol = (salesLabel[0] as Label);
            Label totalProfitDiesel = (salesLabel[1] as Label);

            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "Select date from creditorData; ";

            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                if (1>2) ;
            }
            reader.Close();

            sqlite_cmd.CommandText = "Select totalLTR, totalPKR from diesel ORDER BY id DESC LIMIT 30; ";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
            }
            reader.Close();
            GlobalFunctions.CloseConnection();



        }

    }
}
