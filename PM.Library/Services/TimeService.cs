﻿using PM.Library.Models;

namespace PM.Library.Services
{
    public class TimeService
    {
        private static TimeService? instance;
        private static object _lock = new object();
        public static TimeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new TimeService();
                    }
                }

                return instance;
            }
        }

        private List<Time> times;
        private int IdsCounter = 1;

        private TimeService()
        {
            times = new List<Time>();
        }

        public List<Time> Times
        {
            get { return times; }
        }

        public List<Time> Search(string query)
        {
            return Times.Where(t => t.Hours.Equals(query)).ToList();
        }

        public Time? GetTime(int id)
        {
            return times.FirstOrDefault(t => t.Id == id);
        }

        public void AddTime(Time? time)
        {
            if(time != null)
            {
                time.Id = IdsCounter++;
                times.Add(time);
            }
        }

        public void DeleteTime(int id)
        {
            var timeToRemove = GetTime(id);

            if (timeToRemove != null)
            {
                times.Remove(timeToRemove);
            }
        }
    }
}