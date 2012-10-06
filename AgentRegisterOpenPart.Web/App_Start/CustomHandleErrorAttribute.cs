using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgentRegisterOpenPart.Web
{
	public class CustomHandleErrorAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			Exception exception = filterContext.Exception;
			base.OnException(filterContext);
		}
	}
}