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
            MoviesTable moviesTable = new();
            ViewModel viewModel = new()
            {
                SessionsList = sessionTable.GetSessionsList(id),
                MovieModel = moviesTable.GetMovieModel(id)
            };
            return View(viewModel);
        }
        public IActionResult CreateMovie(MovieModel movie)
        {
            if (movie.Name == null)
            {
                return View();
            }
            else
            {
                MoviesTable moviesTable = new();
                moviesTable.Create(movie);

                return RedirectToAction("Index", new { Username = "root", Password = "root" });
            }
        }
        public IActionResult EditMovie(MovieModel movie)
        {
            MoviesTable moviesTable = new();

            if (movie.Name == null)
            {
                return View(moviesTable.GetMovieModel((uint)movie.Id));
            }
            else
            {
                moviesTable.Update(movie);

                return RedirectToAction("Movie", new { id = movie.Id });
            }
        }
        public IActionResult DeleteMovie(uint id)
        {
            MoviesTable moviesTable = new();
            moviesTable.Delete(id);

            SessionsTable sessionsTable = new();
            sessionsTable.Delete(id);

            SeatsTable seatsTable = new();
            seatsTable.Delete(id);

            return RedirectToAction("Index", new { Username = "root", Password = "root" });
        }

        public IActionResult EditSession(uint id)
        {
            SeatsTable seatsTable = new();
            SessionsTable sessionsTable = new();
            ViewModel viewModel = new()
            {
                SeatsList = seatsTable.GetSeatsList(id),
                SessionModel = sessionsTable.GetSessionModel(id)
            };
            return View(viewModel);
        }
        public IActionResult UpdateSession(SessionModel session) /*===========TO DEBUG===========*/
        {
            return RedirectToAction("Session", new { id = session.Id });
        }
        public IActionResult DeleteSession(uint id)/*--------------------TO DO--------------------*/
        {
            return null;
        }

        public IActionResult UpdateSeat(SeatModel seat)
        {
            SeatsTable seatsTable = new();
            seatsTable.Update(seat);

            return RedirectToAction("EditSession", new { id = seat.Session_Id });
        }
    }
}