using Microsoft.AspNetCore.Mvc;
using Session02MVC.Models;

namespace Session02MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ////return View(); //Return View With Name Of Action{Index}
            ////return View("Index"); //Return View Specific Name 
            ////return View("Index",new Movie()); //Return View Specific Name And Model

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
