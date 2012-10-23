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
            if (ConfigurationHelper.DatabaseCreationMode == "DropCreateAgentsControllerWithInitialData")            
                Database.SetInitializer<AgentContext>(new DropCreateAgentsControllerWithInitialData());
            else if (ConfigurationHelper.DatabaseCreationMode == "CreateAgentsControllerWithInitialData")
                Database.SetInitializer<AgentContext>(new CreateAgentsControllerWithInitialData());
        }

		public AgentContext()			
		{
		}
		public DbSet<Agent> Agents { get; set; }
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            //Agents
            //modelBuilder.Entity<Agent>().Property<DateTime>(a => a.DateCertificateExpires).IsOptional();
            modelBuilder.Entity<Agent>()
            .HasOptional<Territory>(a => a.TerritoryWorksAt)
            .WithMany()
            .HasForeignKey<string>(a=>a.TerritoryWorksAtKLADRCode);

            //Territory
            //has manually entered key values.
            modelBuilder.Entity<Territory>().HasKey<string>(t=>t.KLADRCode);
            modelBuilder.Entity<Territory>()
            .Property(t => t.KLADRCode)            
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Status
            //has manually entered key values.
            modelBuilder.Entity<Status>()
            .Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }
	}
}