namespace Twitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        private ICollection<User> favoredBy;
        private ICollection<User> reTweetedBy;

        private ICollection<Tweet> tweetReplys;
        private ICollection<Tweet> tweetMentions;

        private ICollection<Tag> tags;

        private ICollection<Report> reports;

        public Tweet()
        {
            this.favoredBy = new HashSet<User>();
            this.reTweetedBy = new HashSet<User>();

            this.tweetReplys = new HashSet<Tweet>();
            this.tweetMentions = new HashSet<Tweet>();

            this.tags = new HashSet<Tag>();

            this.reports = new HashSet<Report>();
        }

        [Key]
        public int Id { get; set; }

        //[Required]
        public DateTime CreatedAt { get; set; }

        //[Required]
        public string Text { get; set; }

        public string URL { get; set; }

        public virtual User Author { get; set; }
        public virtual Tweet InitialTweet { get; set; }

        public virtual ICollection<User> FavoredBy
        {
            get { return this.favoredBy; }
            set { this.favoredBy = value; }
        }
        public virtual ICollection<User> ReTweetedBy
        {
            get { return this.reTweetedBy; }
            set { this.reTweetedBy = value; }
        }

        public virtual ICollection<Tweet> TweetReplays
        {
            get { return this.tweetReplys; }
            set { this.tweetReplys = value; }
        }
        public virtual ICollection<Tweet> TweetMentions
        {
            get { return this.tweetMentions; }
            set { this.tweetMentions = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Report> Reports
        {
            get { return this.reports; }
            set { this.reports = value; }
        }
    }
}
