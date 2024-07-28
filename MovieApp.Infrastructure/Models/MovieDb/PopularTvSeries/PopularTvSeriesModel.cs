using MovieApp.Infrastructure.MovieDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Models.MovieDb.PopularTvSeries
{
    public class PopularTvSeriesModel
    {
        public int page { get; set; }
        public List<PopularTvSeriesResultDto> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
