using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentRegisterOpenPart.Web.Models
{
	public class Agent
	{
		public virtual int Id { get; set; }	
		public virtual string FirstName { get; set; }
		public virtual string MiddleName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string CertificateNumber { get; set; }
        
        public virtual int InsuranceCompanyWorksInId { get; set; }
        public virtual InsuranceCompany InsuranceCompanyWorksIn { get; set; }

		public virtual DateTime DateAdded { get; set; }
		
        public virtual string TerritoryWorksAtKLADRCode { get; set; }
        public virtual Territory TerritoryWorksAt { get; set; }

		public virtual int StatusID { get; set; }
		public virtual Status Status { get; set; }

		public virtual string OrganizationHandedCertificate { get; set; }
		public virtual DateTime RecordValidDeadline { get; set; }
		public virtual string ProductsWorksWith { get; set; }
	}
}