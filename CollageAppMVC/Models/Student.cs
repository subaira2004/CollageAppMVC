namespace CollageAppMVC.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Student
    {
        public int StudentId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateOfJoin { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateofGraduaton { get; set; }

        public int? DeptSectionId { get; set; }

        [Required]
        public string Name { get; set; }

        public int SectionId { get; set; }

        public virtual DeptSection DeptSection { get; set; }
    }
}
