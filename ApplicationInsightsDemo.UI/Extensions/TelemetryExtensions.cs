using System;
using System.Diagnostics;
using Microsoft.ApplicationInsights;

namespace ApplicationInsightsDemo.UI.Extensions
{
    public static class TelemetryExtensions
    {
        public static TimeMetric StartTimeMetric(this TelemetryClient telemetryClient, string metricName)
        {
            return new TimeMetric(telemetryClient, metricName);
        }
    }

    public class TimeMetric : IDisposable
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly string _metricName;
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        public TimeMetric(TelemetryClient telemetryClient, string metricName)
        {
            _telemetryClient = telemetryClient;
            _metricName = metricName;
        }

        public void Complete()
        {
            _stopwatch.Stop();
            double elapsed = _stopwatch.ElapsedMilliseconds;
            _telemetryClient.GetMetric(_metricName).TrackValue(elapsed);
        }

        public void Dispose()
        {
            Complete();
        }
    }
}
