using Microsoft.AspNetCore.Identity;

namespace MovieApp.Domain.Entity
{

    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserList>? UserLists { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }


}