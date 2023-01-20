using System;
using System.Collections.Generic;

namespace SCHOOLProj.Models
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public string? StudentFname { get; set; }
        public string? StudentLname { get; set; }
        public DateTime? Dob { get; set; }
    }
}
