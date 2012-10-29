using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AgentRegisterOpenPart.Web.Models;

namespace AgentRegisterOpenPart.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

            ForceAgentContextDatabaseCreation();
		}

        private static void ForceAgentContextDatabaseCreation()
        {            
            using (var context = new AgentContext())
            {
                try
                {
                    context.Database.Initialize(force: false);
                }
                catch (Exception ex)
                {
                    var dbEx = ex.InnerException as DbEntityValidationException;
                    if (dbEx != null)
                    {
                        StringBuilder errors = new StringBuilder();
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                errors.AppendFormat("Property: {0} Error: {1}; ", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                        LogContext.LogException(dbEx, "AgentRegisterOpenPart", errors.ToString());
                    }
                    throw new Exception("Произошла ошибка инициализации данных.");
                }
            }
        }
	}
}