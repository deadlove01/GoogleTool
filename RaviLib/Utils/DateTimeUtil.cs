using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaviLib.Utils
{
    public class DateTimeUtil
    {
        private static readonly DateTime UnixEpoch =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long GetCurrentUnixTimestampMillis(DateTime time)
        {
            return (long)(time - UnixEpoch).TotalMilliseconds;
        }

        public static DateTime DateTimeFromUnixTimestampMillis(long millis)
        {
            return UnixEpoch.AddMilliseconds(millis);
        }

        public static long GetCurrentUnixTimestampSeconds(DateTime time)
        {
            return (long)(time - UnixEpoch).TotalSeconds;
        }

        public static DateTime DateTimeFromUnixTimestampSeconds(long seconds)
        {
            return UnixEpoch.AddSeconds(seconds);
        }

        public static DateTime GetCustomDate(DateTime time, int hours = 0, int minutes = 0, int seconds = 0)
        {
            return new DateTime(time.Year, time.Month, time.Day, hours, minutes, seconds);
        }



        public static DateTime GetFirstDateOfMonth(DateTime time, int hours = 0, int minutes = 0, int seconds = 0)
        {
            return new DateTime(time.Year, time.Month, 1, hours, minutes, seconds);
        }

        public static DateTime GetLastDateOfMonth(DateTime time, int hours = 23, int minutes = 59, int seconds = 59)
        {
            return new DateTime(time.Year, time.Month, DateTime.DaysInMonth(time.Year, time.Month), hours, minutes, seconds);
        }


        public static DateTime GetFirstDateOfWeek(DateTime time, int hours = 0, int minutes = 0, int seconds = 0)
        {
            int deltaDay = DayOfWeek.Monday - time.DayOfWeek;
            if (deltaDay > 0)
                deltaDay -= 7;
            DateTime date = time.AddDays(deltaDay);
            return new DateTime(date.Year, date.Month, date.Day, hours, minutes, seconds);
        }


        public static int NumberOfMonthsBetweenTwoDate(DateTime startDate, DateTime endDate)
        {
            return ((endDate.Year * 12) + endDate.Month) - ((startDate.Year * 12) + startDate.Month) + 1;
        }
    }
}
