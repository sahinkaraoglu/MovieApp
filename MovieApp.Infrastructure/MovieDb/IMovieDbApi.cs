
using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;
using MovieApp.Infrastructure.Models.MovieDb.TvSeriesDetail;

namespace MovieApp.Infrastructure.MovieDb
{
    public interface IMovieDbApi
    {
        Task<PopularTvSeriesModel> GetMoviesAsync();
        Task<TvSeriesDetailModel> GetMovieByIdAsync(int id);
    }
}