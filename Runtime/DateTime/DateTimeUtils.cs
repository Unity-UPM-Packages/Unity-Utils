using System;
using UnityEngine;

namespace TheLegends.Unity.Utils
{
    public static class DateTimeUtils
    {
        /// <summary>
        /// Calculates the number of days between two DateTime values.
        /// </summary>
        public static int CalculateDaysDifference(DateTime fromDate, DateTime toDate)
        {
            return (toDate.Date - fromDate.Date).Days;
        }

        /// <summary>
        /// Calculates the number of hours between two DateTime values.
        /// </summary>
        public static double CalculateHoursDifference(DateTime fromDate, DateTime toDate)
        {
            return (toDate - fromDate).TotalHours;
        }

        /// <summary>
        /// Checks whether N days have passed.
        /// </summary>
        public static bool HasPassedDays(DateTime fromDate, DateTime currentDate, int daysThreshold)
        {
            return CalculateDaysDifference(fromDate, currentDate) >= daysThreshold;
        }

        /// <summary>
        /// Checks whether two dates are on the same day.
        /// </summary>
        public static bool IsSameDay(DateTime date1, DateTime date2)
        {
            return date1.Date == date2.Date;
        }

        /// <summary>
        /// Gets a DateTime value from PlayerPrefs (returns null if not found).
        /// </summary>
        public static DateTime? GetDateFromPrefs(string key)
        {
            string dateStr = PlayerPrefs.GetString(key, "");
            if (string.IsNullOrEmpty(dateStr))
                return null;

            if (DateTime.TryParse(dateStr, out DateTime date))
                return date;

            return null;
        }

        /// <summary>
        /// Saves a DateTime value to PlayerPrefs.
        /// </summary>
        public static void SaveDateToPrefs(string key, DateTime date)
        {
            PlayerPrefs.SetString(key, date.ToString("o")); // ISO 8601 format
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Calculates the number of days from the PlayerPrefs date to the current date.
        /// </summary>
        public static int? CalculateDaysSincePrefsDate(string prefKey, DateTime? currentDate = null)
        {
            DateTime? savedDate = GetDateFromPrefs(prefKey);
            if (!savedDate.HasValue)
                return null;

            DateTime now = currentDate ?? DateTime.UtcNow;
            return CalculateDaysDifference(savedDate.Value, now);
        }
    }
}
