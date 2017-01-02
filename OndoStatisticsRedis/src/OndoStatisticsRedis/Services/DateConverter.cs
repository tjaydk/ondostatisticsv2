using OndoStatisticsRedis.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Services
{
    public class DateConverter
    {
        /// <summary>
        /// Returns an int representing the current quarter
        /// </summary>
        /// <returns></returns>
        public static int getCurrentQuarter()
        {
            DateTime now = DateTime.Now;

            return (now.Month - 1) / 3 + 1;
        }

        /// <summary>
        /// Returns an int representing the current year
        /// </summary>
        /// <returns></returns>
        public static int getCurrentYear()
        {
            DateTime now = DateTime.Now;
            return now.Year;
        }

        /// <summary>
        /// Returns a DateTime representation of the current CET time +4 hours, this is not the correct danish time,
        /// but the 4 hours is to ensure that the Irish server displays the correct time
        /// </summary>
        /// <returns></returns>
        public static DateTime getTimeInDK()
        {

            DateTime dt = new DateTime(DateTime.UtcNow.AddHours(4).Ticks, DateTimeKind.Utc);
            TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            
            return TimeZoneInfo.ConvertTime(dt, timezone);
        }

        /// <summary>
        /// Returns the weeknumber from a DateTime.
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static int dateToWeek(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Converts a string in the format "yyyy-MM-dd" to a DateTime. 
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime dateStringToDateTime(string dateString)
        {
            string[] yyyyMMdd = dateString.Split('-');

            for (int i = 0; i < yyyyMMdd.Length; i++)
            {
                if (yyyyMMdd[i].Length < 2)
                {
                    string newStr = "0" + yyyyMMdd[i];
                    yyyyMMdd[i] = newStr;
                }
            }
            if (yyyyMMdd.Length != 3 || yyyyMMdd[0].Length != 4 || yyyyMMdd[1].Length != 2 || yyyyMMdd[2].Length != 2) throw new WrongDateFormatException("Date need to be in the format yyyy-MM-dd");
            
            return new DateTime(Int32.Parse(yyyyMMdd[0]), Int32.Parse(yyyyMMdd[1]), Int32.Parse(yyyyMMdd[2]));
        }


        /// <summary>
        /// Returns an int count of the days in the quarter to which the date given belongs
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int totalNumberOfDaysInQuarter(DateTime date)
        {
            DateTime FirstDateOfQuarter = new DateTime();
            DateTime LastDateOfQuarter = new DateTime();
            if (date.Month == 1 || date.Month == 2 || date.Month == 3)
            {
                FirstDateOfQuarter = new DateTime(date.Year, 1, 1);
                LastDateOfQuarter = new DateTime(date.Year, 3, 31);
            }
            else if (date.Month == 4 || date.Month == 5 || date.Month == 6)
            {
                FirstDateOfQuarter = new DateTime(date.Year, 4, 1);
                LastDateOfQuarter = new DateTime(date.Year, 6, 30);
            }
            else if (date.Month == 7 || date.Month == 8 || date.Month == 9)
            {
                FirstDateOfQuarter = new DateTime(date.Year, 7, 1);
                LastDateOfQuarter = new DateTime(date.Year, 9, 30);
            }
            else if (date.Month == 10 || date.Month == 11 || date.Month == 12)
            {
                FirstDateOfQuarter = new DateTime(date.Year, 10, 1);
                LastDateOfQuarter = new DateTime(date.Year, 12, 31);
            }

            return (LastDateOfQuarter - FirstDateOfQuarter).Days + 1;
        }

        /// <summary>
        /// Returns an int count of how many days have gone by in the quarter uptil a given date
        /// If first day of quarter then return 0
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int daysInQuarterSoFar(DateTime date)
        {
            DateTime firstDateOfQuarter = new DateTime();
            if (date.Month == 1 || date.Month == 2 || date.Month == 3)
            {
                firstDateOfQuarter = new DateTime(date.Year, 1, 1);
            }
            else if (date.Month == 4 || date.Month == 5 || date.Month == 6)
            {
                firstDateOfQuarter = new DateTime(date.Year, 4, 1);
            }
            else if (date.Month == 7 || date.Month == 8 || date.Month == 9)
            {
                firstDateOfQuarter = new DateTime(date.Year, 7, 1);
            }
            else if (date.Month == 10 || date.Month == 11 || date.Month == 12)
            {
                firstDateOfQuarter = new DateTime(date.Year, 10, 1);
            }

            return (date - firstDateOfQuarter).Days;
        }


        /// <summary>
        /// Returns the date of the first day in current quarter
        /// </summary>
        /// <returns></returns>
        public static DateTime firstDayInQuarter()
        {
            DateTime date = DateTime.Now;

            DateTime firstDateOfQuarter = new DateTime();
            if (date.Month == 1 || date.Month == 2 || date.Month == 3)
            {
                firstDateOfQuarter = new DateTime(date.Year, 1, 1);
            }
            else if (date.Month == 4 || date.Month == 5 || date.Month == 6)
            {
                firstDateOfQuarter = new DateTime(date.Year, 4, 1);
            }
            else if (date.Month == 7 || date.Month == 8 || date.Month == 9)
            {
                firstDateOfQuarter = new DateTime(date.Year, 7, 1);
            }
            else if (date.Month == 10 || date.Month == 11 || date.Month == 12)
            {
                firstDateOfQuarter = new DateTime(date.Year, 10, 1);
            }

            return firstDateOfQuarter;
        }
    }
}
