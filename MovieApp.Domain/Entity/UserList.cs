namespace MovieApp.Domain.Entity
{
    public class UserList : BaseEntity
    {
        public string Title { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ListMovie> ListMovies { get; set; }
    }


}