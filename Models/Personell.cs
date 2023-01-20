using System;
using System.Collections.Generic;

namespace SCHOOLProj.Models
{
    public partial class Personell
    {
        public Personell()
        {
            Courses = new HashSet<Course>();
        }

        public int PersonellId { get; set; }
        public string? PersonellRole { get; set; }
        public string? PersonellFname { get; set; }
        public string? PersonellLname { get; set; }
        public DateTime? Startdate { get; set; }
        public int? Salary { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
