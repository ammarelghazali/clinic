using System;
using System.Collections.Generic;

#nullable disable

namespace clinic.Models
{
    public partial class VisitType
    {
        public VisitType()
        {
            Visits = new HashSet<Visit>();
        }

        public int VisitTypeId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
