﻿using PM.Library.DTO;

namespace PM.Library.Models
{
    public class Project
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Boolean IsActive { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public ClientDTO? Client { get; set; }
        public List<Bill>? Bills { get; set; }

        public Project()
        {
            LongName = string.Empty;
            ShortName = string.Empty;
        }

        public Project(ProjectDTO dto)
        {
            this.Id = dto.Id;
            this.OpenDate = dto.OpenDate;
            this.ClosedDate = dto.ClosedDate;
            this.IsActive = dto.IsActive;
            this.ShortName = dto.ShortName;
            this.LongName = dto.LongName;
            this.Client = dto.Client;
            this.Bills = dto.Bills;
        }

        public override string ToString()
        {
            return Id + ") " + LongName;
        }
    }
}