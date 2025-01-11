namespace MovieApp.Domain.Entity
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
} 