using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DTO.SupplierCategoryDTOs;
using WebApplication1.Services.ServiceInterfaces;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    public class SupplierCategoryController : Controller
    {
        private readonly ISupplierCategoryService _supplierCategoryService;

        public SupplierCategoryController(ISupplierCategoryService supplierCategoryService)
        {
            _supplierCategoryService = supplierCategoryService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            List<SupplierCategoryResponse> suppliers = await _supplierCategoryService.GetAllSupplierCategories();
            return View(suppliers);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> AddSupplierCategory()
        {
            List<SupplierCategoryResponse> supplierCategories = await _supplierCategoryService.GetAllSupplierCategories();

            ViewBag.SupplierCategories = supplierCategories.Select(temp =>
                new SelectListItem() { Text = temp.Description, Value = temp.CategoryId.ToString() }
            );

            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddSupplierCategory(SupplierCategoryAddRequest supplierCategoryAddRequest)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            SupplierCategoryResponse response = await _supplierCategoryService.AddSupplierCategory(supplierCategoryAddRequest);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> FindSupplierCategory(Guid id)
        {
            SupplierCategoryResponse? supplierCategoryResponse = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryID(id);
            if (supplierCategoryResponse == null)
            {
                return RedirectToAction("Index");
            }

            return View(supplierCategoryResponse);
        }

        

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> EditSupplierCategory(Guid id)
        {
            SupplierCategoryResponse? response = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryID(id);

            if (response == null)
            {
                return RedirectToAction("Index");
            }

            SupplierCategoryUpdateRequest supplierCategoryUpdateRequest = response.ToSupplierCategoryUpdateRequest();


            return View(supplierCategoryUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{id}")] 
        public async Task<IActionResult> EditSupplierCategory(SupplierCategoryUpdateRequest supplierCategoryUpdateRequest)
        {
            SupplierCategoryResponse? response = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryID(supplierCategoryUpdateRequest.CategoryId);

            if (response == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                SupplierCategoryResponse updatedSupplierCategory = await _supplierCategoryService.UpdateSupplierCategory(supplierCategoryUpdateRequest);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(response.ToSupplierCategoryUpdateRequest());
            }
        }


        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteSupplierCategory(Guid id)
        {
            SupplierCategoryResponse? supplierCategoryResponse = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryID(id);

            if (supplierCategoryResponse == null)
            {
                return RedirectToAction("Index");
            }

            if (await _supplierCategoryService.DeleteSupplierCategory(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
