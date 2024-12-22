namespace MovieApp.Domain.Entity
{
    public class ListMovie
    {
        public int Id { get; set; }
        public int UserListId { get; set; }
        public int MovieId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual UserList UserList { get; set; }
    }


}