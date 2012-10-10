using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgentRegisterOpenPart.Web.Models
{
	public class LogItem
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public LogItemType Type { get; set; }

		public DateTime Date { get; set; }

		public string Text { get; set; }

		public string Source { get; set; }

		public string Parameters { get; set; }
	}

	public enum LogItemType
	{
		Info = 0,
		Error = 1
	}
}