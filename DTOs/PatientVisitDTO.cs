using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic.Models.DTOs
{
    public class PatientVisitDTO
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int Id { get; set; }
        public DoctorDTO Doctor { get; set; }
        public string BloodSugarLevel { get; set; }
        public string BloodPressure { get; set; }
        public string DoctorNotices { get; set; }
        public string Medicine { get; set; }
        public DateTime? DoctorTime { get; set; }
        public DateTime? VisitTime { get; set; }
        public string NurseNotices { get; set; }
        public int? Serial { get; set; }
        public int VisitTypeId { get; set; }
     //   public string Mobile { get; set; }
        public string IsChecked { get; set; }


    }
}
