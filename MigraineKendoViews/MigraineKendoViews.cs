using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.UI;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI.Fluent;

namespace SeriusSoft.MigraineDiaryMVC.MigraineKendoViews
{
    public static class Helpers
    {
			//public static HtmlString KendoCalendar(this HtmlHelper html, string name, DateTime? startDate = null)
			//{
			//	return html.Kendo().Calendar().Name(name).Value(startDate ?? DateTime.Today)
			//}

			public static CalendarBuilder MigraineCalendar(this HtmlHelper html, string name = "MigraineCalendar", DateTime? startDate = null)
			{
				return html.Kendo().Calendar().Name(name).Value(startDate ?? DateTime.Today);
			}

			public static CalendarBuilder AsMonth(this CalendarBuilder calendar)
			{
				return calendar.Depth(CalendarView.Month);
			}

			public static void With(this CalendarBuilder calendar)
			{
				
			}
    }
}
