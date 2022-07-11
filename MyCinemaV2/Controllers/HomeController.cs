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
            MoviesTable moviesTable = new();
            return View(moviesTable.GetMoviesList(1));
        }

        public IActionResult Session(uint id)
        {
            SessionTable sessionTable = new();
            ViewBag.SessionsList = sessionTable.GetSessionsList(id);

            MoviesTable moviesTable = new();
            return View(moviesTable.GetMovieModel(id));
        }

        public IActionResult Seat(uint movieId, uint sessionId)
        {
            SeatsTable seatsTable = new();
            ViewBag.SeatsList = seatsTable.GetSeatsList(sessionId);

            MoviesTable moviesTable = new();
            return View(moviesTable.GetMovieModel(movieId));
        }

        public IActionResult Ticket(uint movieId, uint seatId)
        {
            SeatsTable seatsTable = new();
            ViewBag.Seat = seatsTable.GetSeatModel(seatId);

            MoviesTable moviesTable = new();
            return View(moviesTable.GetMovieModel(movieId));
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