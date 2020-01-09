using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project_new.Models
{
    public class MyCourses
    {
        [Key]
        [Required]
        public string coursename { get; set; }
        [Required]
        public string day { get; set; }
        [Required]
        public TimeSpan startHour { get; set; }
        [Required]
        public TimeSpan endHour { get; set; }
        [Required]
        public string Class { get; set; }

        public List<MyCourses> info { get; set; }
    }
}