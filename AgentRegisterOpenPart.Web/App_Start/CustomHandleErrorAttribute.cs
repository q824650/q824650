using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgentRegisterOpenPart.Web.Models;

namespace AgentRegisterOpenPart.Web
{
	public class CustomHandleErrorAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			// Log exception
			filterContext.Exception.Data["Url"] = filterContext.HttpContext.Request.QueryString;
			filterContext.Exception.Data["Controller"] = filterContext.RouteData.Values["controller"].ToString();
			filterContext.Exception.Data["Action"] = filterContext.RouteData.Values["action"].ToString();

			LogContext.LogException(filterContext.Exception, "AgentRegisterOpenPart", string.Empty);
			base.OnException(filterContext);
		}
	}
}