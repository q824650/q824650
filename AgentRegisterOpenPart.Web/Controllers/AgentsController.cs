using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgentRegisterOpenPart.Web.Models;
using AgentRegisterOpenPart.Web.Utils;
using AgentRegisterOpenPart.Web.BusinessLayer;

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
        /// <summary>
        /// Works according to the pattern Controller.
        /// Specifics: unlike other methods it has its own processing of exceptions. 
        /// It shows the user a generic message and writes the original exception message and details to the log.
        /// </summary>        
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

					try
					{
						// Data access
						viewModel.Agents = AgentSearch.GetAgents(searchText);

						if (viewModel.Agents == null)
						{
							ViewBag.Error = "Поиск может быть выполнен по ФИО или номеру сертификата. Пожалуйста уточните запрос.";
						}
						else if (viewModel.Agents.Count == 0)
						{
							ViewBag.Error = "По вашему запросу не найдено ни одно совпадение.";
						}
						else if (viewModel.Agents.Count > ConfigurationHelper.MaxAgentsSearchResultSetLength)
						{
							ViewBag.Error = string.Format(
								"По вашему запросу найдено более {0} записей. Пожалуйста уточните свой запрос.",
								ConfigurationHelper.MaxAgentsSearchResultSetLength);

							viewModel.Agents = viewModel.Agents
								.Take(ConfigurationHelper.MaxAgentsSearchResultSetLength)
								.ToList();
						}
					}
					catch (Exception ex)
					{
						LogContext.LogException(ex, "AgentRegisterOpenPart", "Was handled in AgentsController Search catch block");
						ViewBag.Error = "Произошла системная ошибка. Попробуйте воспользоваться поиском позднее. Приносим извинения за неудобства.";
						viewModel.Agents = null;
					}
				}
				else
				{
					ViewBag.Error = "Поисковая строка не задана.";
				}
			}
			else
			{
				ViewBag.Error = "Текст с изображения введен неверно.";
			}

			viewModel.CaptchaImage = CaptchaImage.GenerateCaptchaImage();
			return View(viewModel);
		}
	}
}
