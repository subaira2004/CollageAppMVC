namespace CollageAppMVC.Models
{
    using System.Data.Entity;

    public partial class CollageAppEDM : DbContext
    {
        public CollageAppEDM()
            : base("name=CollageAppEDM")
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DeptSection> DeptSections { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeptSection>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.DeptSection)
                .HasForeignKey(e => e.DeptSectionId);
        }
    }
}
