using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AgentRegisterOpenPart.Web.Models
{
	public class Agent
	{
		public virtual int Id { get; set; }

        [StringLength(60, MinimumLength = 1, ErrorMessage="Длина номера агентского договора находится вне пределов 1..60.")]
        public virtual string AgencyAgreementNumber { get; set; }
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Длина имени находится вне пределов 1..60.")]
		public virtual string FirstName { get; set; }
        [StringLength(60, ErrorMessage = "Длина отчества находится вне пределов 0..60.")]
		public virtual string MiddleName { get; set; }
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Длина фамилии находится вне пределов 1..60.")]
		public virtual string LastName { get; set; }
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Длина номера сертификата находится вне пределов 1..60.")]
		public virtual string CertificateNumber { get; set; }
        public virtual DateTime DateCertificateExpires { get; set; }

        public virtual int InsuranceCompanyWorksInId { get; set; }
        public virtual InsuranceCompany InsuranceCompanyWorksIn { get; set; }
		
        [StringLength(15)]
        public virtual string TerritoryWorksAtKLADRCode { get; set; }
        public virtual Territory TerritoryWorksAt { get; set; }

		public virtual int StatusID { get; set; }
        [StringLength(20, ErrorMessage = "Длина наименования статуса находится вне пределов 0..20.")]
		public virtual Status Status { get; set; }

        [StringLength(60, MinimumLength = 1, ErrorMessage = "Длина названия организации, выдавшей сертификат, находится вне пределов 1..60.")]
		public virtual string OrganizationHandedCertificate { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Длина списка продуктов находится вне пределов 1..255.")]
		public virtual string ProductsWorksWith { get; set; }
	}
}