using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWorld
{
    class GlobalFunctions
    {
        static public long epochTime()
        {
            TimeSpan t = DateTime.Today.AddDays(0) - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalSeconds;
            return secondsSinceEpoch;
        }

        static public long epochTimeParam(DateTime currentTime)
        {
            TimeSpan t = currentTime - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalSeconds;
            return secondsSinceEpoch;
        }

        static public String epochToDateTime(long epochtime)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epochTime());
            DateTime tt = dateTimeOffset.Date;
            return tt.ToString("dd/MM/yyyy");
        }
    }
}
