using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using AgentRegisterOpenPart.Web.Models;

namespace AgentRegisterOpenPart.Web.DataAccessLayer
{
    public class CreateAgentsControllerWithInitialData : CreateDatabaseIfNotExists<AgentContext>
    {
        protected override void Seed(AgentContext context)
        {            
            InitDatabase.CreateIndicesSeedLookups(context);
            context.SaveChanges();
        }
    }
}
