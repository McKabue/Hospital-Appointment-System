﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hos.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Registration_Number { get; set; }
        public DateTime? Birth_Date { get; set; }

        public string Program { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }

        public string Faculty { get; set; }
        public string Course { get; set; }
        public string Medical_Type { get; set; }
        public string Available_Doctor { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status? Status { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }
        public virtual IList<Feeling> Feelings { get; set; }
        public virtual IList<Possible_Cause> Possible_Causes { get; set; }
    }

    public enum Status
    {
        Not_Confirmed, Confirmed, Treated, Postponed
    }

    public class Feeling
    {
        public int FeelingID { get; set; }
        public string FeelingBody { get; set; }
        public int AppointmentID { get; set; }
    }

    public class Possible_Cause
    {
        public int Possible_CauseID { get; set; }
        public string Possible_CauseBody { get; set; }
        public int AppointmentID { get; set; }
    }







    public class OptionsData
    {
        public UserModel User { get; set; }
        public IList<Faculty> Faculties { get; set; }
        public IList<Program> Programs { get; set; }
        public IList<Medical_Type> Medical_Types { get; set; }
    }
   

    /*public class Stream
    {
        public int StreamID { get; set; }
        public string Name { get; set; }
    }*/

    public class Program
    {
        public int ProgramID { get; set; }
        public string Name { get; set; }
        public virtual IList<Year> Years { get; set; }
    }

    public class Year
    {
        public int YearID { get; set; }
        public string Name { get; set; }
        public int ProgramID { get; set; }
        public virtual IList<Semester> Semesters { get; set; }
    }

    public class Semester
    {
        public int SemesterID { get; set; }
        public string Name { get; set; }
        public int YearID { get; set; }
    }





    public class Faculty
    {
        public int FacultyID { get; set; }
        public string Name { get; set; }
        
        public virtual IList<Course> Courses { get; set; }
    }

    public class Course
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        
        public int FacultyID { get; set; }
        [JsonIgnore]
        [ForeignKey("FacultyID")]
        public virtual Faculty Faculty { get; set; }
    }

    public class Medical_Type
    {
        public int Medical_TypeID { get; set; }
        public string Name { get; set; }
        public virtual IList<Available_Doctor> Available_Doctors { get; set; }
    }

    public class Available_Doctor
    {
        public int Available_DoctorID { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        public int Medical_TypeID { get; set; }
    }
}