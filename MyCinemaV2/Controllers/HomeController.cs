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
            return View(moviesTable.GetList(1));
        }
        public IActionResult Session(uint id)
        {
            SessionsTable sessionTable = new();
            MoviesTable moviesTable = new();
            ViewModel viewModel = new()
            {
                SessionsList = sessionTable.GetList(id),
                MovieModel = moviesTable.GetModel(id)
            };
            return View(viewModel);
        }
        public IActionResult Seat(uint movieId, uint sessionId)
        {
            SeatsTable seatsTable = new();
            MoviesTable moviesTable = new();
            ViewModel viewModel = new()
            {
                SeatsList = seatsTable.GetList(sessionId),
                MovieModel = moviesTable.GetModel(movieId)
            };
            return View(viewModel);
        }
        public IActionResult Ticket(uint movieId, uint seatId)
        {
            SeatsTable seatsTable = new();
            seatsTable.SetStatusById(seatId, 1);

            MoviesTable moviesTable = new();
            ViewModel viewModel = new()
            {
                SeatModel = seatsTable.GetModel(seatId),
                MovieModel = moviesTable.GetModel(movieId)
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