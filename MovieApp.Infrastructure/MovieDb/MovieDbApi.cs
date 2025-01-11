using MovieApp.Infrastructure.Models.MovieDb.Movies;
using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;
using MovieApp.Infrastructure.Models.MovieDb.TvSeriesDetail;
using System.Text.Json;

namespace MovieApp.Infrastructure.MovieDb
{
    public class MovieDbApi : IMovieDbApi
    {
        public async Task<PopularMoviesModel> GetPopularMoviesAsync()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://api.themoviedb.org/3/discover/movie?include_adult=false&language=en-US&page=1&sort_by=vote_average.desc&vote_count.gte=200&api_key=c53f45afaec37433217a385a706e45f1");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            var movieResponse = JsonSerializer.Deserialize<PopularMoviesModel>(content);
            return movieResponse;
        }

        public async Task<MovieDetailModel> GetMovieDetailByIdAsync(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{id}?api_key=c53f45afaec37433217a385a706e45f1");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            var movieResponse = JsonSerializer.Deserialize<MovieDetailModel>(content);
            return movieResponse;
        }

        public async Task<PopularTvSeriesModel> GetTvSeriesAsync()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://api.themoviedb.org/3/discover/tv?include_adult=false&language=en-US&page=1&sort_by=vote_average.desc&vote_count.gte=200&api_key=c53f45afaec37433217a385a706e45f1");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            var seriesResponse = JsonSerializer.Deserialize<PopularTvSeriesModel>(content);
            return seriesResponse;
        }

        public async Task<TvSeriesDetailModel> GetTvSeriesDetailByIdAsync(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://api.themoviedb.org/3/tv/{id}?api_key=c53f45afaec37433217a385a706e45f1");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            var seriesDetail = JsonSerializer.Deserialize<TvSeriesDetailModel>(content);
            return seriesDetail;
        }
    }
}
