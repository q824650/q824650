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
		// System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MVC4Sample.Web.Models.PersonContext>());

		public LogContext()
//		//	: base("name=LogContext")
		{
		}

		public DbSet<LogItem> Logs { get; set; }



		public static int LogException(Exception exception, string source, string parameters)
		{
			using (LogContext db = new LogContext())
			{
				LogItem logItem = new LogItem()
				{
					Date = DateTime.Now,
					Source = source,
					Parameters = parameters,
					Type = LogItemType.Error,
					Text = ExceptionTextFormatter.FormatToString(exception),
				};

				db.Logs.Add(logItem);
				db.SaveChanges();

				return logItem.Id;
			}
		}
	}
}