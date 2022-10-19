using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic.DTOs
{
    public class NursePostVisitDTO
    {
        public int DoctorIdFk { get; set; }
        public int PatientIdFk { get; set; }
        public DateTime? VisitTime { get; set; }
        public string NurseNotices { get; set; }
        public int VisitTypeIdFk { get; set; }
        public int? Serial { get; set; }
        public string IsChecked { get; set; }


    }
}
