using System;
using System.Collections.Generic;

#nullable disable

namespace clinic.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
