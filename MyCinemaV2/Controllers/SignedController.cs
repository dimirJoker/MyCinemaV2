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
                return View(moviesTable.GetList());
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
                SessionsList = sessionTable.GetListByMovieId(id),
                MovieModel = moviesTable.GetModel(id)
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
                moviesTable.CreateModel(movie);

                return RedirectToAction("Index", new { Username = "root", Password = "root" });
            }
        }
        public IActionResult UpdateMovie(MovieModel movie)
        {
            MoviesTable moviesTable = new();

            if (movie.Name == null)
            {
                return View(moviesTable.GetModel((uint)movie.Id));
            }
            else
            {
                moviesTable.UpdateModel(movie);

                return RedirectToAction("Movie", new { id = movie.Id });
            }
        }
        public IActionResult DeleteMovie(uint id)
        {
            MoviesTable moviesTable = new();
            moviesTable.DeleteById(id);

            SessionsTable sessionsTable = new();
            sessionsTable.DeleteByMovieId(id);

            SeatsTable seatsTable = new();
            seatsTable.DeleteByMovieId(id);

            return RedirectToAction("Index", new { Username = "root", Password = "root" });
        }

        public IActionResult UpdateSession(SessionModel session)
        {
            if (session.Session == null)
            {
                SeatsTable seatsTable = new();
                SessionsTable sessionsTable = new();
                ViewModel viewModel = new()
                {
                    SeatsList = seatsTable.GetListBySessionId((uint)session.Id),
                    SessionModel = sessionsTable.GetModel((uint)session.Id)
                };

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("UpdateSession", new { id = session.Id });
            }
        }
        public IActionResult DeleteSession(uint id)/*--------------------TO DO--------------------*/
        {
            return null;
        }

        public IActionResult UpdateSeat(SeatModel seat)
        {
            SeatsTable seatsTable = new();
            seatsTable.UpdateModel(seat);

            return RedirectToAction("EditSession", new { id = seat.Session_Id });
        }
    }
}