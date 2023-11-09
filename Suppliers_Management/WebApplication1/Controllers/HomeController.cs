using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // The default action that displays the home page
        [Route("/")]
        public IActionResult Index()
        {
            // Sets a greeting message to be displayed on the home page
            ViewBag.greeting = "Supplier Management Home Page";
            return View();
        }
    }
}
