using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramApi.Telemetry
{

  /// <summary>
  /// Application insights metric collection for angram services.
  /// </summary>
  internal sealed class AnagramResolverMetricCollection : IAnagramResolverMetric, IDisposable
  {
    #region Fields
    private readonly Metric _computeTimeMetric;
    private bool _isDisposed;
    #endregion

    #region Constructor
    internal  AnagramResolverMetricCollection(TelemetryClient telemetryClient)
    {
      _computeTimeMetric = new Metric("AnagramComputeTime(ms)", telemetryClient);
    }


    #endregion

    #region Public

    public void TrackComputeTime(TimeSpan anagramComputeTime)
    {
      if (!_isDisposed)
      {
        _computeTimeMetric.TrackValue(anagramComputeTime.TotalMilliseconds);
      }
    }

    public void Dispose()
    {
      _isDisposed = true;
      var disposable = (_computeTimeMetric as IDisposable);
      if (disposable != null)
      {
        disposable.Dispose();
      }
    }
    #endregion

  }
}
