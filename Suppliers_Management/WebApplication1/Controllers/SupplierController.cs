using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO.SupplierDTOs;
using WebApplication1.Services.ServiceInterfaces;

namespace WebApplication1.Controllers
{
    public class SupplierController : Controller
    {

        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public IActionResult Index()
        {
            ViewBag.greeting = "Hello World";
            return View();
        }

        [Route("addsupplier")]
        public IActionResult AddSupplier(SupplierAddRequest supplierAddRequest)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            TempData["msg"] = "Added";
            return View();
        }
    }
}
