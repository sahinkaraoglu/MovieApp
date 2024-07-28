
using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;

namespace MovieApp.Infrastructure.MovieDb
{
    public interface IMovieDbApi
    {
        Task<PopularTvSeriesModel> GetMoviesAsync();
    }
}