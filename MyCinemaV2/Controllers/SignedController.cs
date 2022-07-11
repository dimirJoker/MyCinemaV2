using Microsoft.AspNetCore.Mvc;
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
    }
}