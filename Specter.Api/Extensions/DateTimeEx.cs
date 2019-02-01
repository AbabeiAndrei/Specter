using System;
using System.Text.RegularExpressions;

namespace Specter.Api.Extensions 
{
    public static class DateTimeEx
    {
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return date.StartOfWeek(startOfWeek).AddDays(6);
        }
    }
}