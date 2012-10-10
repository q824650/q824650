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
		// You can add custom code to this file. Changes will not be overwritten.
		// 
		// If you want Entity Framework to drop and regenerate your database
		// automatically whenever you change your model schema, add the following
		// code to the Application_Start method in your Global.asax file.
		// Note: this will destroy and re-create your database with every model change.
		// 
		// System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MVC4Sample.Web.Models.PersonContext>());

		public LogContext()
		//	: base("name=LogContext")
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