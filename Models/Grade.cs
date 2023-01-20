using System;
using System.Collections.Generic;

namespace SCHOOLProj.Models
{
    public partial class Grade
    {
        public int? GradeId { get; set; }
        public int? FkStudentId { get; set; }
        public int? FkCourseId { get; set; }
        public int? Grade1 { get; set; }
        public DateTime? SetDate { get; set; }

        public virtual Course? FkCourse { get; set; }
        public virtual Student? FkStudent { get; set; }
    }
}
