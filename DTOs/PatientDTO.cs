using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic.Models.DTOs
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Ssn { get; set; }

        public List<PatientVisitDTO> Visits { get; set; }
    }
}
