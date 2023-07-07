using PM.Library.Models;

namespace PM.Library.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Boolean IsActive { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }

        public ClientDTO()
        {
            Name = string.Empty;
        }

        public ClientDTO(Client client)
        {
            this.Id = client.Id;
            this.Name = client.Name;
            this.Notes = client.Notes;
            this.OpenDate = client.OpenDate;
            this.ClosedDate = client.ClosedDate;
            this.IsActive = client.IsActive;
        }

        public override string ToString()
        {
            return Id + ") " + Name;
        }
    }
}
