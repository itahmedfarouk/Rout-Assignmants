using Microsoft.AspNetCore.Mvc;
using Session02MVC.Models;
using System.Xml.Linq;

namespace Session02MVC.Controllers
{
    public class MoviesController : Controller
    {
        public string Index()
         {
            return $"Hello From Index";
        }

        #region Example 01
        ////Get BaseUrl/Movies/GetMovie/id=10&name=fileName   
        //[HttpGet]
        //public ContentResult GetMovie(int? id, string name)
        //{
        //    ////ContentResult result = new ContentResult();
        //    ////result.Content=$"Movie :: {id} </br> {name}";
        //    ////result.ContentType = "text/html";
        //    ////result.StatusCode = 700;
        //    ////return result;

        //    // same as But easy
        //    return Content($"Movie :: {id} </br> {name}", "text/html");
        //} 
        #endregion


        ////Get BaseUrl/Movies/GetMovie/id=10&name=fileName   
        [HttpGet]
        public IActionResult GetMovie(int? id, string name)
        {
            //if id = 0  -> Bad Request
            //id <10 NotFound
            // id >= 10 -> Return Data
            if (id == 0)
                return BadRequest();
            else if (id < 10)
                return NotFound();
            else
                return Content($"Movie With Name {name} ,, Id : {id}");

        }


        // Get BaseUrl/Movies/TestRedirectToAction
        [HttpGet]
        public IActionResult TestRedurictTiAction()
        {
            return RedirectToRoute("Default", new { Controller = "Movies", Action = "GetMovie", id = 16, name = "test" }); //Redirect To Route Not Action

            ////return Redirect("https://www.amazon.com/"); //Redirct To external Link
            ///
            ////return Redirect("https://localhost:7155/Movies/GetMovie?id=15&name=test"); //Redirct To Action
            //same as
            //return RedirectToAction("GetMovie");// In The Same Controller
            //return RedirectToAction(nameof(GetMovie), "Movies", new {id=15,name="test"});// In The Different Controller
        }


        [HttpPost]
        public IActionResult TestModelBindeing([FromRoute]int id ,[FromQuery] string name)
        {
            return Content($"hello {name} your id is {id}");
        }


        ////[HttpGet]
        ////public IActionResult AddMovie(int[] arr) // Collection  => https://localhost:7155/Movies/AddMovie?arr[0]=100&arr[1]=200
        ////{
        ////        return Content($"hello {arr[0]} your id is {arr[1]}");
        ////}

        [HttpGet]
        public IActionResult AddMovie(string Title, Movie movie, int Id, int[] arr) // Mixed [Complex Object + Simple Object + Collection]  => https://localhost:7155/Movies/AddMovie/20?Title=Avatar&arr[0]=100&arr[1]=200
        {
            return Content($"hello {arr[0]} your id is {arr[1]}");
        }


        ////[HttpGet]
        ////public IActionResult AddMovie(string Title, Movie movie, int Id) // Mixed [Complex Object + Simple Object]
        ////{
        ////    if (movie is null)
        ////        return BadRequest();
        ////    else
        ////        return Content($"hello {movie.Title} your id is {movie.Id}");
        ////}

        //[HttpGet]
        //public IActionResult AddMovie([FromHeader] Movie movie) // Complex Object
        //{
        //    if (movie is null)
        //        return BadRequest();
        //    else
        //        return Content($"hello {movie.Title} your id is {movie.Id}");
        //}

        ////[HttpPost]
        ////public IActionResult AddMovie([FromHeader] Movie movie) // Complex Object
        ////{
        ////    if (movie is null)
        ////        return BadRequest();
        ////    else
        ////        return Content($"hello {movie.Title} your id is {movie.Id}");
        ////}

        ////[HttpPost]
        ////public IActionResult AddMovie([FromHeader]int Id , [FromHeader] string Title) // Complex Object
        ////{
        ////        return Content($"hello {Title} your id is {Id}");
        ////}
    }
}
