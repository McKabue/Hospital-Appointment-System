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
        public string Registration_Number { get; set; }
        public DateTime Birth_Date { get; set; }
        public string Faculty { get; set; }
        public string Stream { get; set; }
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
        public virtual IList<Faculty> Faculties { get; set; }
        public virtual IList<Stream> Streams { get; set; }
        public virtual IList<Course> Courses { get; set; }
        public virtual IList<Medical_Type> Medical_Types { get; set; }
        public virtual IList<Available_Doctor> Available_Doctors { get; set; }
    }

    public class Faculty
    {
        public int FacultyID { get; set; }
        public string Name { get; set; }
    }

    public class Stream
    {
        public int StreamID { get; set; }
    }

    public class Course
    {
        public int CourseID { get; set; }
    }

    public class Medical_Type
    {
        public int Medical_TypeID { get; set; }
    }

    public class Available_Doctor
    {
        public int Available_DoctorID { get; set; }
    }
}