using Hos.HELPERS;
using Hos.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Hos.Controllers
{
    [RoutePrefix("api/Appointment")]
    public class AppointmentController : ApiController
    {
        private HosContext context = new HosContext();

        [AllowAnonymous]
        //[Authorize]
        [Route("Data")]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage responseMessage;

            var result =(from optionsData in context.OptionsDatas.ToList()
                          select new
                          {
                              User = from user in context.Users.Where(c => c.UserName == User.Identity.Name).ToList()
                                          select new
                                          {
                                              FirstName = user.FirstName,
                                              LastName = user.LastName,
                                              National_ID_Number = user.National_ID_Number,
                                              Registration_Number = user.UserName
                                          },
                              Faculties = from faculty in context.Faculties.ToList()
                                           select new
                                           {
                                               FacultyID = faculty.FacultyID,
                                               Name = faculty.Name,
                                               Courses = from course in context.Courses.Where(c => c.FacultyID == faculty.FacultyID).ToList()
                                                         select new
                                                         {
                                                             CourseID = course.CourseID,
                                                             Name = course.Name
                                                         }
                                           },
                              Programs = from program in context.Programs.ToList()
                                          select new
                                          {
                                              ProgramID = program.ProgramID,
                                              Name = program.Name,
                                              Years = from year in context.Years.Where(c => c.ProgramID == program.ProgramID).ToList()
                                                      select new
                                                      {
                                                          YearID = year.YearID,
                                                          Name = year.Name,
                                                          Semesters = from semester in context.Semesters.Where(c => c.YearID == year.YearID).ToList()
                                                                      select new
                                                                      {
                                                                          SemesterID = semester.SemesterID,
                                                                          Name = semester.Name
                                                                      }
                                                      }
                                          },
                              
                              Medical_Types = from medical_Type in context.Medical_Types.ToList()
                                              select new
                                              {
                                                  Medical_TypeID = medical_Type.Medical_TypeID,
                                                  Name = medical_Type.Name,
                                                  Available_Doctors = from available_Doctor in context.Available_Doctors.Where(c => c.Medical_TypeID == medical_Type.Medical_TypeID).ToList()
                                                                      select new
                                                                      {
                                                                          Available_DoctorID = available_Doctor.Available_DoctorID,
                                                                          Name = available_Doctor.Name
                                                                      }
                                              }
                          }).AsEnumerable();

            responseMessage = Request.CreateResponse(HttpStatusCode.OK, result);
            return responseMessage;
        }

        [Authorize]
        [Route("AppointmentData")]
        public async Task<IHttpActionResult> PostAppointment(JObject data)
        {
            dynamic json = data;

            var appointment = 
                new Appointment {
                    Registration_Number = json.Registration_Number,
                    Birth_Date = DateTime.Parse("1992-09-03"),
                    Program = json.Program,
                    Year = json.Year,
                    Semester = json.Semester,
                    Faculty = json.Faculty,
                    Course = json.Course,
                    Medical_Type = json.Medical_Type,
                    Available_Doctor = json.Doctor
                    //Birth_Date = DateTime.Parse(json.Birth_Date),
                    //Program = json.Program,
                    //Year = json.Year,
                    //Semester = json.Semester,
                    //Faculty = json.Faculty,
                    //Course = json.Course,
                    //Medical_Type = json.Medical_Type,
                    //Available_Doctor = json.Doctor
                
            };
            context.Appointments.Add(appointment);
            context.SaveChanges();

            if (json.Feelings != null)
            {
                foreach (var feeling in json.Feelings)
                {
                    var feelings =
                            new Feeling
                            {
                                FeelingBody = feeling,
                                AppointmentID = appointment.AppointmentID
                            };
                    context.Feelings.Add(feelings);
                    context.SaveChanges();
                }
            }

            if (json.Possible_Causes != null)
            {
                foreach (var cause in json.Possible_Causes)
                {
                    var causes =
                            new Possible_Cause
                            {
                                Possible_CauseBody = cause,
                                AppointmentID = appointment.AppointmentID
                            };
                    context.Possible_Causes.Add(causes);
                    context.SaveChanges();
                }
            }
            






            return Ok(appointment.AppointmentID);

        }
    }
}
