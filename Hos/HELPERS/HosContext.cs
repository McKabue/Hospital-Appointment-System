using Hos.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hos.HELPERS
{
    public class HosContext : IdentityDbContext<UserProfile>
    {
        public HosContext() : base("HosContext") { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Feeling> Feelings { get; set; }
        public DbSet<Possible_Cause> Possible_Causes { get; set; }



        public DbSet<OptionsData> OptionsDatas { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Medical_Type> Medical_Types { get; set; }
        public DbSet<Available_Doctor> Available_Doctors { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<UserProfile>().ToTable("Users");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
        }
    }
}