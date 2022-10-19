using System;
using System.Collections.Generic;

#nullable disable

namespace clinic.Models
{
    public partial class Visit
    {
        public int Id { get; set; }
        public int DoctorIdFk { get; set; }
        public int PatientIdFk { get; set; }
        public string BloodSugarLevel { get; set; }
        public string BloodPressure { get; set; }
        public string DoctorNotices { get; set; }
        public string Medicine { get; set; }
        public DateTime? DoctorTime { get; set; }
        public DateTime? VisitTime { get; set; }
        public string NurseNotices { get; set; }
        public int VisitTypeIdFk { get; set; }
        public int? Serial { get; set; }
        public bool? IsChecked { get; set; }

        public virtual Doctor DoctorIdFkNavigation { get; set; }
        public virtual Patient PatientIdFkNavigation { get; set; }
        public virtual VisitType VisitTypeIdFkNavigation { get; set; }
    }
}
