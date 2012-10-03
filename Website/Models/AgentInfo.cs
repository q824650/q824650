using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class AgentInfo
    {
        public virtual int Id {get;set;}
        public virtual string FirstName{get;set;}
        public virtual string MiddleName{get;set;}
        public virtual string LastName{get;set;}
        public virtual string CertificateNr { get; set; }
    }
}