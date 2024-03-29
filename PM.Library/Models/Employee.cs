﻿using PM.Library.DTO;

namespace PM.Library.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int Id { get; set; }

        public Employee()
        {
            Name = string.Empty;
        }

        public Employee(EmployeeDTO dto)
        {
            this.Id = dto.Id;
            this.Name = dto.Name;
            this.Rate = dto.Rate;
        }

        public override string ToString()
        {
            return Id + ") " + Name;
        }
    }
}
