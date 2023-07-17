using PM.API.Database;
using PM.Library.DTO;
using PM.Library.Models;

namespace PM.API.EC
{
    public class ClientEC
    {
        public ClientDTO? Delete(int id)
        {
            Filebase.Current.DeleteClient(id.ToString());
            return Get(id);
        }

        public ClientDTO AddOrUpdate(ClientDTO dto)
        {
            return new ClientDTO(Filebase.Current.AddOrUpdate(new Client(dto)));
        }

        public IEnumerable<ClientDTO> Search(string query)
        {
            return Filebase.Current.Clients.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).Take(1000).Select(c => new ClientDTO(c));
        }

        public ClientDTO? Get(int id)
        {
            return new ClientDTO(Filebase.Current.Clients.FirstOrDefault(c => c.Id == id) ?? new Client());
        }
    }
}
