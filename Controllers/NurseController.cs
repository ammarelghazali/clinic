using clinic.DTOs;
using clinic.Models;
using clinic.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace clinic.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class NurseController : ControllerBase
    {
        clinicContext db = new clinicContext();
        // GET: api/<MainController>
        [HttpGet("GetAllPatients")]
        public List<PatientDTO> GetAllPatients()
        {
            clinicContext db = new clinicContext();
            List<Patient> patients = db.Patients.Include(x => x.Visits).ThenInclude(x => x.DoctorIdFkNavigation).ToList();
            List<PatientDTO> ptlist = new List<PatientDTO>();
            foreach (var pt in patients)
            {
                PatientDTO ptdto = new PatientDTO();
                ptdto.Id = pt.Id;
                ptdto.Name = pt.Name;
                ptdto.Mobile = pt.Mobile;
                ptdto.Ssn = pt.Ssn;
                ptdto.Age = ptdto.Age;
                ptdto.Address = pt.Address;
                List<PatientVisitDTO> visits = new List<PatientVisitDTO>();
                foreach (var visit in pt.Visits)
                {
                    PatientVisitDTO ptvisitdto = new PatientVisitDTO();
                    ptvisitdto.Id = visit.Id;
                    ptvisitdto.NurseNotices = visit.NurseNotices;
                    ptvisitdto.Medicine = visit.Medicine;
                    ptvisitdto.DoctorNotices = visit.DoctorNotices;
                    ptvisitdto.BloodPressure = visit.BloodPressure;
                    ptvisitdto.VisitTime = visit.VisitTime;

                    DoctorDTO doctor = new DoctorDTO();
                    doctor.Id = visit.DoctorIdFkNavigation.Id;
                    doctor.Name = visit.DoctorIdFkNavigation.Name;
                    doctor.Number = visit.DoctorIdFkNavigation.Number;
                    ptvisitdto.Doctor = doctor;

                    visits.Add(ptvisitdto);
                }
                ptdto.Visits = visits;
                ptlist.Add(ptdto);
            }
            return ptlist;
        }
        [HttpPost("PostVisit")]
        public void InsertSerial(NursePostVisitDTO visitdto)
        {
            int? serial = 1;
            Visit visit = new Visit();

            serial = GetMaxSerial();

            visit.Serial = serial;
            visit.VisitTime = DateTime.Today;
            visit.DoctorIdFk = visitdto.DoctorIdFk;
            visit.PatientIdFk = visitdto.PatientIdFk;
            visit.VisitTypeIdFk = visitdto.VisitTypeIdFk;


            db.Visits.Add(visit);
            db.SaveChanges();

        }
        // [HttpPost("SerialPOST")]
        private int? GetMaxSerial()
        {
            int? serial = 1;
            var today = DateTime.Today;
            var maxSerial = db.Visits.Where(x => x.VisitTime.Value.Date == today.Date).Max(x => x.Serial);

            if (maxSerial != null)
            {
                serial = maxSerial + 1;
            }

            return serial;
        }

        // GET api/<MainController>/5
        [HttpGet("GetPatientByID")]
        public PatientDTO GetPatientByID(int id)
        {
            Patient patient = db.Patients.Include(x => x.Visits).ThenInclude(x => x.DoctorIdFkNavigation).Single(x => x.Id == id);
            PatientDTO model = new PatientDTO();
            model.Id = patient.Id;
            model.Name = patient.Name;
            model.Mobile = patient.Mobile;
            model.Ssn = patient.Ssn;
            List<PatientVisitDTO> visitlist = new List<PatientVisitDTO>();
            foreach (var visit in patient.Visits)
            {
                PatientVisitDTO visitdto = new PatientVisitDTO();
                visitdto.Id = visit.Id;
                visitdto.NurseNotices = visit.NurseNotices;
                visitdto.BloodPressure = visit.BloodPressure;
                visitdto.BloodSugarLevel = visit.BloodSugarLevel;
                visitdto.VisitTime = visit.VisitTime;
                visitdto.Medicine = visit.Medicine;

                DoctorDTO DocDTO = new DoctorDTO();
                DocDTO.Id = visit.DoctorIdFkNavigation.Id;
                DocDTO.Name = visit.DoctorIdFkNavigation.Name;
                DocDTO.Number = visit.DoctorIdFkNavigation.Number;

                visitdto.Doctor = DocDTO;
                visitlist.Add(visitdto);

            }
            model.Visits = visitlist;
            return model;
        }

        [HttpGet("Getbymobile5")]
        public Patient GetPatientByMobile(String mobile)
        {
            Patient patient = db.Patients.SingleOrDefault(x => x.Mobile == mobile);
            return patient;
        }
        private Patient InsertPaitent(PostPatientDTO ptTemp)
        {
            Patient pt = GetPatientByMobile(ptTemp.Mobile);

            if (pt == null)
            {
                pt = new Patient();

                pt.Name = ptTemp.Name;
                pt.Address = ptTemp.Address;
                pt.Age = ptTemp.Age;
                pt.Mobile = ptTemp.Mobile;
                pt.Ssn = ptTemp.Ssn;
                db.Patients.Add(pt);
                db.SaveChanges();
            }

            return pt;
        }


        // POST api/<MainController>
        [HttpPost("AddPatient")]
        public ActionResult AddPatient(PostPatientDTO ptTemp)
        {
            Patient pt = InsertPaitent(ptTemp);

            List<Visit> visitlist = new List<Visit>();
            foreach (var visit in ptTemp.Visits)
            {
                Visit vt = new Visit();
                //  vt.Id = visit.Id;
                vt.Medicine = visit.Medicine;
                vt.DoctorNotices = visit.DoctorNotices;
                vt.NurseNotices = visit.NurseNotices;
                vt.DoctorIdFk = visit.DoctorIdFk;
                vt.PatientIdFk = pt.Id;
                vt.VisitTypeIdFk = visit.VisitTypeIdFk;
                visitlist.Add(vt);
            }
            db.Visits.AddRange(visitlist);
            db.SaveChanges();
            return Ok();
        }



        // PUT api/<MainController>/5
        [HttpPut("UpdatePatient")]
        public void UpdatePatient(NurseUpdatePatientDTO ptTemp)
        {
            
            var pt = db.Patients.Single(x => x.Id == ptTemp.Id);
            pt.Name = ptTemp.Name;
            pt.Address = ptTemp.Address;
            pt.Age = ptTemp.Age;
            pt.Mobile = ptTemp.Mobile;
            pt.Ssn = ptTemp.Ssn;
            db.Patients.Update(pt);
            db.SaveChanges();


        }

        // DELETE api/<MainController>/5
        [HttpDelete("DeletePatientbyid")]
        public void DeletePatient(int id)
        {
            clinicContext db = new clinicContext();
            var pt = db.Patients.Single(x => x.Id == id);
            db.Remove(pt);
            db.SaveChanges();

        }

        [HttpGet("GetPatientsSerialToday")]
        public List<PatientSerialDTO> GetPatientsSerial()
        {
            var today = DateTime.Today.Date;
            // var today =new  DateTime(2022,7,25);

            List<PatientSerialDTO> visits = NewMethod(today);
            return visits;
        }

        private List<PatientSerialDTO> NewMethod(DateTime today)
        {
            return (from v in db.Visits
                    join p in db.Patients
                    on v.PatientIdFk equals p.Id
                    where v.VisitTime.Value.Date.Equals(today)
                    select new PatientSerialDTO
                    {
                        PatientName = p.Name,
                        PatientMobile = p.Mobile,
                        Serial = v.Serial,
                        VisitTime = v.VisitTime
                    })
                         .ToList();
        }

        [HttpDelete("DeletePatientSerialByDate")]
        public void DeletePatientSerial(DateTime date,int serial)
        { 
            var pt = db.Visits.Where(x => x.VisitTime.Equals(date)).Where(x=>x.Serial==serial);
          
            db.RemoveRange();
            db.SaveChanges();
            
        }
        [HttpPut("IsChecked")]
        public void IsChecked(int id)
        {
            var visit = db.Visits.Single(x => x.Id == id);
            visit.IsChecked = true;


            db.Visits.Update(visit);
            db.SaveChanges();


        }




    }
}
