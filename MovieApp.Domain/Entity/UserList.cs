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


}