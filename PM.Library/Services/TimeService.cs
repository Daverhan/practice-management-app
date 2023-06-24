using PM.Library.Models;

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

        public void AddOrUpdate(Time time)
        {
            if(time.Id == 0)
            {
                time.Id = LastId + 1;
                time.Date = DateTime.Now;
                times.Add(time);
            }
        }

        private int LastId
        {
            get
            {
                return Times.Any() ? Times.Select(t => t.Id).Max() : 0;
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
