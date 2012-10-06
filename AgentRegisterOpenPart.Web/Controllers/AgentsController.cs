using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgentRegisterOpenPart.Web.Models;
using AgentRegisterOpenPart.Web.Utils;

namespace AgentRegisterOpenPart.Web.Controllers
{
	public class AgentsController : Controller
	{
		[HttpGet]
		public ActionResult Search()
		{
			AgentsSearchViewModel viewModel = new AgentsSearchViewModel();
			viewModel.CaptchaImage = CaptchaImage.GenerateCaptchaImage();
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Search(AgentsSearchViewModel viewModel)
		{
			CaptchaImage image = CaptchaImage.GetAndRemoveCachedCaptcha(viewModel.CaptchaId);

			// If image exists in cache (wasn't removed during timeout)
			if (image == null)
			{
				// Redirect to search again
				return RedirectToAction("Search");
			}

			if (string.Equals(image.Text, viewModel.CaptchaText, StringComparison.OrdinalIgnoreCase))
			{
				string searchText = viewModel.SearchText;
				if (!string.IsNullOrWhiteSpace(searchText))
				{
					searchText = searchText.Trim();

					// Data access
					List<Agent> agents = GetAgents(searchText);
					viewModel.Agents = agents;

					//if (agents.Count > 0 && agents.Count < ConfigurationHelper.MaxAgentsSearchResultSetLength)
					//{
					//	viewModel.Agents = agents;
					//}
					//else if (agents.Count == 0)
					//{
					//	ViewBag.Error = "По вашему запросу не найдено ни одно совпадение";
					//}
					//else
					//{
					//	ViewBag.Error = string.Format(
					//		"По вашему запросу найдено более {0} записей. Уточните свой запрос",
					//		agents.Count);
					//}
				}
				else
				{
					ViewBag.Error = "Поисковая строка не задана";
				}
			}
			else
			{
				ViewBag.Error = "Текст с изображения введен неверно";
			}

			viewModel.CaptchaImage = CaptchaImage.GenerateCaptchaImage();
			return View(viewModel);
		}

		private static List<Agent> GetAgents(string searchText)
		{
			List<Agent> result = new List<Agent>();
			for (int i = 0; i < 10; i++)
			{
				result.Add(new Agent()
					{
						Id = i,
						UId = i,
						NrInRegister = "" + i,
						CertificateNr = "000000000" + i,
						FirstName = "Иван" + i,
						MiddleName = "Терентьевич" + i,
						LastName = "Иванов" + i,
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



			return result;
		}
	}
}
