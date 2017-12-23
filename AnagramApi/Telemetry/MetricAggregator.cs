using System;
using System.Threading;

namespace AnagramApi.Telemetry
{
  /// <summary>
  /// Aggregates metric values for a single time period.
  /// </summary>
  internal sealed class MetricAggregator
  {
    private SpinLock _trackLock = new SpinLock();

    public DateTimeOffset StartTimestamp { get; }

    public int Count { get; private set; }

    public double Sum { get; private set; }

    public double SumOfSquares { get; private set; }

    public double Min { get; private set; }

    public double Max { get; private set; }

    public double Average { get { return (Count == 0) ? 0 : (Sum / Count); } }

    public double Variance
    {
      get
      {
        return (Count == 0) ? 0 : (SumOfSquares / Count) - (Average * Average);
      }
    }
    public double StandardDeviation { get { return Math.Sqrt(Variance); } }

    public MetricAggregator(DateTimeOffset startTimestamp)
    {
      StartTimestamp = startTimestamp;
    }

    public void TrackValue(double value)
    {
      bool lockAcquired = false;

      try
      {
        _trackLock.Enter(ref lockAcquired);

        if ((Count == 0) || (value < Min)) { Min = value; }
        if ((Count == 0) || (value > Max)) { Max = value; }
        Count++;
        Sum += value;
        SumOfSquares += value * value;
      }
      finally
      {
        if (lockAcquired)
        {
          _trackLock.Exit();
        }
      }
    }
  }
}
