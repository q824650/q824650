﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
	public class Agent
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string FirmName { get; set; }
		public string CertificateNumber { get; set; }
	}
}