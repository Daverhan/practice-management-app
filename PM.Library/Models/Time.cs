﻿using PM.Library.DTO;

namespace PM.Library.Models
{
    public class Time
    {
        public DateTime Date { get; set; }
        public string? Narrative { get; set; }
        public int Hours { get; set; }
        public int Id { get; set; }
        public ProjectDTO? Project { get; set; }
        public EmployeeDTO? Employee { get; set; }

        public Time()
        {

        }

        public Time(TimeDTO dto)
        {
            this.Date = dto.Date;
            this.Narrative = dto.Narrative;
            this.Hours = dto.Hours;
            this.Id = dto.Id;
            this.Project = dto.Project;
            this.Employee = dto.Employee;
        }

        public override string ToString()
        {
            return Id + ") " + Employee?.Name + " wrote a record for Project \"" + Project?.LongName + "\"";
        }
    }
}
