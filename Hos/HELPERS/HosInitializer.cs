using Hos.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hos.HELPERS
{
    public class HosInitializer : DropCreateDatabaseAlways<HosContext>  //DropCreateDatabaseAlways //DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(HosContext context)
        {
            var hasher = new PasswordHasher();
            var user = new UserManager<UserProfile>(new UserStore<UserProfile>(context));
            user.UserValidator = new UserValidator<UserProfile>(user) { AllowOnlyAlphanumericUserNames = false };
            user.Create(new UserProfile() { UserName = "IN16/20034/13", Email = "jj@gmail.com", PasswordHash = hasher.HashPassword("1234567890") });
            user.Create(new UserProfile() { UserName = "IN162006413", Email = "kk@gmail.com", PasswordHash = hasher.HashPassword("1234567890") });
            
            /////////////////////////////////////////////////////////////////////////////
            var appointments = new List<Appointment> 
            { 
                new Appointment {
                    AppointmentID = 1,
                    Registration_Number = "IN17/2004/37",
                    Birth_Date = DateTime.Parse("1992-09-03"),
                    Faculty = "FIST",
                    Stream  = "Year 3, Semester 2",
                    Course = "Applied Computer Science",
                    Medical_Type = "General",
                    Available_Doctor = "Dr. Kamau"
                }
            };
            appointments.ForEach(s => context.Appointments.Add(s));
            context.SaveChanges();

            //////////////////////////////////////////////////////////////////////////////
            var feelings = new List<Feeling> 
            { 
                new Feeling {
                    FeelingBody = "dizzy",
                    AppointmentID = 1
                },
                new Feeling {
                    FeelingBody = "tired",
                    AppointmentID = 1
                },
                new Feeling {
                    FeelingBody = "sleepy",
                    AppointmentID = 1
                }
            };
            feelings.ForEach(s => context.Feelings.Add(s));
            context.SaveChanges();

            /////////////////////////////////////////////////////////////////////////////////
            var possible_Causes = new List<Possible_Cause> 
            { 
                new Possible_Cause {
                    Possible_CauseBody = "cold",
                    AppointmentID = 1
                },
                new Possible_Cause {
                    Possible_CauseBody = "maralia",
                    AppointmentID = 1
                },
                new Possible_Cause {
                    Possible_CauseBody = "flu",
                    AppointmentID = 1
                }
            };
            possible_Causes.ForEach(s => context.Possible_Causes.Add(s));
            context.SaveChanges();

            ////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////

            var optionsData = new List<OptionsData> 
            { 
                new OptionsData {
                    OptionsDataID = 1
                }
            };
            optionsData.ForEach(s => context.OptionsDatas.Add(s));
            context.SaveChanges();
            ///////////////////////////////////////////////////////////////////////////////////
            var faculty = new List<Faculty> 
            { 
                new Faculty {
                    Name = "FIST"
                },
                new Faculty {
                    Name = "SPAS"
                },
                new Faculty {
                    Name = "Commerce"
                },
                new Faculty {
                    Name = "Education"
                },
                new Faculty {
                    Name = "Agriculture"
                }
            };
            faculty.ForEach(s => context.Faculties.Add(s));
            context.SaveChanges();

            ////////////////////////////////////////////////////////////////////////////////
            var stream = new List<Stream> 
            { 
                new Stream {
                    Name = "Year 1, Semester 1"
                },
                new Stream {
                    Name = "Year 1, Semester 2"
                },
                new Stream {
                    Name = "Year 2, Semester 1"
                },
                new Stream {
                    Name = "Year 2, Semester 2"
                },
                new Stream {
                    Name = "Year 3, Semester 1"
                },
                new Stream {
                    Name = "Year 3, Semester 2"
                },
                new Stream {
                    Name = "Year 4, Semester 1"
                },
                new Stream {
                    Name = "Year 4, Semester 2"
                }
            };
            stream.ForEach(s => context.Streams.Add(s));
            context.SaveChanges();

            ///////////////////////////////////////////////////////////////////////////////////////////
            var course = new List<Course> 
            { 
                new Course {
                    Name = "Applied Computer Science",
                    FacultyID = 1
                },
                new Course {
                    Name = "Computer Science",
                    FacultyID = 3
                },
                new Course {
                    Name = "Softwre Engineering",
                    FacultyID = 3
                },
                new Course {
                    Name = "Bachelor of Commerce",
                    FacultyID = 2
                },
                new Course {
                    Name = "Bachelor of Education",
                    FacultyID = 4
                }
            };
            course.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            ///////////////////////////////////////////////////////////////////////////////////////////
            var medical_Type = new List<Medical_Type> 
            { 
                new Medical_Type {
                    Name = "Surgery"
                },
                new Medical_Type {
                    Name = "Immergency"
                },
                new Medical_Type {
                    Name = "Gynechology"
                },
                new Medical_Type {
                    Name = "E.N.T"
                },
                new Medical_Type {
                    Name = "Pharmacy"
                }
            };
            medical_Type.ForEach(s => context.Medical_Types.Add(s));
            context.SaveChanges();

            ////////////////////////////////////////////////////////////////////////////////////////////
            var available_Doctor = new List<Available_Doctor> 
            { 
                new Available_Doctor {
                    Name = "Dr. Kamau",
                    Medical_TypeID = 1
                },
                new Available_Doctor {
                    Name = "Dr. Nancy",
                    Medical_TypeID = 1
                },
                new Available_Doctor {
                    Name = "Miss Rebbecca",
                    Medical_TypeID = 3
                },
                new Available_Doctor {
                    Name = "Sister Moraa",
                    Medical_TypeID = 3
                },
                new Available_Doctor {
                    Name = "Winfrey",
                    Medical_TypeID = 4
                }
            };
            available_Doctor.ForEach(s => context.Available_Doctors.Add(s));
            context.SaveChanges();
        }
    }
}