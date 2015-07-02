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
    public class HosInitializer : CreateDatabaseIfNotExists<HosContext>  //DropCreateDatabaseAlways //DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(HosContext context)
        {
            var hasher = new PasswordHasher();
            var user = new UserManager<UserProfile>(new UserStore<UserProfile>(context));
            user.UserValidator = new UserValidator<UserProfile>(user) { AllowOnlyAlphanumericUserNames = false };

            user.Create(new UserProfile() { FirstName = "makori", LastName = "m", SurName="mokaya", UserName = "Admin", RoleName = "ADMIN", PasswordHash = hasher.HashPassword("Admin") });




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
        }
    }
}