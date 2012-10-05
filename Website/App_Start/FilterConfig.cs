using System.Web;
using System.Web.Mvc;
using Website.App_Start;

namespace Website
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new CustomHandleErrorAttribute());
		}
	}
}