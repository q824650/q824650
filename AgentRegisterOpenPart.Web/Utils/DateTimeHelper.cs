using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentRegisterOpenPart.Web.Utils
{
	public static class DateTimeHelper
	{
		public static string ToDateString(DateTime dateTime)
		{
			return dateTime.ToString("dd.MM.yyyy");
		}
	}
}