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
    public class HosInitializer : DropCreateDatabaseIfModelChanges<HosContext>  //DropCreateDatabaseAlways //DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(HosContext context)
        {
            var hasher = new PasswordHasher();
            var user = new UserManager<UserProfile>(new UserStore<UserProfile>(context));
            user.UserValidator = new UserValidator<UserProfile>(user) { AllowOnlyAlphanumericUserNames = false };
            user.Create(new UserProfile() { FirstName = "kamaa", LastName = "john", UserName = "IN16/20034/13", RoleName = "STUDENT", PasswordHash = hasher.HashPassword("1234567890") });
            user.Create(new UserProfile() { FirstName = "kamaa2", LastName = "john2", UserName = "IN162006413", RoleName = "STUDENT", PasswordHash = hasher.HashPassword("1234567890") });

            user.Create(new UserProfile() { FirstName = "Dr. Kamau", LastName = "john2", UserName = "Dr.Kamau", RoleName = "DOCTOR", PasswordHash = hasher.HashPassword("1234567890") });
            user.Create(new UserProfile() { FirstName = "Dr. Nancy", LastName = "john2", UserName = "Dr.Nancy", RoleName = "DOCTOR", PasswordHash = hasher.HashPassword("1234567890") });
            user.Create(new UserProfile() { FirstName = "Miss Rebecca", LastName = "john2", UserName = "MissRebecca", RoleName = "DOCTOR", PasswordHash = hasher.HashPassword("1234567890") });
            user.Create(new UserProfile() { FirstName = "sister Moraa", LastName = "john2", UserName = "sisterMoraa", RoleName = "DOCTOR", PasswordHash = hasher.HashPassword("1234567890") });
            user.Create(new UserProfile() { FirstName = "kamaa Again", LastName = "john2", UserName = "kamaaAgain", RoleName = "DOCTOR", PasswordHash = hasher.HashPassword("1234567890") });
            
            /////////////////////////////////////////////////////////////////////////////
            var appointments = new List<Appointment> 
            { 
                new Appointment {
                    AppointmentID = 1,
                    Registration_Number = "IN17/2004/37",
                    Birth_Date = DateTime.Parse("1992-09-03"),
                    Faculty = "FIST",
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

            ////////////////////////////////////////////////////////////////////////////1
            var programs = new List<Program> 
            { 
                new Program {
                    ProgramID = 1,
                    Name = "JAB"
                },
                new Program {
                    ProgramID = 2,
                    Name = "SSP"
                }
            };
            programs.ForEach(s => context.Programs.Add(s));
            context.SaveChanges();

            ////////////////////////////////////////////////////////////////////////////2
            var years = new List<Year> 
            { 
                new Year {
                    YearID = 1,
                    Name = "ONE",
                    ProgramID = 1
                },
                new Year {
                    YearID = 2,
                    Name = "TWO",
                    ProgramID = 1
                },
                new Year {
                    YearID = 3,
                    Name = "THREE",
                    ProgramID = 1
                },
                new Year {
                    YearID = 4,
                    Name = "FOUR",
                    ProgramID = 1
                },
                new Year {
                    YearID = 5,
                    Name = "FIVE",
                    ProgramID = 1
                },


                new Year {
                    YearID = 6,
                    Name = "ONE",
                    ProgramID = 2
                },
                new Year {
                    YearID = 7,
                    Name = "TWO",
                    ProgramID = 2
                },
                new Year {
                    YearID = 8,
                    Name = "THREE",
                    ProgramID = 2
                },
                new Year {
                    YearID = 9,
                    Name = "FOUR",
                    ProgramID = 2
                },
                new Year {
                    YearID = 10,
                    Name = "FIVE",
                    ProgramID = 2
                }
            };
            years.ForEach(s => context.Years.Add(s));
            context.SaveChanges();

            ////////////////////////////////////////////////////////////////////////////3
            var semesters = new List<Semester> 
            { 
                new Semester {
                    Name = "Semester 1",
                    YearID = 1
                },
                new Semester {
                    Name = "Semester 2",
                    YearID = 1
                },
                new Semester {
                    Name = "Semester 1",
                    YearID = 2
                },
                new Semester {
                    Name = "Semester 2",
                    YearID = 2
                },
                new Semester {
                    Name = "Semester 1",
                    YearID = 3
                },
                new Semester {
                    Name = "Semester 2",
                    YearID = 3
                },
                new Semester {
                    Name = "Semester 1",
                    YearID = 4
                },
                new Semester {
                    Name = "Semester 2",
                    YearID = 4
                },
                new Semester {
                    Name = "Semester 1",
                    YearID = 5
                },
                new Semester {
                    Name = "Semester 2",
                    YearID = 5
                },



                new Semester {
                    Name = "Semester 1",
                    YearID = 6
                },
                new Semester {
                    Name = "Semester 2",
                    YearID = 6
                },
                new Semester {
                    Name = "Semester 1",
                    YearID = 7
                },
                new Semester {
                    Name = "Semester 2",
                    YearID = 7
                },
                new Semester {
                    Name = "Semester 1",
                    YearID = 8
                },
                new Semester {
                    Name = "Semester 2",
                    YearID = 8
                },
                new Semester {
                    Name = "Semester 1",
                    YearID = 9
                },
                new Semester {
                    Name = "Semester 2",
                    YearID = 9
                },
                new Semester {
                    Name = "Semester 1",
                    YearID = 10
                },
                new Semester {
                    Name = "Semester 2",
                    YearID = 10
                }
            };
            semesters.ForEach(s => context.Semesters.Add(s));
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
                    Doctor_UserName = "Dr.Kamau",
                    Medical_TypeID = 1
                },
                new Available_Doctor {
                    Name = "Dr. Nancy",
                    Doctor_UserName = "Dr.Nancy",
                    Medical_TypeID = 1
                },
                new Available_Doctor {
                    Name = "Miss Rebbecca",
                    Doctor_UserName = "MissRebbecca",
                    Medical_TypeID = 3
                },
                new Available_Doctor {
                    Name = "Sister Moraa",
                    Doctor_UserName = "sisterMoraa",
                    Medical_TypeID = 3
                },
                new Available_Doctor {
                    Name = "Dr. Kamau A",
                    Doctor_UserName = "kamaaAgain",
                    Medical_TypeID = 4
                }
            };
            available_Doctor.ForEach(s => context.Available_Doctors.Add(s));
            context.SaveChanges();
        }
    }
}