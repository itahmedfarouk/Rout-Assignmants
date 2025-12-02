using System.Xml.Linq;

namespace Session02MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Register Services In DI Container
            builder.Services.AddControllersWithViews(); //Register MVC Services
            #endregion

            var app = builder.Build();


            #region MapGet
            //app.MapGet("/", () => "Hello World!"); //Default Route
            ////////app.MapGet("/", () => "Hello World!"); //Confellect => Make Error
            //app.MapGet("/ahmed", () => "Hello Ahmed!"); //Static Segment Route
            //app.MapGet("/{name}", async (context) => {
            //    var name = context.GetRouteValue("name");
            //    await context.Response.WriteAsync($"Hello {name}!"); //Lambda Exp
            //    }); //Dynamic Segment Route

            //app.MapGet("/MR {name}", async (context) => {
            //    var name = context.GetRouteValue("name");
            //    await context.Response.WriteAsync($"Hello Mr {name}!"); //Lambda Exp
            //}); //Mixed Segment Route 
            #endregion


            app.UseStaticFiles(); //To Enable Access To wwwroot Folder

            ////app.MapControllerRoute(
            ////    name: "Default",
            ////    pattern: "{Controller=Movies}/{Action=Index}/{id?}/{name}"
            ////    );

            ////app.MapControllerRoute(
            ////    name: "Default",
            ////    pattern: "{Controller=Movies}/{Action=Index}/{id?}"
            ////    //same as
            ////    ////defaults: new {Controller = "Movies",  Action = "Index" },
            ////    ////constraints: new {}
            ////    );



            app.MapControllerRoute(
               name: "Default",
               pattern: "{Controller=Home}/{Action=Index}/{id?}"
               //same as
               ////defaults: new {Controller = "Movies",  Action = "Index" },
               ////constraints: new {}
               );
            app.Run();
        }
    }
}
