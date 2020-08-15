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
        static public void Sales(ArrayList salesLabel)
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
                totalLtrPetrol = totalLtrPetrol + double.Parse(reader.GetString(0));
                totalPkrPetrol = totalPkrPetrol + double.Parse(reader.GetString(1));
            }
            reader.Close();

            sqlite_cmd.CommandText = "Select totalLTR, totalPKR from diesel ORDER BY id DESC LIMIT 30; ";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                totalLtrDiesel = totalLtrDiesel + double.Parse(reader.GetString(0));
                totalPkrDiesel = totalPkrDiesel + double.Parse(reader.GetString(1));
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

            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            sqlite_cmd.CommandText = "Select LTRUsable from ddPetrol ORDER BY id DESC LIMIT 1; ";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                fuelTankPetrol.Content = reader.GetString(0);
            }
            reader.Close();
            sqlite_cmd.CommandText = "Select LTRUsable from ddDiesel ORDER BY id DESC LIMIT 1;";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                fuelTankDiesel.Content = reader.GetString(0);
            }
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
                if (int.Parse(reader.GetString(0)) > 1599000000) ;
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
