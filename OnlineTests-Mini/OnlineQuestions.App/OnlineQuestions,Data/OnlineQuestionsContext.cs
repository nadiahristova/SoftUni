namespace OnlineQuestions.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using OnlineQuestions.Data.Migrations;
    using OnlineQuestions.Models;
    using System.Data.Entity;

    public class OnlineQuestionsContext : IdentityDbContext<ApplicationUser>
    {
        // Your context has been configured to use a 'OnlineQuestionsContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'OnlineQuestions.Data.OnlineQuestionsContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'OnlineQuestionsContext' 
        // connection string in the application configuration file.
        public OnlineQuestionsContext()
            : base("OnlineQuestions", throwIfV1Schema: false)
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<OnlineQuestionsContext, Configuration>();
            Database.SetInitializer(migrationStrategy);
        }

        // will be needed bevcause of the customization of the user
        public static OnlineQuestionsContext Create()
        {
            return new OnlineQuestionsContext();
        }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<UserDoesTest> UsersDoTests { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>()
               .HasMany(t => t.Questions)
               .WithMany(q => q.PresentInTests)
               .Map(m =>
               {
                   m.MapLeftKey("TestId");
                   m.MapRightKey("QuestionId");
                   m.ToTable("TestHasQuestion");
               });

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithRequired(a => a.Question);
        }
    }
}