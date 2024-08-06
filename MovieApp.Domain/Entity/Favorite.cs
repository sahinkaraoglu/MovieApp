using Microsoft.AspNetCore.Identity;

namespace MovieApp.Domain.Entity
{

    public class UserList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ListMovie>? ListMovies { get; set; }
    }

    public class ListMovie
    {
        public int Id { get; set; }
        public int UserListId { get; set; }
        public int MovieId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual UserList UserList { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserList>? UserLists { get; set; }
    }


}