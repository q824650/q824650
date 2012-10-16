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
            //Database.SetInitializer<AgentContext>(new CreateAgentsControllerWithInitialData());
            Database.SetInitializer<AgentContext>(new DropCreateAgentsControllerWithInitialData());
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
            modelBuilder.Entity<Agent>()
            .Property(a => a.CertificateNumber).HasMaxLength(60);
            modelBuilder.Entity<Agent>()
            .Property(a => a.FirstName).HasMaxLength(60);
            modelBuilder.Entity<Agent>()
            .Property(a => a.MiddleName).HasMaxLength(60);
            modelBuilder.Entity<Agent>()
            .Property(a => a.LastName).HasMaxLength(60);
            modelBuilder.Entity<Agent>()
            .Property(a => a.TerritoryWorksAtKLADRCode)
            .HasMaxLength(13)
            .HasColumnType("varchar");
            modelBuilder.Entity<Agent>()
            .Property(a => a.AgencyAgreementNumber)
            .HasMaxLength(13)
            .HasColumnType("varchar");

            modelBuilder.Entity<Agent>()
                .HasOptional<Territory>(a => a.TerritoryWorksAt)
                .WithMany()
                .HasForeignKey<string>(a=>a.TerritoryWorksAtKLADRCode);

            //Territory has manually entered key values.
            modelBuilder.Entity<Territory>().HasKey<string>(t=>t.KLADRCode);
            modelBuilder.Entity<Territory>()
            .Property(t => t.KLADRCode)
            .HasMaxLength(13)
            .HasColumnType("varchar")
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Status has manually entered key values.
            modelBuilder.Entity<Status>()
            .Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }
	}
}