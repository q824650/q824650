using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AgentRegisterOpenPart.Web.Utils;

namespace AgentRegisterOpenPart.Web.Models
{
	public class AgentContext : DbContext
	{
		// You can add custom code to this file. Changes will not be overwritten.
		// 
		// If you want Entity Framework to drop and regenerate your database
		// automatically whenever you change your model schema, add the following
		// code to the Application_Start method in your Global.asax file.
		// Note: this will destroy and re-create your database with every model change.
		// 
		// System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MVC4Sample.Web.Models.PersonContext>());

		public AgentContext()
			: base("name=AgentContext")
		{
		}

		public DbSet<Agent> Agents { get; set; }



		public static List<Agent> GetAgents(string searchText)
		{
			#region Test data
			List<Agent> result = new List<Agent>();
			for (int i = 0; i < 30; i++)
			{
				result.Add(new Agent()
					{
						Id = i,
						UId = i,
						NrInRegister = "" + i,
						CertificateNr = "000000000" + i,
						FirstName = (char)('А' + i) + "ван",
						MiddleName = (char)('А' + i) + "ванович",
						LastName = (i < 8 || i > 15) ? (char)('А' + i % 3) + "ванов" : "Иванов",
						FirmWhereStudied = "Рос-Страх",
						FirmWhereWorks = "Рос-Страх",
						DateIncludedInRegister = DateTime.Now,
						ProductsWorksWith = "A, B, C, D",
						RecordValidDeadline = DateTime.Now,
						Status = "Активен",
						StatusID = 1,
						TerritoryWhereWorks = "Россия",
						TerritoryWhereWorksID = 1
					});
			}

			//// Fill DB
			//AgentContext ac = new AgentContext();
			//result.ForEach(a => ac.Agents.Add(a));
			//ac.SaveChanges();

			return result;
			#endregion

			using (AgentContext db = new AgentContext())
			{
				IQueryable<Agent> agentsResult = null;

				searchText = searchText.Trim();
				if (HasDigits(searchText))
				{
					if (HasNotAllowedChars(searchText))
					{
						// Уточнить поиск
						return null;
					}
					else
					{
						// Ищем сертификат по числовой подстроке
						agentsResult = db.Agents.Where(a =>
							a.CertificateNr.Contains(searchText));
					}
				}
				else
				{
					string[] words = searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					string w0, w1, w2;
					if (words.Length == 1)
					{
						w0 = words[0].ToLower();
						agentsResult = db.Agents.Where(a =>
							a.LastName.ToLower() == w0);
					}
					else if (words.Length == 2)
					{
						w0 = words[0].ToLower();
						w1 = words[1].ToLower();
						agentsResult = db.Agents.Where(a =>
							(a.FirstName.ToLower() == w0 && a.LastName.ToLower() == w1) ||
							(a.FirstName.ToLower() == w1 && a.LastName.ToLower() == w0));
					}
					else if (words.Length == 3)
					{
						w0 = words[0].ToLower();
						w1 = words[1].ToLower();
						w2 = words[2].ToLower();
						agentsResult = db.Agents.Where(a =>
							(a.FirstName.ToLower() == w1 && a.MiddleName.ToLower() == w2 && a.LastName.ToLower() == w0) ||
							(a.FirstName.ToLower() == w0 && a.MiddleName.ToLower() == w1 && a.LastName.ToLower() == w2));
					}
					else
					{
						// Уточнить поиск
						return null;
					}
				}

				return agentsResult
					.OrderBy(a => a.LastName + " " + a.FirstName + " " + a.MiddleName)
					.Take(ConfigurationHelper.MaxAgentsSearchResultSetLength + 1)
					.ToList();
			}
		}

		private static bool HasDigits(string text)
		{
			foreach (char c in text)
			{
				if (c >= '0' && c <= '9')
				{
					return true;
				}
			}
			return false;
		}

		private static bool HasNotAllowedChars(string text)
		{
			// TODO: 
			return false;
		}
	}
}