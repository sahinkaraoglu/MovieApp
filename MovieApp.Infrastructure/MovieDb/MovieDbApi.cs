using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries;
using MovieApp.Infrastructure.Models.MovieDb.TvSeriesDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.MovieDb
{
    public class MovieDbApi : IMovieDbApi
    {
        public async Task<PopularTvSeriesModel> GetMoviesAsync()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://api.themoviedb.org/3/discover/tv?include_adult=false&language=tr-TR&page=1&sort_by=popularity.desc&api_key=c53f45afaec37433217a385a706e45f1");
            var content = await response.Content.ReadAsStringAsync();

            // JsonSerializer - .net'in kendi paketi
            // Newtonsoft.Json - harici bir nuget paketi
            // Reflection nedir? --

            // serialize -> objeden texte
            // deserialize -> textten objeye

            client.Dispose();
            var movieResponse = JsonSerializer.Deserialize<PopularTvSeriesModel>(content);
            return movieResponse;
        }

        public async Task<TvSeriesDetailModel> GetMovieByIdAsync(int id)
        {
            HttpClient client = new HttpClient();
        
            var response = await client.GetAsync($"https://api.themoviedb.org/3/tv/{id}?api_key=c53f45afaec37433217a385a706e45f1");
            var content = await response.Content.ReadAsStringAsync();

            // JsonSerializer - .net'in kendi paketi
            // Newtonsoft.Json - harici bir nuget paketi
            // Reflection nedir? --

            // serialize -> objeden texte
            // deserialize -> textten objeye

            client.Dispose();
            var movieResponse = JsonSerializer.Deserialize<TvSeriesDetailModel>(content);

            return movieResponse;
        }
    }

    
}
