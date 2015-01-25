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
    public class HosInitializer : DropCreateDatabaseAlways<HosContext>
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

        }
    }
}