﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgentRegisterOpenPart.Web.Models;
using AgentRegisterOpenPart.Web.Utils;

namespace AgentRegisterOpenPart.Web.Controllers
{
	public class CaptchaController : Controller
	{
		[AcceptVerbs(HttpVerbs.Get)]
		public FileResult Image(string id)
		{
			CaptchaImage captchaImage = CaptchaImageCache.GetCachedCaptcha(id);
			using (MemoryStream stream = new MemoryStream())
			{
                if (captchaImage != null)
                {
                    using (Bitmap bitmap = captchaImage.RenderImage())
                    {
                        bitmap.Save(stream, ImageFormat.Png);
                    }
                }
                else
                {
                    LogContext.LogInformation("Iamge is not found in cache (CaptchaController method Image)", "CaptchaController", "captchaImage.Id = " + id);
                }
				return File(stream.ToArray(), "image/png");
			}
		}
	}
}
