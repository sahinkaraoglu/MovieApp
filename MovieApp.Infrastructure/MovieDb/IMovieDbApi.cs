using MovieApp.Infrastructure.Models.MovieDb.Movies;
using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;
using MovieApp.Infrastructure.Models.MovieDb.TvSeriesDetail;

namespace MovieApp.Infrastructure.MovieDb
{
    public interface IMovieDbApi
    {
        Task<PopularTvSeriesModel> GetMoviesAsync();
        Task<PopularMoviesModel> GetPopularMoviesAsync();
        Task<TvSeriesDetailModel> GetMovieByIdAsync(int id);
        Task<MovieDetailModel> GetMovieDetailByIdAsync(int id);
    }
}