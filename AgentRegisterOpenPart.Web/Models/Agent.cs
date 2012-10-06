using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentRegisterOpenPart.Web.Models
{
	public class Agent
	{
		public virtual int Id { get; set; }
		public virtual int UId { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string MiddleName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string CertificateNr { get; set; }
		public virtual string FirmWhereWorks { get; set; }
		public virtual DateTime DateIncludedInRegister { get; set; }
		public virtual string NrInRegister { get; set; }
		public virtual int TerritoryWhereWorksID { get; set; }
		public virtual string TerritoryWhereWorks { get; set; }
		public virtual int StatusID { get; set; }
		public virtual string Status { get; set; }
		public virtual string FirmWhereStudied { get; set; }
		public virtual DateTime RecordValidDeadline { get; set; }
		public virtual string ProductsWorksWith { get; set; }
	}
}