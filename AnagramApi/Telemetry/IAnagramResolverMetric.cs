using System;

namespace AnagramApi.Telemetry
{
  public interface IAnagramResolverMetric
  {
    void TrackComputeTime(TimeSpan anagramComputeTime);

    //Add more as needed
    //void TrackEvent
  }
}