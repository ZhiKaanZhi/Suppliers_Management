using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DTO.CountryDTOs;
using WebApplication1.DTO.SupplierCategoryDTOs;
using WebApplication1.DTO.SupplierDTOs;
using WebApplication1.Entities;
using WebApplication1.Services;
using WebApplication1.Services.ServiceInterfaces;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class SupplierController : Controller
    {

        private readonly ISupplierService _supplierService;
        private readonly ICountryService _countryService;
        private readonly ISupplierCategoryService _supplierCategoryService;

        public SupplierController(ISupplierService supplierService, ICountryService countryService, ISupplierCategoryService supplierCategoryService)
        {
            _supplierService = supplierService;
            _countryService = countryService;
            _supplierCategoryService = supplierCategoryService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            List<SupplierResponse> suppliers = await _supplierService.GetAllSuppliers();
            return View(suppliers);
        }


        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> FindSupplier(Guid id)
        {
            SupplierResponse? supplierResponse = await _supplierService.GetSupplierBySupplierID(id);
            if (supplierResponse == null)
            {
                return RedirectToAction("Index");
            }

            return View(supplierResponse);
        }


        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> AddSupplier()
        {
            var viewModel = new SupplierAddRequest();

            await PopulateSupplierCategoriesAndCountries(viewModel);


            return View(viewModel);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddSupplier(SupplierAddRequest supplierAddRequest)
        {
            if (!ModelState.IsValid)
            {
                await PopulateSupplierCategoriesAndCountries(supplierAddRequest);
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(supplierAddRequest);
            }

            SupplierResponse supplierResponse = await _supplierService.AddSupplier(supplierAddRequest);
            if (supplierResponse == null)
            {
                TempData["msg"] = "Could not add Supplier";
                return View();
            }

            TempData["msg"] = "Added Succesfully";
            return RedirectToAction("Index", "Supplier");
        }




        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> EditSupplier(Guid id)
        {
            SupplierResponse? response = await _supplierService.GetSupplierBySupplierID(id);
            if (response == null)
            {
                return RedirectToAction("Index");
            }

            SupplierUpdateRequest supplierUpdateRequest = response.ToSupplierUpdateRequest();
            await PopulateSupplierCategoriesAndCountriesForEdit(supplierUpdateRequest);


            return View(supplierUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> EditSupplier(SupplierUpdateRequest supplierUpdateRequest)
        {
            SupplierResponse? response = await _supplierService.GetSupplierBySupplierID(supplierUpdateRequest.SupplierId);

            if (response == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                await PopulateSupplierCategoriesAndCountriesForEdit(supplierUpdateRequest);
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(supplierUpdateRequest);
            }

            // Validate that the provided category and country names are valid
            var category = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryName(supplierUpdateRequest.CategoryName);
            var country = await _countryService.GetCountryByCountryName(supplierUpdateRequest.CountryName);

            if (category == null || country == null)
            {
                ModelState.AddModelError("", "Invalid category or country selected.");
                await PopulateSupplierCategoriesAndCountriesForEdit(supplierUpdateRequest);
                return View(supplierUpdateRequest);
            }

            // Your service should handle the lookup of IDs based on names internally
            SupplierResponse updatedSupplier = await _supplierService.UpdateSupplier(supplierUpdateRequest);
            return RedirectToAction("Index");
        }



        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            SupplierResponse? response = await _supplierService.GetSupplierBySupplierID(id);
            if (response == null)
            {
                return RedirectToAction("Index");
            }

            if (await _supplierService.DeleteSupplier(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }




        private async Task PopulateSupplierCategoriesAndCountries(SupplierAddRequest request)
        {
            List<SupplierCategoryResponse> categoryResponses = await _supplierCategoryService.GetAllSupplierCategories();
            List<SupplierCategory> supplierCategoriesTransformed = categoryResponses.Select(temp => temp.ToSupplierCategory()).ToList();

            List<CountryResponse> countryResponses = await _countryService.GetAllCountries();
            List<Country> countriesTransformed = countryResponses.Select(temp => temp.ToCountry()).ToList();

            request.Categories = supplierCategoriesTransformed;
            request.Countries = countriesTransformed;
        }

        private async Task PopulateSupplierCategoriesAndCountriesForEdit(SupplierUpdateRequest request)
        {
            List<SupplierCategoryResponse> categoryResponses = await _supplierCategoryService.GetAllSupplierCategories();
            List<SupplierCategory> supplierCategoriesTransformed = categoryResponses.Select(temp => temp.ToSupplierCategory()).ToList();

            List<CountryResponse> countryResponses = await _countryService.GetAllCountries();
            List<Country> countriesTransformed = countryResponses.Select(temp => temp.ToCountry()).ToList();

            request.Categories = supplierCategoriesTransformed; // assuming SupplierUpdateRequest has these properties
            request.Countries = countriesTransformed; // if not, add them
        }
    }
}
