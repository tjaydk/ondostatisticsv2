using System;

namespace DatabaseSpeedTester.Services.Original
{
    class DateTimeConverter
    {
        /// <summary>
        /// Returns an int representing the current quarter
        /// </summary>
        /// <returns></returns>
        public static int getCurrentQuarter(DateTime date)
        {
            return (date.Month - 1) / 3 + 1;
        }

        /// <summary>
        /// Returns an int count of the days in the quarter to which the date given belongs
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int totalNumberOfDaysInQuarter(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            DateTime FirstDateOfQuarter = new DateTime();
            DateTime LastDateOfQuarter = new DateTime();
            if (month == 1 || month == 2 || month == 3)
            {
                FirstDateOfQuarter = new DateTime(year, 1, 1);
                LastDateOfQuarter = new DateTime(year, 3, 31);
            }
            else if (month == 4 || month == 5 || month == 6)
            {
                FirstDateOfQuarter = new DateTime(year, 4, 1);
                LastDateOfQuarter = new DateTime(year, 6, 30);
            }
            else if (month == 7 || month == 8 || month == 9)
            {
                FirstDateOfQuarter = new DateTime(year, 7, 1);
                LastDateOfQuarter = new DateTime(year, 9, 30);
            }
            else if (month == 10 || month == 11 || month == 12)
            {
                FirstDateOfQuarter = new DateTime(year, 10, 1);
                LastDateOfQuarter = new DateTime(year, 12, 31);
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
            int year = date.Year;
            int month = date.Month;
            DateTime firstDateOfQuarter = new DateTime();
            if (month == 1 || month == 2 || month == 3)
            {
                firstDateOfQuarter = new DateTime(year, 1, 1);
            }
            else if (month == 4 || month == 5 || month == 6)
            {
                firstDateOfQuarter = new DateTime(year, 4, 1);
            }
            else if (month == 7 || month == 8 || month == 9)
            {
                firstDateOfQuarter = new DateTime(year, 7, 1);
            }
            else if (month == 10 || month == 11 || month == 12)
            {
                firstDateOfQuarter = new DateTime(year, 10, 1);
            }

            return (date - firstDateOfQuarter).Days;
        }

        /// <summary>
        /// returns an int count how many days are left in a given quarter
        /// If last day of quarter then return 0
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int daysLeftInQuarter(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            DateTime lastDateOfQuarter = new DateTime();
            if (month == 1 || month == 2 || month == 3)
            {
                lastDateOfQuarter = new DateTime(year, 3, 31);
            }
            else if (month == 4 || month == 5 || month == 6)
            {
                lastDateOfQuarter = new DateTime(year, 6, 30);
            }
            else if (month == 7 || month == 8 || month == 9)
            {
                lastDateOfQuarter = new DateTime(year, 9, 30);
            }
            else if (month == 10 || month == 11 || month == 12)
            {
                lastDateOfQuarter = new DateTime(year, 12, 31);
            }

            return (lastDateOfQuarter - date).Days +1;
        }
    }
}
