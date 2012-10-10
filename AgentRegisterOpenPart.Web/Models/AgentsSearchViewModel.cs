using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgentRegisterOpenPart.Web.Utils;

namespace AgentRegisterOpenPart.Web.Models
{
	public class AgentsSearchViewModel
	{
		public string SearchText { get; set; }
		public List<Agent> Agents { get; set; }
		public CaptchaImage CaptchaImage { get; set; }
		public string CaptchaText { get; set; }
		public string CaptchaId { get; set; }
	}
}