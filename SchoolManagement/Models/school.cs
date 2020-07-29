using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class school
    {
        [Key]
        public int SchoolId { get; set; }

        [Required(ErrorMessage = "Please Provide School Name")]
       // [MaxLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Select School Type")]
        [Display(Name = "Type of school")]
        public string SchoolType { get; set; }

        [Required(ErrorMessage = "Please Provide Online classes")]
       // [Display(Name = "Open for Online Classes?")]
        public bool OnlineClasses { get; set; }

        [Required(ErrorMessage = "Please Provide Valid Date")]
        [DataType(DataType.Date)]
       // [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        public DateTime LaunchDate { get; set; }
    }
}