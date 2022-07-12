using Microsoft.AspNetCore.Mvc;
using MyCinemaV2.Models;
using MyCinemaV2.Services;

namespace MyCinemaV2.Controllers
{
    public class SignedController : Controller
    {
        public IActionResult Index(string Username, string Password)
        {
            if (Username == "root" && Password == "root")
            {
                MoviesTable moviesTable = new();
                return View(moviesTable.GetMoviesList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Movie(uint id)
        {
            SessionsTable sessionTable = new();
            ViewBag.SessionsList = sessionTable.GetSessionsList(id);

            MoviesTable moviesTable = new();
            return View(moviesTable.GetMovieModel(id));
        }
        public IActionResult EditMovie(uint id)
        {
            MoviesTable moviesTable = new();
            return View(moviesTable.GetMovieModel(id));
        }
        public IActionResult UpdateMovie(MovieModel movie)
        {
            MoviesTable moviesTable = new();
            moviesTable.Update(movie);

            return RedirectToAction("Movie", new { id = movie.Id });
        }
        public IActionResult DeleteMovie(uint id)
        {
            MoviesTable moviesTable = new();
            moviesTable.Delete(id);

            return RedirectToAction("Index", new { Username = "root", Password = "root" });
        }

        public IActionResult Session(uint id)
        {
            SeatsTable seatsTable = new();
            ViewBag.SeatsList = seatsTable.GetSeatsList(id);

            SessionsTable sessionsTable = new();
            return View(sessionsTable.GetSessionModel(id));
        }
        public IActionResult EditSession(SessionModel session) /*===========TO DEBUG===========*/
        {
            return RedirectToAction("Session", new { id = session.Id });
        }
        public IActionResult EditSeat(SeatModel seat)
        {
            SeatsTable seatsTable = new();
            seatsTable.Update(seat);

            return RedirectToAction("Session", new { id = seat.Session_Id });
        }
    }
}