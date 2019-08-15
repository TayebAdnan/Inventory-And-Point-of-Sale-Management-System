using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public static class WeekMonthReportController 
    {
        // GET: WeekMonthReport
        // Extension method to return the first  day of the week (Sunday).
        public static DateTime FirstOfWeek(this DateTime date)
        {
            return date.AddDays(DayOfWeek.Sunday - date.DayOfWeek);
        }
        // Extension method to return the first working day of the week (Monday).
        public static DateTime FirstOfWorkingWeek(this DateTime date)
        {
            return date.AddDays(DayOfWeek.Monday - date.DayOfWeek);
        }
    }
}