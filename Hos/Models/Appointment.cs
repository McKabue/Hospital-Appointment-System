using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public virtual IList<Feeling> Feelings { get; set; }
        public virtual IList<Possible_Cause> Possible_Causes { get; set; }
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
        public int OptionsDataID { get; set; }
        public virtual IList<UserProfile> User { get; set; }
        public virtual IList<Faculty> Faculties { get; set; }
        public virtual IList<Program> Programs { get; set; }
        public virtual IList<Medical_Type> Medical_Types { get; set; }
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
        public string Name { get; set; }
        public string Doctor_UserName { get; set; }
        public int Medical_TypeID { get; set; }
    }
}