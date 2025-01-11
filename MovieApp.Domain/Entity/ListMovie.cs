namespace MovieApp.Domain.Entity
{
    public class ListMovie : BaseEntity
    {
        public long UserListId { get; set; }
        public int MovieId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual UserList UserList { get; set; }
    }


}