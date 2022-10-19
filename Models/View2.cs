using System;
using System.Collections.Generic;

#nullable disable

namespace clinic.Models
{
    public partial class View2
    {
        public int PatientIdFk { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public DateTime? VisitTime { get; set; }
        public string Age { get; set; }
        public string BloodSugarLevel { get; set; }
        public string BloodPressure { get; set; }
        public string DoctorNotices { get; set; }
        public string Medicine { get; set; }
        public DateTime? DoctorTime { get; set; }
        public string NurseNotices { get; set; }
        public int VisitTypeIdFk { get; set; }
    }
}
