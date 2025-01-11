using Microsoft.AspNetCore.Identity;

namespace MovieApp.Domain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserList>? UserLists { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
} 