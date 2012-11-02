using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Configuration;


namespace AgentRegisterOpenPart.Web.Utils
{
	public static class ConfigurationHelper
	{
        public static bool PerformanceStatisticsOn = false;
        public static string MAXAGENTSSEARCHRESULTSETLENGTH = "MaxAgentsSearchResultSetLength";

		public static int MaxAgentsSearchResultSetLength
		{         
			get
			{
                if (RoleEnvironment.IsAvailable)
                {// Read it from ServiceConfiguration.cscfg    
                    return int.Parse(RoleEnvironment.GetConfigurationSettingValue(MAXAGENTSSEARCHRESULTSETLENGTH));
                }
                else
                {   // Fallback to web.config
                    return int.Parse(WebConfigurationManager.AppSettings[MAXAGENTSSEARCHRESULTSETLENGTH]);
                }				
			}
		}

        public static string DatabaseCreationMode
        {
            get
            {
                return WebConfigurationManager.AppSettings["DatabaseCreationMode"];
            }
        }

        ///
        ///http://social.msdn.microsoft.com/Forums/pl-PL/ssdsgetstarted/thread/255c1882-7979-433c-a3aa-61744bd59ac3
        ///
        public static string getConnectionString(string ConnectionStringName)
        {            
            if (RoleEnvironment.IsAvailable)
            {// Read it from ServiceConfiguration.cscfg    
                return RoleEnvironment.GetConfigurationSettingValue(ConnectionStringName);
            }
            else
            {
                // Fallback to web.config
                return WebConfigurationManager.AppSettings[ConnectionStringName];
            }            
        }        
	}
}