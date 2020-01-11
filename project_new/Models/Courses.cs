using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace project_new.Models
{
    public class Courses
    {
        [Key]
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int LecturerId { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public TimeSpan startHour { get; set; }

        [Required]
        public TimeSpan endHour { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public string classA { get; set; }

        [Required]
        public string classB { get; set; }

        public List<Courses> info { get; set; }

    }
}
