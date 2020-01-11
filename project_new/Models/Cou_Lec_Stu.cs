using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project_new.Models
{
    public class Cou_Lec_Stu
    {
        [Key]
        [Required]
        public string coursename { get; set; }
        [Required]
        
        public string StudentId { get; set; }
        [Required]
        public string FirstNameStu { get; set; }
        [Required]
        public string LastNameStu { get; set; }
        [Required]
        public int? GradeA { get; set; }
        [Required]
        public int? GradeB { get; set; }

        public List<Cou_Lec_Stu> info { get; set; }
   
    }
}