using System;
using System.Collections.Generic;

#nullable disable

namespace clinic.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Ssn { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
