using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
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

					if (agents.Count > 0 && agents.Count < Configuration.MaxAgentsSearchResultSetLength)
					{
						viewModel.Agents = agents;
					}
					else if (agents.Count == 0)
					{
						ViewBag.Error = "По вашему запросу не найдено ни одно совпадение";
					}
					else
					{
						ViewBag.Error = string.Format(
							"По вашему запросу найдено более {0} записей. Уточните свой запрос",
							agents.Count);
					}
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
			return new List<Agent>()
				{
					new Agent() { Id = Guid.NewGuid(), Name = "Иван Иванов", FirmName = "Рос-Страх", CertificateNumber = "11-12345" },
					new Agent() { Id = Guid.NewGuid(), Name = "Перт Иванов", FirmName = "Рос-Страх", CertificateNumber = "11-22345" },
					new Agent() { Id = Guid.NewGuid(), Name = "Сидор Иванов", FirmName = "Страх-Страх", CertificateNumber = "11-32345" },
					new Agent() { Id = Guid.NewGuid(), Name = "Терентий Иванов", FirmName="Рос", CertificateNumber = "11-42345" }
				}
				.Where(a => a.Name.Contains(searchText) || a.CertificateNumber.Contains(searchText))
				.ToList();
		}
    }
}
