using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace AgentRegisterOpenPart.Web.Utils
{
    public static class CaptchaImageCache 
    {
        private static readonly object _theLock = new object();
        private static readonly LinkedList<CaptchaImage> _images = new LinkedList<CaptchaImage>();
        private static Timer _cleaningTimer;
        private static int _cleaningTimerPeriodMsec = 10000; // 10 sec

        static CaptchaImageCache()
        {
            _cleaningTimer = new Timer(new TimerCallback(o =>
                {
                    try
                    {
                        lock (_theLock)
                        {
                            // Get list of old images to remove from cache
                            List<CaptchaImage> imagesToRemove = new List<CaptchaImage>();
                            foreach (CaptchaImage image in _images)
                            {
                                if (image.RenderedAt.AddSeconds(CaptchaImage.CacheTimeOut) < DateTime.Now)
                                {
                                    imagesToRemove.Add(image);
                                }
                            }

                            // Remove old images from cache
                            foreach (CaptchaImage image in imagesToRemove)
                            {
                                _images.Remove(image);
                            }
                        }
                    }
                    finally
                    {
                        SheduleNextCleaningTimerTick();
                    }
                }));
            SheduleNextCleaningTimerTick();
        }

        private static void SheduleNextCleaningTimerTick()
        {
            _cleaningTimer.Change(_cleaningTimerPeriodMsec, Timeout.Infinite);
        }

        /// <summary>
        /// Generates new captcha image and caches it
        /// </summary>
        /// <returns>CaptchaImage</returns>
        public static CaptchaImage GenerateCaptchaImage()
        {
            CaptchaImage captchaImage = new CaptchaImage();
            lock (_theLock)
            {
                _images.AddFirst(captchaImage);
            }
            return captchaImage;
        }

        /// <summary>
        /// Gets the cached captcha and then removes it from cache
        /// </summary>
        /// <param name="id">Captcha Id</param>
        /// <returns>CaptchaImage</returns>
        public static CaptchaImage GetAndRemoveCachedCaptcha(string id)
        {
            CaptchaImage image = null;
            lock (_theLock)
            {
                image = GetCachedCaptcha(id);
                if (image != null)
                {
                    _images.Remove(image);
                }
            }
            return image;
        }

        /// <summary>
        /// Gets the cached captcha
        /// </summary>
        /// <param name="id">Captcha Id</param>
        /// <returns>CaptchaImage</returns>
        public static CaptchaImage GetCachedCaptcha(string id)
        {
            lock (_theLock)
            {
                foreach (CaptchaImage image in _images)
                {
                    if (image.UniqueId == id)
                    {
                        return image;
                    }
                }
            }
            return null;
        }

    }
}
