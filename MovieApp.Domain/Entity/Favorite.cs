namespace MovieApp.Domain.Entity
{
    public class Favorite
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }
    }
}
