using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace AgentRegisterOpenPart.Web.Utils
{
	public class ExceptionTextFormatter : TextExceptionFormatter
	{
		public ExceptionTextFormatter(TextWriter textWriter, Exception exception, Guid handlingInstanceId)
			: base(textWriter, exception, Guid.Empty)
		{
		}

		public override void Format()
		{
			Writer.WriteLine("EXCEPTION");

			foreach (DictionaryEntry entry in this.Exception.Data)
			{
				Writer.WriteLine("* '{0}' = '{1}' *", entry.Key, entry.Value);
			}

			Writer.WriteLine("");
			base.Format();
		}

		protected override void WriteHelpLink(string helpLink)
		{
		}

		public static string FormatToString(Exception exception)
		{
			StringBuilder sb = new StringBuilder();
			StringWriter stringwriter = new StringWriter(sb);

			ExceptionTextFormatter formatter = new ExceptionTextFormatter(stringwriter, exception, Guid.Empty);
			formatter.Format();

			return sb.ToString();
		}

	}
}