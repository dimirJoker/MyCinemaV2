using System.Collections.Generic;

namespace MyCinemaV2.Models
{
    public class ViewModel
    {
        public List<MovieModel> MoviesList { get; set; }
        public List<SeatModel> SeatsList { get; set; }
        public List<SessionModel> SessionsList { get; set; }
        public MovieModel MovieModel { get; set; }
        public SeatModel SeatModel { get; set; }
        public SessionModel SessionModel { get; set; }
    }
}