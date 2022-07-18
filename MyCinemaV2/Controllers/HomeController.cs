using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCinemaV2.Models;
using MyCinemaV2.Services;
using System.Diagnostics;

namespace MyCinemaV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MovieModel movie = new()
            {
                Status = 1
            };
            return View(IMoviesDB.GetList(movie));
        }
        public IActionResult Session(MovieModel movie)
        {
            SessionModel session = new()
            {
                Movie_Id = movie.Id
            };

            ViewModel viewModel = new()
            {
                SessionsList = IMoviesDB.GetList(session),
                MovieModel = IMoviesDB.GetModel(movie)
            };
            return View(viewModel);
        }
        public IActionResult Seat(uint movieId, uint sessionId)
        {
            MovieModel movie = new()
            {
                Id = movieId
            };

            SeatModel seat = new()
            {
                Session_Id = sessionId
            };

            ViewModel viewModel = new()
            {
                SeatsList = IMoviesDB.GetList(seat),
                MovieModel = IMoviesDB.GetModel(movie)
            };
            return View(viewModel);
        }
        public IActionResult Ticket(uint movieId, uint seatId)
        {
            MovieModel movie = new()
            {
                Id = movieId
            };

            SeatModel seat = new()
            {
                Id = seatId
            };
            IMoviesDB.SetStatus(seat, 1);

            ViewModel viewModel = new()
            {
                SeatModel = IMoviesDB.GetModel(seat),
                MovieModel = IMoviesDB.GetModel(movie)
            };
            return View(viewModel);
        }
        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}