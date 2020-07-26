﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWorld
{
    class GlobalFunctions
    {
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
    }
}