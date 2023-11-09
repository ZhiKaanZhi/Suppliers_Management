using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DTO.SupplierCategoryDTOs;
using WebApplication1.Services.ServiceInterfaces;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class SupplierCategoryController : Controller
    {
        // Service for managing supplier categories
        private readonly ISupplierCategoryService _supplierCategoryService;

        // Constructor to inject the supplier category service
        public SupplierCategoryController(ISupplierCategoryService supplierCategoryService)
        {
            _supplierCategoryService = supplierCategoryService;
        }

        // Action to display the list of supplier categories
        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            // Fetches all supplier categories to display
            List<SupplierCategoryResponse> suppliers = await _supplierCategoryService.GetAllSupplierCategories();
            return View(suppliers);
        }

        // GET action to display the AddSupplierCategory view
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> AddSupplierCategory()
        {
            // Preparation for a new supplier category addition
            List<SupplierCategoryResponse> supplierCategories = await _supplierCategoryService.GetAllSupplierCategories();

            ViewBag.SupplierCategories = supplierCategories.Select(temp =>
                new SelectListItem() { Text = temp.Description, Value = temp.CategoryId.ToString() }
            );

            return View();
        }

        // POST action to add a new supplier category
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddSupplierCategory(SupplierCategoryAddRequest supplierCategoryAddRequest)
        {
            // Adds a new supplier category after validation
            if (!ModelState.IsValid)
            {

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            SupplierCategoryResponse response = await _supplierCategoryService.AddSupplierCategory(supplierCategoryAddRequest);

            return RedirectToAction("Index");
        }

        // GET action to find and display a single supplier category by ID
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> FindSupplierCategory(Guid id)
        {
            // Fetches and displays a single supplier category
            SupplierCategoryResponse? supplierCategoryResponse = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryID(id);
            if (supplierCategoryResponse == null)
            {
                return RedirectToAction("Index");
            }

            return View(supplierCategoryResponse);
        }


        // GET action to display the EditSupplierCategory view with existing category data
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> EditSupplierCategory(Guid id)
        {
            // Fetches supplier category details for editing
            SupplierCategoryResponse? response = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryID(id);

            if (response == null)
            {
                return RedirectToAction("Index");
            }

            SupplierCategoryUpdateRequest supplierCategoryUpdateRequest = response.ToSupplierCategoryUpdateRequest();


            return View(supplierCategoryUpdateRequest);
        }


        // POST action to update an existing supplier category
        [HttpPost]
        [Route("[action]/{id}")] 
        public async Task<IActionResult> EditSupplierCategory(SupplierCategoryUpdateRequest supplierCategoryUpdateRequest)
        {
            // Processes the update of a supplier category after validation
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

        // POST action to delete a supplier category
        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteSupplierCategory(Guid id)
        {
            // Deletes a supplier category and redirects to the index
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
