using System;
using System.Collections.Generic;

#nullable disable

namespace clinic.Models
{
    public partial class VisitEnteranceOrder
    {
        public int Order { get; set; }
        public DateTime? VisitDate { get; set; }
        public string PatientName { get; set; }
    }
}
