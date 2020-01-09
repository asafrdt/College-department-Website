using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_new.Models
{
    public class StudentInCourse

    {

        [Key]
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int Grade { get; set; }

        public List<StudentInCourse> StudentInCourseinfo { get; set; }

        public virtual Student Student { get; set; }

        public virtual Courses Course { get; set; }

    }
}