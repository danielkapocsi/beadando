using System;
using System.Collections.Generic;

namespace beadando
{
    public partial class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;

        public Competition(int id, string name, DateTime date, string location)
        {
            Id = id;
            Name = name;
            Date = date;
            Location = location;
        }

        public override string ToString()
        {
            return $"({Id}) - {Name} - {Location} - {Date.ToString("yyyy-MM-dd")}";
        }
    }
}
