using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic.Models.DTOs
{
    public class PostPatientVisitDTO
    {
        public int DoctorIdFk { get; set; }
        public int PatientIdFk { get; set; }
        public string BloodSugarLevel { get; set; }
        public string BloodPressure { get; set; }
        public string DoctorNotices { get; set; }
        public string Medicine { get; set; }
        public DateTime? DoctorTime { get; set; }
        public DateTime? VisitTime { get; set; }
        public int? Serial { get; set; }
        public string NurseNotices { get; set; }
        public int VisitTypeIdFk { get; set; }
    }
}
