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

namespace HelloWorld
{
    class GlobalFunctions
    {

       static SQLiteConnection sqlite_conn;
        static public SQLiteConnection Connect()
        {
           
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source = database.db; Version = 3;");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was an error connecting to Database");
            }
            return sqlite_conn;

        }

        static public void CloseConnection()
        {
            sqlite_conn.Close();
        }



        static public string epochTime()
        {
            DateTime todayDateTime = DateTime.Now;
            string todayDateString = todayDateTime.ToString("MM/dd/yyyy");
            DateTime todayDate = Convert.ToDateTime(todayDateString);
            TimeSpan t = todayDate - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalSeconds;
            return secondsSinceEpoch.ToString();
        }

        static public string epochTimeParam(DateTime currentTime)
        {
            string todayDateString = currentTime.ToString("MM/dd/yyyy");
            DateTime todayDate = Convert.ToDateTime(todayDateString);
            TimeSpan t = todayDate - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalSeconds;
            return secondsSinceEpoch.ToString();
        }

        static public String epochToDateTime(long epochtime)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epochtime);
            DateTime tt = dateTimeOffset.Date;
            return tt.ToString("dd/MM/yyyy");
        }

        static public ArrayList dateTimeToMonthRange(DateTime dateTime)
        {
            int Month = dateTime.Month;
            int Year = dateTime.Year;
            int nextMonth = 1;
            if (Month < 12)
                nextMonth = Month + 1;
            string monthStartDate = Month + "/1/" + Year;
            string monthEndDate = nextMonth + "/1/" + Year;
            DateTime monthStart = Convert.ToDateTime(monthStartDate);
            DateTime monthEnd = Convert.ToDateTime(monthEndDate);
            string monthStartingEpoch = epochTimeParam(monthStart);
            string monthEndingEpoch = epochTimeParam(monthEnd);
            ArrayList monthStartandEndEpoch = new ArrayList();
            monthStartandEndEpoch.Add(monthStartingEpoch);
            monthStartandEndEpoch.Add(monthEndingEpoch);
            return monthStartandEndEpoch;
        }


        static public ArrayList dateTimeToYearRange(DateTime dateTime)
        {
            int Year = dateTime.Year;
            int nextYear = Year + 1;
            string yearStartDate = "1/1/" + Year;
            string yearEndDate = "1/1/" + nextYear;
            DateTime yearStart = Convert.ToDateTime(yearStartDate);
            DateTime yearEnd = Convert.ToDateTime(yearEndDate);
            string yearStartingEpoch = epochTimeParam(yearStart);
            string yearEndingEpoch = epochTimeParam(yearEnd);
            ArrayList yearStartandEndEpoch = new ArrayList();
            yearStartandEndEpoch.Add(yearStartingEpoch);
            yearStartandEndEpoch.Add(yearEndingEpoch);
            return yearStartandEndEpoch;
        }

        static public ArrayList comboBoxtoDateRangeList(ComboBox monthBox,ComboBox yearBox)
        {
            bool isMonthlyRange = false;
            ArrayList dateRange;
            string year = DateTime.Now.Year.ToString();
            string month = "1";
            string Date = "";

            if (yearBox.SelectedIndex > 0)
                year = "202" + yearBox.SelectedIndex;
            if (monthBox.SelectedIndex > 0)
            {
                month = monthBox.SelectedIndex.ToString();
                isMonthlyRange = true;
            }
            Date = month + "/01/" + year;
            DateTime dateTime = Convert.ToDateTime(Date);
            if (isMonthlyRange)
                dateRange = GlobalFunctions.dateTimeToMonthRange(dateTime);
            else
                dateRange = GlobalFunctions.dateTimeToYearRange(dateTime);
            return dateRange;
        }
    }
}
