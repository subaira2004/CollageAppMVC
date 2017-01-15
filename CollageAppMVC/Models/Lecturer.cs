namespace CollageAppMVC.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Lecturer
    {
        public int LecturerId { get; set; }
        [Display(Name="Department")]
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Department Department { get; set; }
    }
}
