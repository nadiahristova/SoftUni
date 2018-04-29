namespace Twitter.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;
    using Migrations;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class TwitterContext : IdentityDbContext<User>, ITwitterContext
    {
        public TwitterContext()
            : base("TwitterContext", throwIfV1Schema: false)
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<TwitterContext, Configuration>();
            Database.SetInitializer(migrationStrategy);
        }

        public static TwitterContext Create()
        {
            return new TwitterContext();
        }
     
        public virtual DbSet<Tweet> Tweets { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<Report> Reports { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followers)
                .WithMany(f => f.Following)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("FollowerId");
                    m.ToTable("Followers");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.MyTweets)
                .WithRequired(t => t.Author);

            modelBuilder.Entity<User>()
                .HasMany(u => u.MyFavoriteTweets)
                .WithMany(t => t.FavoredBy)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("FavoriteTweetId");
                    m.ToTable("FavoriteTweets");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReTweets)
                .WithMany(t => t.ReTweetedBy)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("TweetId");
                    m.ToTable("ReTweets");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.SentMessages)
                .WithRequired(t => t.Sender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.RecievedMessages)
                .WithRequired(t => t.Recipient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReportsSent)
                .WithRequired(r => r.ReportedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tweet>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.Tweets)
                .Map(m =>
                 {
                     m.MapLeftKey("TagId");
                     m.MapRightKey("TweetId");
                     m.ToTable("TweetWithTags");
                 });

            modelBuilder.Entity<Tweet>()
               .HasMany(t => t.TweetMentions)
               .WithMany()
               .Map(m =>
               {
                   m.MapLeftKey("LeftTweetId");
                   m.MapRightKey("RightTweetId");
                   m.ToTable("TweetMentions");
               });

            modelBuilder.Entity<Tweet>()
                .HasMany(t => t.TweetReplays)
                .WithOptional(t => t.InitialTweet);

            modelBuilder.Entity<Tweet>()
                .HasMany(t => t.Reports)
                .WithRequired(r => r.ReportedTweet)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}