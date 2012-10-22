using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AgentRegisterOpenPart.Web.Utils
{
	public static class ConfigurationHelper
	{
        public static bool PerformanceStatisticsOn = false;

		public static int MaxAgentsSearchResultSetLength
		{
			get
			{
				return int.Parse(WebConfigurationManager.AppSettings["MaxAgentsSearchResultSetLength"]);
			}
		}

        public static string DatabaseCreationMode
        {
            get
            {
                return WebConfigurationManager.AppSettings["DatabaseCreationMode"];
            }
        }
	}
}