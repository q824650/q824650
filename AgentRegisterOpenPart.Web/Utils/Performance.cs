using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AgentRegisterOpenPart.Web.Utils
{
    public class Performance : IDisposable
    {
        private Stopwatch _sw;
        private Action<long> _onTimeMeasuredCallback;

        public Performance(Action<long> onTimeMeasuredCallback)
        {
            if (ConfigurationHelper.PerformanceStatisticsOn)
            {
                _onTimeMeasuredCallback = onTimeMeasuredCallback;
                _sw = Stopwatch.StartNew();
            }
        }

        public void Dispose()
        {
            if (ConfigurationHelper.PerformanceStatisticsOn)
            {
                _sw.Stop();
                _onTimeMeasuredCallback(_sw.ElapsedMilliseconds);
            }
        }
    }
}