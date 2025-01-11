using Microsoft.AspNetCore.Identity;

namespace MovieApp.Domain.Entity
{

    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserList>? UserLists { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }


}