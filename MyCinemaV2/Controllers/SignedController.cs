using Microsoft.AspNetCore.Mvc;
using MyCinemaV2.Models;
using MyCinemaV2.Services;

namespace MyCinemaV2.Controllers
{
    public class SignedController : Controller
    {
        public IActionResult Index(UserModel user)
        {
            if (user.Username == "root" && user.Password == "root")
            {
                MovieModel movie = null;
                return View(IMoviesDB.GetList(movie));
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
                IMoviesDB.Create(movie);

                return RedirectToAction("Index", new { Username = "root", Password = "root" });
            }
        }
        public IActionResult Movie(MovieModel movie)
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
        public IActionResult UpdateMovie(MovieModel movie)
        {
            if (movie.Name == null)
            {
                return View(IMoviesDB.GetModel(movie));
            }
            else
            {
                IMoviesDB.Update(movie);

                return RedirectToAction("Movie", new { id = movie.Id });
            }
        }
        public IActionResult DeleteMovie(MovieModel movie)
        {
            SeatModel seat = new()
            {
                Movie_Id = movie.Id
            };
            IMoviesDB.Delete(seat);

            SessionModel session = new()
            {
                Movie_Id = movie.Id
            };
            IMoviesDB.Delete(session);

            IMoviesDB.Delete(movie);

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
                IMoviesDB.Create(viewModel.SessionModel);

                SeatModel seat = new()
                {
                    Movie_Id = viewModel.SessionModel.Movie_Id,
                    Session_Id = IMoviesDB.GetIdMaxValue(viewModel.SessionModel)
                };
                IMoviesDB.Create(seat);

                return RedirectToAction("Movie", new { id = viewModel.SessionModel.Movie_Id });
            }
        }
        public IActionResult UpdateSession(SessionModel session, ViewModel viewModel)
        {
            if (viewModel.SessionModel == null)
            {
                SeatModel seat = new()
                {
                    Session_Id = session.Id
                };

                viewModel = new()
                {
                    SeatsList = IMoviesDB.GetList(seat),
                    SessionModel = IMoviesDB.GetModel(session)
                };
                return View(viewModel);
            }
            else
            {
                IMoviesDB.Update(viewModel.SessionModel);

                return RedirectToAction("Movie", new { id = viewModel.SessionModel.Movie_Id });
            }
        }
        public IActionResult UpdateSeat(SeatModel seat)
        {
            IMoviesDB.Update(seat);

            return RedirectToAction("UpdateSession", new { id = seat.Session_Id });
        }
        public IActionResult DeleteSession(SessionModel session)
        {
            SeatModel seat = new()
            {
                Session_Id = session.Id
            };
            IMoviesDB.Delete(seat);

            var memo = IMoviesDB.GetModel(session);

            IMoviesDB.Delete(session);

            return RedirectToAction("Movie", new { id = memo.Movie_Id });
        }
    }
}