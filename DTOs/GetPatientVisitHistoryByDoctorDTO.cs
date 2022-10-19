using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic.DTOs
{
    public class GetPatientVisitHistoryByDoctorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Mobile { get; set; }
        public string BloodSugarLevel { get; set; }
        public string BloodPressure { get; set; }
        public string DoctorNotices { get; set; }
        public string Medicine { get; set; }

        public DateTime? VisitTime { get; set; }
        public string NurseNotices { get; set; }

    }
}
