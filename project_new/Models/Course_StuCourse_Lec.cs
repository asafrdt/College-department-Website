using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project_new.Models
{
    public class Course_StuCourse_Lec
    {
        [Key]
        [Required]
        public string coursename { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public TimeSpan startHour { get; set; }
        [Required]
        public TimeSpan endHour { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public string FirstNameLec { get; set; }
        [Required]
        public string LastNameLec { get; set; }
        [Required]
        public int sicStudentId { get; set; }
        [Required]
        public int LecturerId { get; set; }
        [Required]
        public int liLecturerId { get; set; }
        [Required]
        public DateTime? moedA { get; set; }
        [Required]
        public string classA { get; set; }
        [Required]
        public DateTime? moedB { get; set; }
        [Required]
        public string classB { get; set; }

        [Required]
        public int? GradeA { get; set; }
        [Required]
        public int? GradeB { get; set; }


    }
}