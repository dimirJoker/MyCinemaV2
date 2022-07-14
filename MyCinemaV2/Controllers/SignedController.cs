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
        public IActionResult UpdateMovie(MovieModel movie)
        {
            MoviesTable moviesTable = new();

            if (movie.Name == null)
            {
                return View(moviesTable.GetModel((uint)movie.Id));
            }
            else
            {
                moviesTable.Update(movie);

                return RedirectToAction("Movie", new { id = movie.Id });
            }
        }
        public IActionResult DeleteMovie(uint id)
        {
            SeatsTable seatsTable = new();
            seatsTable.DeleteAllByMovieId(id);

            SessionsTable sessionsTable = new();
            sessionsTable.DeleteAllByMovieId(id);

            MoviesTable moviesTable = new();
            moviesTable.Delete(id);

            return RedirectToAction("Index", new { Username = "root", Password = "root" });
        }

        public IActionResult CreateSession(uint movieId, ViewModel viewModel)
        {
            if (viewModel.SessionModel == null)
            {
                viewModel.SessionModel = new()
                {
                    Movie_Id = movieId
                };
                return View(viewModel);
            }
            else
            {
                SessionsTable sessionsTable = new();
                sessionsTable.Create(viewModel.SessionModel);

                SeatModel seat = new()
                {
                    Movie_Id = viewModel.SessionModel.Movie_Id,
                    Session_Id = sessionsTable.GetIdMaxValue()
                };

                SeatsTable seatsTable = new();
                seatsTable.Create(seat);

                return RedirectToAction("Movie", new { id = viewModel.SessionModel.Movie_Id });
            }
        }
        public IActionResult UpdateSession(uint id, ViewModel viewModel)
        {
            SessionsTable sessionsTable = new();

            if (viewModel.SessionModel == null)
            {
                SeatsTable seatsTable = new();
                viewModel = new()
                {
                    SeatsList = seatsTable.GetListBySessionId(id),
                    SessionModel = sessionsTable.GetModel(id)
                };
                return View(viewModel);
            }
            else
            {
                sessionsTable.Update(viewModel.SessionModel);

                return RedirectToAction("Movie", new { id = viewModel.SessionModel.Movie_Id });
            }
        }
        public IActionResult UpdateSeat(SeatModel seat)
        {
            SeatsTable seatsTable = new();
            seatsTable.Update(seat);

            return RedirectToAction("UpdateSession", new { id = seat.Session_Id });
        }
        public IActionResult DeleteSession(uint id)
        {
            SeatsTable seatsTable = new();
            seatsTable.DeleteAllBySessionId(id);

            SessionsTable sessionsTable = new();
            var session = sessionsTable.GetModel(id);
            sessionsTable.Delete(id);

            return RedirectToAction("Movie", new { id = session.Movie_Id });
        }
    }
}