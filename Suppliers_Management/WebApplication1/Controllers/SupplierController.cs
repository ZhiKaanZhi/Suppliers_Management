using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
