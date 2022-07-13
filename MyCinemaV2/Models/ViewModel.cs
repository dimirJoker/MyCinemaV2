using System.Collections.Generic;

namespace MyCinemaV2.Models
{
    public class ViewModel
    {
        public MovieModel MovieModel { get; set; }
        public List<MovieModel> MoviesList { get; set; }

        public SessionModel SessionModel { get; set; }
        public List<SessionModel> SessionsList { get; set; }

        public SeatModel SeatModel { get; set; }
        public List<SeatModel> SeatsList { get; set; }
    }
}