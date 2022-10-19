using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic.DTOs
{
    public class PatientVisitForDoctorDTO
    {
        public int PatientId { get; set; }
        public int Id { get; set; }
        public string BloodSugarLevel { get; set; }
        public string BloodPressure { get; set; }
        public string DoctorNotices { get; set; }
        public string Medicine { get; set; }
        public int VisitTypeId { get; set; }
        public string IsChecked { get; set; }
    }
}
