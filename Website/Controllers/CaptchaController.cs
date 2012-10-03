﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class CaptchaController : Controller
    {
		[AcceptVerbs(HttpVerbs.Get)]
		public FileResult Image(string id)
        {
			CaptchaImage captchaImage = CaptchaImage.GetCachedCaptcha(id);

			MemoryStream stream = new MemoryStream();
			if (captchaImage != null)
			{
				using (Bitmap bitmap = captchaImage.RenderImage())
				{
					bitmap.Save(stream, ImageFormat.Png);
				}
			}

			return File(stream.ToArray(), "image/png");
        }

		public static CaptchaImage GenerateCaptchaImage()
		{
			CaptchaImage captchaImage = new CaptchaImage();

			HttpRuntime.Cache.Add(
				captchaImage.UniqueId,
				captchaImage,
				null,
				DateTime.Now.AddSeconds(CaptchaImage.CacheTimeOut),
				Cache.NoSlidingExpiration,
				CacheItemPriority.NotRemovable,
				null);

			return captchaImage;
		}
    }
}