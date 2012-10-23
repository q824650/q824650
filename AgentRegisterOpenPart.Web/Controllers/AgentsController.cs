using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgentRegisterOpenPart.Web.Models;
using AgentRegisterOpenPart.Web.Utils;
using AgentRegisterOpenPart.Web.BusinessLayer;
using System.Data.Entity.Validation;
using System.Text;

namespace AgentRegisterOpenPart.Web.Controllers
{
    [PerformanceFilterAttribute]
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
                        using (new Performance(ms => ViewBag.DbTime = ms))
                        {
                            // Data access
                            viewModel.Agents = AgentSearch.GetAgents(searchText);
                        }

						if (viewModel.Agents == null)
						{
							ViewBag.Error = "Поиск может быть выполнен по ФИО или номеру сертификата. Пожалуйста уточните запрос.";
						}
						else if (viewModel.Agents.Count == 0)
						{
							ViewBag.Error = "По вашему запросу не найдено ни одного совпадения.";
						}
						else if (viewModel.Agents.Count > ConfigurationHelper.MaxAgentsSearchResultSetLength)
						{
							ViewBag.Error = "Вашему запросу соответствует слишком много результатов поиска. Пожалуйста, уточните свой запрос.";
							viewModel.Agents = null;
						}
					}
                    catch (DbEntityValidationException dbEx)
                    {
                        StringBuilder errors = new StringBuilder();
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                errors.AppendFormat("Property: {0} Error: {1}; ", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                        LogContext.LogException(dbEx, "AgentRegisterOpenPart", errors.ToString());
                        ViewBag.Error = "Произошла ошибка данных. Попробуйте воспользоваться поиском позднее. Приносим извинения за неудобства.";
                        viewModel.Agents = null;
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
				ViewBag.CaptchaError = "Текст не совпадает с указанным на изображении.";
			}

			viewModel.CaptchaImage = CaptchaImage.GenerateCaptchaImage();
			return View(viewModel);
		}
	}
}
