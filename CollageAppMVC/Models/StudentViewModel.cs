using System;
using System.ComponentModel.DataAnnotations;

namespace CollageAppMVC.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        [Display(Name = "Deparment")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [Required]
        [Display(Name = "Section")]
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        [Required]
        [Display(Name = "Student")]
        public string StudentName { get; set; }
        [Display(Name = "Date Of Join")]
        public DateTime? DateOfJoin { get; set; }
        [Display(Name = "Date of Graduaton")]
        public DateTime? DateofGraduaton { get; set; }
    }
}