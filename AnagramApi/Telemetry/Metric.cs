using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;


namespace AnagramApi.Telemetry
{
  /// <summary>
  /// Generic metric implementation as documented by SDK
  /// </summary>
  public sealed class Metric : IDisposable
  {
    private static readonly TimeSpan AggregationPeriod = TimeSpan.FromSeconds(60);

    private bool _isDisposed = false;
    private MetricAggregator _aggregator = null;
    private readonly TelemetryClient _telemetryClient;

    public string Name { get; }

    public Metric(string name, TelemetryClient telemetryClient)
    {
      Name = name ?? "null";
      _aggregator = new MetricAggregator(DateTimeOffset.UtcNow);
      _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));

      Task.Run(AggregatorLoopAsync);
    }

    public void TrackValue(double value)
    {
      MetricAggregator currAggregator = _aggregator;
      if (currAggregator != null)
      {
        currAggregator.TrackValue(value);
      }
    }

    private async Task AggregatorLoopAsync()
    {
      while (_isDisposed == false)
      {
        try
        {
          // Wait for end end of the aggregation period:
          await Task.Delay(AggregationPeriod).ConfigureAwait(continueOnCapturedContext: false);

          // Atomically snap the current aggregation:
          MetricAggregator nextAggregator = new MetricAggregator(DateTimeOffset.UtcNow);
          MetricAggregator prevAggregator = Interlocked.Exchange(ref _aggregator, nextAggregator);

          // Only send anything is at least one value was measured:
          if (prevAggregator != null && prevAggregator.Count > 0)
          {
            // Compute the actual aggregation period length:
            TimeSpan aggPeriod = nextAggregator.StartTimestamp - prevAggregator.StartTimestamp;
            if (aggPeriod.TotalMilliseconds < 1)
            {
              aggPeriod = TimeSpan.FromMilliseconds(1);
            }

            // Construct the metric telemetry item and send:
            var aggregatedMetricTelemetry = new MetricTelemetry(
                    Name,
                    prevAggregator.Count,
                    prevAggregator.Sum,
                    prevAggregator.Min,
                    prevAggregator.Max,
                    prevAggregator.StandardDeviation);
            aggregatedMetricTelemetry.Properties["AggregationPeriod"] = aggPeriod.ToString("c");

            _telemetryClient.Track(aggregatedMetricTelemetry);
          }
        }
        catch (Exception)
        {
          //TODO log it for now just stop the error.
        }
      }
    }

    void IDisposable.Dispose()
    {
      _isDisposed = true;
      _aggregator = null;
    }
  }
}
