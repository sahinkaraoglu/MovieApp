﻿using MovieApp.Infrastructure.Models.MovieDb.Movies;
using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;
using MovieApp.Infrastructure.Models.MovieDb.TvSeriesDetail;

namespace MovieApp.Infrastructure.MovieDb
{
    public interface IMovieDbApi
    {
        Task<PopularTvSeriesModel> GetTvSeriesAsync();
        Task<TvSeriesDetailModel> GetTvSeriesDetailByIdAsync(int id);
        Task<PopularMoviesModel> GetPopularMoviesAsync();
        Task<MovieDetailModel> GetMovieDetailByIdAsync(int id);
    }
}