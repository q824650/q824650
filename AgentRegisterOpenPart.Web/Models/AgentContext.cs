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
            modelBuilder.Entity<Agent>().HasKey<int>(a => a.Id);
            modelBuilder.Entity<Agent>()
                .Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            modelBuilder.Entity<Agent>().Property(a => a.AgencyAgreementNumber).IsRequired();
            modelBuilder.Entity<Agent>().Property(a => a.FirstName).IsRequired();
            modelBuilder.Entity<Agent>().Property(a => a.MiddleName).IsRequired();
            modelBuilder.Entity<Agent>().Property(a => a.LastName).IsRequired();
            modelBuilder.Entity<Agent>().Property(a => a.CertificateNumber).IsRequired();            
            modelBuilder.Entity<Agent>().Property<DateTime>(a => a.DateCertificateExpires).IsRequired();
            modelBuilder.Entity<Agent>().Property<int>(a => a.InsuranceCompanyWorksInId).IsRequired();            
            modelBuilder.Entity<Agent>()
                .HasRequired<Territory>(a => a.TerritoryWorksAt)
                .WithMany()
                .HasForeignKey<string>(a=>a.TerritoryWorksAtKLADRCode);
            modelBuilder.Entity<Agent>().Property<int>(a => a.StatusID).IsRequired();
            modelBuilder.Entity<Agent>().Property(a => a.OrganizationHandedCertificate).IsRequired();
            modelBuilder.Entity<Agent>().Property(a => a.ProductsWorksWith).IsRequired();

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