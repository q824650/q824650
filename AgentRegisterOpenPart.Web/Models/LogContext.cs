using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AgentRegisterOpenPart.Web.Utils;

namespace AgentRegisterOpenPart.Web.Models
{
	public class LogContext : DbContext
	{		
        public DbSet<LogItem> Logs { get; set; }

		protected LogContext() :base()
		{
		}

        protected LogContext(string connectionString)
                    : base(connectionString)
        {
        }

        public static LogContext getInstance()
        {
            string connectionString = ConfigurationHelper.getConnectionString("LogContext");
            if (string.IsNullOrEmpty(connectionString))
                return new LogContext();
            else
                return new LogContext(connectionString);
        }

        public static int LogInformation(string information, string source, string parameters)
        {
            return WriteToLog(new LogItem()
            {
                Date = DateTime.Now,
                Source = source,
                Parameters = parameters,
                Type = LogItemType.Info,
                Text = information,
            });
        }

        public static int LogException(Exception exception, string source, string parameters)
        {
            return WriteToLog(new LogItem()
            {
                Date = DateTime.Now,
                Source = source,
                Parameters = parameters,
                Type = LogItemType.Error,
                Text = ExceptionTextFormatter.FormatToString(exception),
            });
        }

        private static int WriteToLog(LogItem logItem)
        {
            using (LogContext db = LogContext.getInstance())
            {
                db.Logs.Add(logItem);
                db.SaveChanges();

                return logItem.Id;
            }
        }
	}
}