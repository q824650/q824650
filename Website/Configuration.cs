using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Website
{
	public static class Configuration
	{
		public static int MaxAgentsSearchResultSetLength
		{
			get
			{
				return int.Parse(WebConfigurationManager.AppSettings["MaxAgentsSearchResultSetLength"]);
			}
		}
	}
}