using System.Diagnostics;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Gauge;
using Mxc.IBSDiscountCard.Common.Metrics;

namespace Mxc.IBSDiscountCard.WebApi.Metrics
{
    public class MetricsService : IMetricsService
    {
        private readonly IMetricsRoot _metricsRoot;
        private Task _processMetricsLoop;

        public MetricsService(IMetricsRoot metricsRoot)
        {
            _metricsRoot = metricsRoot;
        }

        public void Start()
        {
            if (_processMetricsLoop != null)
            {
                return;
            }

            _processMetricsLoop = Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1500);
                    _metricsRoot.Measure.Gauge.SetValue(ProcessMemory,
                        () => Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0);
                }
            });
        }

        public GaugeOptions ProcessMemory => new GaugeOptions
        {
            Name = "Physical process memory",
            MeasurementUnit = Unit.MegaBytes
        };
    }
}