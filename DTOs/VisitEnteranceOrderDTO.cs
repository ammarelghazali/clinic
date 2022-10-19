using clinic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic.DTOs
{
    public class VisitEnteranceOrderDTO
    {
        public int Order { get; set; }
        public DateTime? VisitDate { get; set; }
        public string PatientName { get; set; }

    }
}
