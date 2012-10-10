using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using AgentRegisterOpenPart.Web.Utils;
using AgentRegisterOpenPart.Web.DataAccessLayer;

namespace AgentRegisterOpenPart.Web.Models
{
	public class AgentContext : DbContext
	{		
		static AgentContext()
        {
            //Database.SetInitializer<AgentContext>(new DropCreateAgentsControllerWithInitialData());
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { //Tell EF that Status and Territory have manually entered key values.
            modelBuilder.Entity<Status>()
            .Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Territory>()
            .Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
	}
}