using System.Linq;
using System.Web.Mvc;
using System;
using System.Web;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using Calendar = cahoot.Models.Calendar;

namespace cahoot.Helpers
{
    public static class CalendarHelper
    {
        public static IHtmlString Calendar(this HtmlHelper helper, List<Calendar> calendarEntires)
        {
            return Calendar(helper, calendarEntires, DateTime.Now);
        }

        public static IHtmlString Calendar(this HtmlHelper helper, List<Calendar> calendarEntires, DateTime dateToShow)
        {
            DateTimeFormatInfo cinfo = DateTimeFormatInfo.CurrentInfo;
            StringBuilder sb = new StringBuilder();
            DateTime date = new DateTime(dateToShow.Year, dateToShow.Month, 1);
            int emptyCells = ((int)date.DayOfWeek + 7 - (int)cinfo.FirstDayOfWeek) % 7;
            int days = DateTime.DaysInMonth(dateToShow.Year, dateToShow.Month);
            var monthIndex = date.Month - DateTime.Now.Month;
            sb.Append("<table class='cal'><tr><th>må</th><th>ti</th><th>on</th><th>to</th><th>fr</th><th>lö</th><th>sö</th></tr>");
            for (int i = 0; i < ((days + emptyCells) > 35 ? 42 : 35); i++)
            {
                if (i % 7 == 0)
                {
                    if (i > 0) sb.Append("</tr>");
                    sb.Append("<tr>");
                }

                if (i < emptyCells || i >= emptyCells + days)
                {
                    sb.Append("<td class='cal-empty'>&nbsp;</td>");
                }
                else
                {
                    bool today = date.Date.ToShortDateString().Equals(DateTime.Now.ToShortDateString());
                    if (calendarEntires.Where(e => e.EventDate.ToShortDateString() == date.ToShortDateString()).Count() > 0)
                    {
                        sb.Append(string.Format("<td class='cal-day{0}'><a href=\"{2}\">{1}</a></td>",
                            today ? " today" : string.Empty, date.Day, "?eventdate=" + date.ToShortDateString() + "&monthindex=" + monthIndex));
                    }
                    else
                    {
                        sb.Append(string.Format("<td class='cal-day{0}'>{1}</td>",
                            today ? " today" : string.Empty, date.Day));
                    }
                    date = date.AddDays(1);
                }
            }
            sb.Append("</tr></table>");
            return helper.Raw(sb.ToString());
        }
    }
 }