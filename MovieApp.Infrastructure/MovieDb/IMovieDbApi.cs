using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;
using MovieApp.Infrastructure.Models.MovieDb.TvSeriesDetail;

namespace MovieApp.Infrastructure.MovieDb
{
    public interface IMovieDbApi
    {
        Task<PopularTvSeriesModel> GetMoviesAsync();
        Task<PopularTvSeriesModel> GetPopularMoviesAsync();
        Task<TvSeriesDetailModel> GetMovieByIdAsync(int id);
        Task<TvSeriesDetailModel> GetMovieDetailByIdAsync(int id);
    }
}