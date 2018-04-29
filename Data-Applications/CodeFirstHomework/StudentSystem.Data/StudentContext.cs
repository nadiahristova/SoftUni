namespace StudentSystem.Data
{
    using StudentSystem.Data.Migrations;
    using StudentSystem.Model;
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Runtime.Remoting.Contexts;

    public class StudentContext : DbContext
    {
        public StudentContext()
            : base("name=StudentContext")
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<StudentContext, Configuration>();
            Database.SetInitializer(migrationStrategy);
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Homework> Homeworks { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().
             HasKey(c => c.Id);

            modelBuilder.Entity<Homework>().
             HasKey(h => h.Id);

            modelBuilder.Entity<Resource>().
             HasKey(r => r.Id);

            modelBuilder.Entity<Student>().
             HasKey(s => s.Id);

            modelBuilder.Entity<Course>().
             Property(c => c.Description).IsUnicode();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        } 
    }
}