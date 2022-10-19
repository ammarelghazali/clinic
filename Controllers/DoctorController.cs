using clinic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using clinic.Models.DTOs;
using clinic.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        clinicContext db = new clinicContext();
        // GET: api/<DoctorController>
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

        private Patient GetPatientByMobile(String mobile)
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

        [HttpGet("GetPatientbyMobile")]
        public IActionResult GetPatientbyMobile(string mobile)
        {
            try
            {
                clinicContext db = new clinicContext();
                Patient patient = db.Patients.Include(x => x.Visits)
                   .ThenInclude(x => x.DoctorIdFkNavigation).Single(x => x.Mobile == mobile);



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
                return Ok(model);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        // POST api/<DoctorController>
        [HttpPost("PostNewPatient")]
        public void PostNewPatient(PostPatientDTO ptTemp)
        {

            clinicContext db = new clinicContext();

            Patient pt = new Patient();

            pt.Name = ptTemp.Name;
            pt.Address = ptTemp.Address;
            pt.Age = ptTemp.Age;
            // pt.Mobile = ptTemp.Mobile;

            pt.Ssn = ptTemp.Ssn;
            db.Patients.Add(pt);
            db.SaveChanges();

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

        }


        // PUT api/<DoctorController>/5
        [HttpPut("UpdatePatientbyID")]
        public void UpdatePatientbyID(Patient ptTemp)
        {
            clinicContext db = new clinicContext();
            var pt = db.Patients.Single(x => x.Id == ptTemp.Id);
            pt.Name = ptTemp.Name;
            pt.Address = ptTemp.Address;
            pt.Age = ptTemp.Age;
            pt.Mobile = ptTemp.Mobile;
            pt.Ssn = ptTemp.Ssn;
            db.Patients.Update(pt);
            db.SaveChanges();


        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("DeletePatientbyID")]
        public void DeletePatientbyID(int id)
        {
            clinicContext db = new clinicContext();
            var pt = db.Patients.Single(x => x.Id == id);
            db.Remove(pt);
            db.SaveChanges();

        }

        [HttpGet("GetPatientsSerialToday")]
        public List<PatientSerialDTO> GetPatientsSerial()
        {
            var today = DateTime.Today;
            // var today =new  DateTime(2022,7,25);

            var visits = (from v in db.Visits
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
            return visits;

        }


        [HttpGet("GetPatientHistory")]
        public List<GetPatientVisitHistoryByDoctorDTO> GetPatientHistory(String mobile)
        {
            var visithistory = (from v in db.Visits
                                join p in db.Patients
                                on v.PatientIdFk equals p.Id
                                where p.Mobile.Equals(mobile)
                                select new GetPatientVisitHistoryByDoctorDTO
                                {
                                    Name = p.Name,
                                    Mobile = p.Mobile,
                                    Age = p.Age,
                                    BloodSugarLevel = v.BloodSugarLevel,
                                    BloodPressure = v.BloodPressure,
                                    DoctorNotices = v.DoctorNotices,
                                    Medicine = v.Medicine,
                                    VisitTime = v.VisitTime,
                                    NurseNotices = v.NurseNotices,
                                })
                         .ToList();
            return visithistory;

        }

        [HttpPost("AddVisit")]
        public ActionResult AddVisit(PatientVisitDTO ptTemp)
        {

            Visit visit = new Visit();
            visit.Medicine = ptTemp.Medicine;
            visit.DoctorNotices = ptTemp.DoctorNotices;
            visit.NurseNotices = ptTemp.NurseNotices;

            visit.VisitTypeIdFk = ptTemp.VisitTypeId;
            visit.PatientIdFk = ptTemp.PatientId;
            visit.DoctorIdFk = ptTemp.DoctorId;
            visit.BloodPressure = ptTemp.BloodPressure;
            visit.BloodSugarLevel = ptTemp.BloodSugarLevel;
            db.SaveChanges();
            return Ok();
        }
        [HttpPut("UpdatePatientVisitbyID")]
        public void UpdateVisit(PatientVisitForDoctorDTO visitdto)
        {
            var visit = db.Visits.Single(x => x.Id==visitdto.Id);

            visit.Id = visitdto.Id;
            visit.Medicine = visitdto.Medicine;
            visit.BloodSugarLevel = visitdto.BloodSugarLevel;
            visit.BloodPressure = visitdto.BloodPressure;
            visit.DoctorNotices = visitdto.DoctorNotices;
            visit.PatientIdFk= visitdto.PatientId;
            
            db.Visits.Update(visit);
            db.SaveChanges();

        }
  

        [HttpDelete("DeletePatientVisitByDate")]
        public void DeletePatientVisit(DateTime date)
        {
            var pt = db.Visits.Where(x => x.VisitTime.Equals(date));
            db.RemoveRange(pt);
            db.SaveChanges();
        }
    }
}
