using System;
using System.Collections.Generic;

namespace Design_Underground_System
{
  class Program
  {
    static void Main(string[] args)
    {
      //https://www.youtube.com/watch?v=AXMSHVedysk
    }
  }

  public class UndergroundSystem
  {
    const string SEPARATOR = ",";
    Dictionary<int, TrainEvent> arrivals;
    Dictionary<string, Departure> departures;
    public UndergroundSystem()
    {
      arrivals = new Dictionary<int, TrainEvent>();
      departures = new Dictionary<string, Departure>();
    }

    public void CheckIn(int id, string stationName, int t)
    {
      arrivals.Add(id, new TrainEvent(id, stationName, t));
    }

    public void CheckOut(int id, string stationName, int t)
    {
      var arrival = arrivals[id];
      arrivals.Remove(id);
      string key = arrival.stationName + SEPARATOR + stationName;
      int diff = t - arrival.time;
      if (!departures.ContainsKey(key))
      {
        departures.Add(key, new Departure());
      }
      var departure = departures[key];
      departure.UpdateTime(diff);
    }

    public double GetAverageTime(string startStation, string endStation)
    {
      string key = startStation + SEPARATOR + endStation;
      var departure = departures[key];
      return departure.GetAverage();
    }

    public class TrainEvent
    {
      public int id;
      public string stationName;
      public int time;
      public TrainEvent(int id, string stationName, int time)
      {
        this.id = id;
        this.stationName = stationName;
        this.time = time;
      }
    }

    public class Departure
    {
      public double totalTime;
      public int count;
      public void UpdateTime(int diff)
      {
        totalTime += diff;
        ++count;
      }

      public double GetAverage()
      {
        return totalTime / count;
      }
    }
  }
}
