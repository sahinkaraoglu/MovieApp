namespace MovieApp.Infrastructure.Models.MovieDb.Movies
{
    public class MovieDetailResponseModel
    {
        public MovieDetailModel data { get; set; }
        public List<CommentModel> comments { get; set; }
    }

    public class CommentModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public UserModel User { get; set; }
    }

    public class UserModel
    {
        public string UserName { get; set; }
    }
} 