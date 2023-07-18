using PM.API.Database;
using PM.Library.DTO;
using PM.Library.Models;

namespace PM.API.EC
{
    public class TimeEC
    {
        public TimeDTO? Delete(int id)
        {
            Filebase.Current.DeleteTimeEntry(id.ToString());
            return Get(id);
        }

        public TimeDTO AddOrUpdate(TimeDTO dto)
        {
            return new TimeDTO(Filebase.Current.AddOrUpdate(new Time(dto)));
        }

        public IEnumerable<TimeDTO> Search(string query)
        {
            return Filebase.Current.Times.Where(t => t.Employee.Name.ToUpper().Contains(query.ToUpper())).Take(1000).Select(t => new TimeDTO(t));
        }

        public TimeDTO? Get(int id)
        {
            return new TimeDTO(Filebase.Current.Times.FirstOrDefault(t => t.Id == id) ?? new Time());
        }
    }
}
