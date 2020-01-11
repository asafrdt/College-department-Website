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
        [Required]
        public int moedAYear { get; set; }
        [Required]
        public int moedAMonth { get; set; }
        [Required]
        public int moedADay { get; set; }
        [Required]
        public string classA { get; set; }
        [Required]
        public int moedBYear { get; set; }
        [Required]
        public int moedBMonth { get; set; }
        [Required]
        public int moedBDay { get; set; }
        [Required]
        public string classB { get; set; }

        public List<MyCourses> info { get; set; }
    }
}