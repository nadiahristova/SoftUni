using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineQuestions.Models
{
    public class UserDoesTest
    {
    #region Composite Primary Key
        [Key, Column(Order = 0)]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Key, Column(Order = 1)]
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
    #endregion

        public byte? RightAnswersCount { get; set; }
    }
}
