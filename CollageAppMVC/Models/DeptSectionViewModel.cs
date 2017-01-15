using System.ComponentModel.DataAnnotations;

namespace CollageAppMVC.Models
{
    public class DeptSectionViewModel
    {
        public int SectionId { get; set; }
        [Required]
        public string SectionName { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}