using Newtonsoft.Json;
using PM.Library.DTO;
using PM.Library.Models;
using PM.Library.Utilities;

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

        private List<TimeDTO> times;

        private TimeService()
        {
            var response = new WebRequestHandler().Get("/Time").Result;
            times = JsonConvert.DeserializeObject<List<TimeDTO>>(response) ?? new List<TimeDTO>();
        }

        public List<TimeDTO> Times
        {
            get { return times; }
        }

        public List<TimeDTO> Search(string query)
        {
            return Times.Where(t => t.Employee.Name.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public TimeDTO? GetTime(int id)
        {
            return times.FirstOrDefault(t => t.Id == id);
        }

        public void AddOrUpdate(TimeDTO time)
        {
            var response = new WebRequestHandler().Post("/Time", time).Result;
            var myUpdatedTime = JsonConvert.DeserializeObject<TimeDTO>(response);
            if (myUpdatedTime != null)
            {
                var existingTime = times.FirstOrDefault(p => p.Id == myUpdatedTime.Id);
                if (existingTime == null)
                {
                    times.Add(myUpdatedTime);
                }
                else
                {
                    var index = times.IndexOf(existingTime);
                    times.RemoveAt(index);
                    times.Insert(index, myUpdatedTime);
                }
            }
        }

        public void DeleteTime(int id)
        {
            var response = new WebRequestHandler().Delete($"/Time/Delete/{id}").Result;

            var timeToRemove = GetTime(id);
            if (timeToRemove != null)
            {
                times.Remove(timeToRemove);
            }
        }
    }
}
