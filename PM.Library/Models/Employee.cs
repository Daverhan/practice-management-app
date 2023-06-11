﻿namespace PM.Library.Models
{
    public class Employee
    {
        public string? Name { get; set; }
        public double Rate { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return Id + ". " + Name;
        }
    }
}
