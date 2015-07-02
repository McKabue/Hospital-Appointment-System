using Hos.HELPERS;
using Hos.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.Results;

namespace Hos.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Appointment")]
    public class AppointmentController : SignalrBaseWebApiController<hosHub>
    {
        private HosContext context = new HosContext();
        private AuthRepo _repo = null;
        public AppointmentController()
        {
            _repo = new AuthRepo();
        }


        [AllowAnonymous]
        //[OverrideAuthorization]
        [Route("Data")]
        public async Task<IHttpActionResult> Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                //var roleName = "STUDENT";
                var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
                var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();

                if (roleName == "STUDENT")
                {

                    var result = new OptionsData
                                  {
                                      User = (from user in context.Users.Where(c => c.UserName == User.Identity.Name)
                                              select new UserModel()
                                              { 
                                                 UserName = user.UserName,
                                                 FirstName = user.FirstName,
                                                 LastName = user.LastName,
                                                 SurName = user.SurName,
                                                 National_ID_Number = user.National_ID_Number
                                             }).FirstOrDefault(),
                                      Faculties = context.Faculties.ToList(),
                                      Programs = context.Programs.Include(y => y.Years).ToList(),
                                      Medical_Types = (from mt in context.Medical_Types.ToList()
                                                      select new Medical_Type()
                                                      {
                                                          Name = mt.Name,
                                                          Available_Doctors = (from ad in context.Available_Doctors.Where(adid => adid.Medical_TypeID == mt.Medical_TypeID).ToList()
                                                                              select new Available_Doctor()
                                                                              {
                                                                                  UserName =  context.Users.Where(id => id.Id == ad.UserId).SingleOrDefault().UserName  // "No Name"
                                                                              }).ToList()
                                                      }).ToList()
                                  };

                    return Ok(result);
                }
                else
                {
                    return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)666, new HttpError("You are a Doctor ot Admin")));
                }
            }

            return BadRequest();
            
        }

        [Authorize]
        [Route("AppointmentData/Save")]
        public async Task<IHttpActionResult> PostAppointment(JObject data)
        {
            

            if (User.Identity.IsAuthenticated)
            {
                var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
                var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
                if (roleName == "DOCTOR")
                {
                    return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)666, new HttpError("You are a Doctor")));
                }

                



                dynamic json = data;
                DateTime date = json.Birth_Date;

                var appointment = new Appointment
                {
                    AppointmentDate = DateTime.UtcNow,
                    Registration_Number = json.Registration_Number,
                    Birth_Date = date,
                    Program = json.Program,
                    Year = json.Year,
                    Semester = json.Semester,
                    Faculty = json.Faculty,
                    Course = json.Course,
                    Medical_Type = json.Medical_Type,
                    Available_Doctor = json.Doctor,
                    Status = Status.Not_Confirmed
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
                await context.SaveChangesAsync();

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
                        await context.SaveChangesAsync();
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
                        await context.SaveChangesAsync();
                    }
                }

                return Ok(appointment.AppointmentID);

            }
            return Unauthorized();
        }


        [HttpGet]
        [Authorize]
        [Route("courses")]
        public async Task<IHttpActionResult> Courses()
        {
                var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
                var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
                if (roleName == "ADMIN")
                {
                    var courses = context.Faculties.ToList();

                    return Ok(courses);
                }
            return BadRequest();
        }


        [HttpPost]
        [Authorize]
        [Route("falculty")]
        public async Task<IHttpActionResult> Falculty(JObject data)
        {

            dynamic json = data;
            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
            if (roleName == "ADMIN")
            {
                var course = context.Faculties.Add(new Faculty
                {
                    Name = json.falculty
                });

                await context.SaveChangesAsync();

                return Ok(course);
            }
            return BadRequest();
        }




        [HttpPost]
        [Authorize]
        [Route("medicaltype")]
        public async Task<IHttpActionResult> Medical_Type(JObject data)
        {

            dynamic json = data;
            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
            if (roleName == "ADMIN")
            {
                var medical_Type = context.Medical_Types.Add(new Medical_Type
                {
                    Name = json.medicaltype
                });

                await context.SaveChangesAsync();

                return Ok(medical_Type);
            }
            return BadRequest();
        }



        [HttpPost]
        [Authorize]
        [Route("course")]
        public async Task<IHttpActionResult> Course(JObject data)
        {

            dynamic json = data;

            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
            if (roleName == "ADMIN")
            {
                var course = context.Courses.Add(new Course
                {
                    Name = json.course,
                    FacultyID = json.FacultyID
                });

                await context.SaveChangesAsync();

                Hub.Clients.All.newCourse(course);
                return Ok(course);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("download/{appointmentid}")]
        public async void Get(string appointmentid)
        {
            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();

            if (roleName == "STUDENT")
            {
                var user = await _repo.findUserAsync(User.Identity.Name);
                var appointment = context.Appointments.FindAsync(appointmentid);

                var doc = new Document(PageSize.A4, 5, 5, 110, 60);
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);

                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                iTextSharp.text.Font times = new Font(bfTimes, 12, Font.ITALIC, BaseColor.RED);
                iTextSharp.text.Font bolder = new Font(bfTimes, 50, Font.BOLD, BaseColor.BLACK);


                doc.Open();

                var phrase = new Phrase();
                phrase.Add(new Chunk(user.UserName + "made an appointment " + appointment.Id));

                




                PdfPTable table = new PdfPTable(2);
                
                float[] widths = new float[] { 2f, 3f };
                table.SetWidths(widths);

                PdfPCell cell = new PdfPCell(new Phrase("Kisii University Annex", bolder));
                //cell.Width = doc.PageSize.Width;
                cell.Border = 0;
                cell.Colspan = 2;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                table.AddCell(cell);

                table.AddCell("Registration Number"); table.AddCell(user.UserName);

                table.AddCell("Full Name"); table.AddCell(user.FirstName + " " + user.LastName + " " + user.SurName);

                doc.Add(table);



                doc.Close();

                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + user.UserName + ".pdf\"");
                HttpContext.Current.Response.ContentType = "Application/pdf";
                HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }

            //return BadRequest("This Appointment Belongs to a user, and only the user can download it...Please!");
        }



        [Authorize]
        [Route("Details")]
        //[EnableQuery]
        [CustomQueryable]
        public async Task<IHttpActionResult> GetDetails()
        {
            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();

            if (roleName == "STUDENT")
            {
                var result = (from appointment in context.Appointments.Where(c => c.Registration_Number == User.Identity.Name).ToList()
                              select new
                              {
                                  User = from user in context.Users.Where(c => c.UserName == User.Identity.Name).ToList()
                                         select new
                                         {
                                             SurName = user.SurName,
                                             FirstName = user.FirstName,
                                             LastName = user.LastName,
                                             National_ID_Number = user.National_ID_Number,
                                             Registration_Number = user.UserName
                                         },
                                  RoleName = roleName,
                                  AppointmentID = appointment.AppointmentID,
                                  Birth_Date = appointment.Birth_Date,
                                  Program = appointment.Program,
                                  Year = appointment.Year,
                                  Semester = appointment.Semester,
                                  Faculty = appointment.Faculty,
                                  Course = appointment.Course,
                                  Feelings = from feeling in context.Feelings.Where(c => c.AppointmentID == appointment.AppointmentID).ToList()
                                             select new
                                             {
                                                 FeelingBody = feeling.FeelingBody
                                             },
                                  Causes = from cause in context.Possible_Causes.Where(c => c.AppointmentID == appointment.AppointmentID).ToList()
                                           select new
                                           {
                                               CauseBody = cause.Possible_CauseBody
                                           },
                                  Medical_Type = appointment.Medical_Type,
                                  Doctor = appointment.Available_Doctor,

                                  DateTime = appointment.AppointmentDate,
                                  Status = appointment.Status.ToString()




                              }).AsQueryable();
                return Ok(result);
            }

            if (roleName == "DOCTOR")
            {
                var result = (from appointment in context.Appointments.ToList()
                              select new
                              {
                                  User = from user in context.Users.Where(c => c.UserName == appointment.Registration_Number).ToList()
                                         select new
                                         {
                                             SurName = user.SurName,
                                             FirstName = user.FirstName,
                                             LastName = user.LastName,
                                             National_ID_Number = user.National_ID_Number,
                                             Registration_Number = user.UserName
                                             //RoleName = user.RoleName
                                         },

                                  RoleName = roleName,
                                  AppointmentID = appointment.AppointmentID,
                                  Birth_Date = appointment.Birth_Date,
                                  Program = appointment.Program,
                                  Year = appointment.Year,
                                  Semester = appointment.Semester,
                                  Faculty = appointment.Faculty,
                                  Course = appointment.Course,
                                  Feelings = from feeling in context.Feelings.Where(c => c.AppointmentID == appointment.AppointmentID).ToList()
                                             select new
                                             {
                                                 FeelingBody = feeling.FeelingBody
                                             },
                                  Causes = from cause in context.Possible_Causes.Where(c => c.AppointmentID == appointment.AppointmentID).ToList()
                                           select new
                                           {
                                               CauseBody = cause.Possible_CauseBody
                                           },
                                  Medical_Type = appointment.Medical_Type,
                                  Doctor = appointment.Available_Doctor,

                                  DateTime = appointment.AppointmentDate,
                                  Status = appointment.Status.ToString()


                              }).AsQueryable();
                return Ok(result);
            }

            if (roleName == "ADMIN")
            {
                var result = (from appointment in context.Appointments.ToList()
                              select new
                              {
                                  User = from user in context.Users.Where(c => c.UserName == appointment.Registration_Number).ToList()
                                         select new
                                         {
                                             SurName = user.SurName,
                                             FirstName = user.FirstName,
                                             LastName = user.LastName,
                                             National_ID_Number = user.National_ID_Number,
                                             Registration_Number = user.UserName
                                             //RoleName = user.RoleName
                                         },

                                  RoleName = roleName,
                                  AppointmentID = appointment.AppointmentID,
                                  Birth_Date = appointment.Birth_Date,
                                  Program = appointment.Program,
                                  Year = appointment.Year,
                                  Semester = appointment.Semester,
                                  Faculty = appointment.Faculty,
                                  Course = appointment.Course,
                                  Feelings = from feeling in context.Feelings.Where(c => c.AppointmentID == appointment.AppointmentID).ToList()
                                             select new
                                             {
                                                 FeelingBody = feeling.FeelingBody
                                             },
                                  Causes = from cause in context.Possible_Causes.Where(c => c.AppointmentID == appointment.AppointmentID).ToList()
                                           select new
                                           {
                                               CauseBody = cause.Possible_CauseBody
                                           },
                                  Medical_Type = appointment.Medical_Type,
                                  Doctor = appointment.Available_Doctor,

                                  DateTime = appointment.AppointmentDate,
                                  Status = appointment.Status.ToString()


                              }).AsQueryable();
                return Ok(result);
            }

            else
            {
                return NotFound();
                //return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)401, new HttpError("Yoe need to re-login again")));
                //return Ok(statusCode(401));
            }
        }


        [Authorize]
        [Route("Status")]
       // [AcceptVerbs("PATCH")]
        public async Task<IHttpActionResult> Put(int key, Appointment newAppointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var appointment = await context.Appointments.FindAsync(key);
            if (appointment == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //newAppointment.Patch(appointment);
            appointment.Status = newAppointment.Status;
            await context.SaveChangesAsync();


            Hub.Clients.All.changedStatus(key, appointment.Status.ToString(), appointment.Available_Doctor);
            return Content(HttpStatusCode.PartialContent, appointment.Status.ToString());
            //return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)333, new HttpError("Status Modified")));
        }


        [Authorize]
        [Route("Delete")]
        public async Task<IHttpActionResult> Delete(int key)
        {
            var entity = context.Appointments.SingleOrDefault(t => t.AppointmentID == key);
            context.Appointments.Remove(entity);
            context.SaveChanges();

            Hub.Clients.All.deleteAppointment(key);
            return Content(HttpStatusCode.NoContent, "Deleted");
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteFalculty")]
        public async Task<IHttpActionResult> deleteFalculty(int id)
        {
            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
            if (roleName == "ADMIN")
            {
                var f = context.Faculties.Find(id);

                if (f == null)
                {
                    return NotFound();
                }
                context.Faculties.Remove(f);

                await context.SaveChangesAsync();

                Hub.Clients.All.deleteFalculty(id);
                return Ok();
            }
            return BadRequest();
        }


        [HttpPut]
        [Authorize]
        [Route("acceptCourseEdit")]
        public async Task<IHttpActionResult> acceptCourseEdit(JObject data)
        {
            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
            if (roleName == "ADMIN")
            {
                dynamic json = data;

                int id = json.id;

                var c = context.Courses.Find(id);

                if (c == null)
                {
                    return NotFound();
                }
                c.Name = json.Name;

                await context.SaveChangesAsync();

                Hub.Clients.All.acceptCourseEdit(c.FacultyID, json.id, json.Name);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteCourse")]
        public async Task<IHttpActionResult> deleteCourse(int id)
        {
            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
            if (roleName == "ADMIN")
            {
                var c = context.Courses.Find(id);

                if (c == null)
                {
                    return NotFound();
                }
                context.Courses.Remove(c);

                await context.SaveChangesAsync();


                Hub.Clients.All.deleteCourse(c.FacultyID, id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        [Route("chooseDoctor")]
        public async Task<IHttpActionResult> chooseDoctor()
        {
            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
            if (roleName == "ADMIN")
            {
                var d = from u in context.Users.Where(u => u.RoleName == "DOCTOR").ToList()
                        select new
                        {
                            UserName = u.UserName
                        };

                return Ok(d);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteDoctor")]
        public async Task<IHttpActionResult> deleteDoctor(int id)
        {
            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
            if (roleName == "ADMIN")
            {
                var ad = context.Available_Doctors.Find(id);

                if (ad == null)
                {
                    return NotFound();
                }
                context.Available_Doctors.Remove(ad);

                await context.SaveChangesAsync();


                Hub.Clients.All.deleteDoctor(ad.Medical_TypeID, id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteMedical_Type")]
        public async Task<IHttpActionResult> deleteMedical_Type(int id)
        {
            var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
            var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
            if (roleName == "ADMIN")
            {
                var mt = context.Medical_Types.Find(id);

                if (mt == null)
                {
                    return NotFound();
                }
                context.Medical_Types.Remove(mt);

                await context.SaveChangesAsync();

                Hub.Clients.All.deleteMedical_Type(id);
                return Ok();
            }
            return BadRequest();
        }




        
    }
}
