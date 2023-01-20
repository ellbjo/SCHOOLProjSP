using System;
using System.Collections.Generic;

namespace SCHOOLProj.Models
{
    public partial class Course
    {
        public int CourseId { get; set; }
        public string? Subject { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? FkPersonellId { get; set; }

        public virtual Personell? FkPersonell { get; set; }
    }
}
