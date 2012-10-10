using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AgentRegisterOpenPart.Web.Utils;
using AgentRegisterOpenPart.Web.DataAccessLayer;

namespace AgentRegisterOpenPart.Web.Models
{
	public class AgentContext : DbContext
	{
		// System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MVC4Sample.Web.Models.PersonContext>());
		static AgentContext()
        {
            Database.SetInitializer<AgentContext>(new DropCreateAgentsControllerWithInitialData());
        }

		public AgentContext()
//			: base("name=AgentContext")
		{

		}
		public DbSet<Agent> Agents { get; set; }
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<Status> Statuses { get; set; }
	}
}