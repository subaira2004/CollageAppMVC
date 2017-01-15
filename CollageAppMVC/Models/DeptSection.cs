namespace CollageAppMVC.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DeptSection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeptSection()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public int SectionId { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
