using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgentRegisterOpenPart.Web.Utils
{
    public class PerformanceFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch _sw;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ConfigurationHelper.PerformanceStatisticsOn)
            {
                _sw = Stopwatch.StartNew();
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            if (ConfigurationHelper.PerformanceStatisticsOn)
            {
                _sw.Stop();
                filterContext.HttpContext.Response.Write(
                    string.Format("<h1>Время: {0}</h1>", _sw.ElapsedMilliseconds));
            }
        }

    }
}