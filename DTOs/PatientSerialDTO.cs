using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic.Models.DTOs
{
    public class PatientSerialDTO
    {
     
        public string PatientName { get; set; }
        public string PatientMobile { get; set; }
        public int? Serial { get; set; }    
        public DateTime? VisitTime { get; set; }
        public string NurseNotices { get; set; }
     

    
    }
}
