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
        // Services injected through the constructor
        private readonly ISupplierService _supplierService;
        private readonly ICountryService _countryService;
        private readonly ISupplierCategoryService _supplierCategoryService;

        // Constructor to inject dependencies
        public SupplierController(ISupplierService supplierService, ICountryService countryService, ISupplierCategoryService supplierCategoryService)
        {
            _supplierService = supplierService;
            _countryService = countryService;
            _supplierCategoryService = supplierCategoryService;
        }


        // Action to display the list of suppliers
        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            List<SupplierResponse> suppliers = await _supplierService.GetAllSuppliers();
            return View(suppliers);
        }


        // Action to find and display a single supplier by ID
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

        // GET action to display the AddSupplier view
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> AddSupplier()
        {
            var viewModel = new SupplierAddRequest();

            await PopulateSupplierCategoriesAndCountries(viewModel);


            return View(viewModel);
        }

        // POST action to add a new supplier
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



        // GET action to display the EditSupplier view
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


        // POST action to update an existing supplier
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

            var category = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryName(supplierUpdateRequest.CategoryName);
            var country = await _countryService.GetCountryByCountryName(supplierUpdateRequest.CountryName);

            if (category == null || country == null)
            {
                ModelState.AddModelError("", "Invalid category or country selected.");
                await PopulateSupplierCategoriesAndCountriesForEdit(supplierUpdateRequest);
                return View(supplierUpdateRequest);
            }

            _ = await _supplierService.UpdateSupplier(supplierUpdateRequest);
            return RedirectToAction("Index");
        }


        // POST action to delete a supplier
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



        // Helper method to populate categories and countries for the AddSupplier view
        private async Task PopulateSupplierCategoriesAndCountries(SupplierAddRequest request)
        {
            List<SupplierCategoryResponse> categoryResponses = await _supplierCategoryService.GetAllSupplierCategories();
            List<SupplierCategory> supplierCategoriesTransformed = categoryResponses.Select(temp => temp.ToSupplierCategory()).ToList();

            List<CountryResponse> countryResponses = await _countryService.GetAllCountries();
            List<Country> countriesTransformed = countryResponses.Select(temp => temp.ToCountry()).ToList();

            request.Categories = supplierCategoriesTransformed;
            request.Countries = countriesTransformed;
        }


        // Helper method to populate categories and countries for the EditSupplier view
        private async Task PopulateSupplierCategoriesAndCountriesForEdit(SupplierUpdateRequest request)
        {
            List<SupplierCategoryResponse> categoryResponses = await _supplierCategoryService.GetAllSupplierCategories();
            List<SupplierCategory> supplierCategoriesTransformed = categoryResponses.Select(temp => temp.ToSupplierCategory()).ToList();

            List<CountryResponse> countryResponses = await _countryService.GetAllCountries();
            List<Country> countriesTransformed = countryResponses.Select(temp => temp.ToCountry()).ToList();

            request.Categories = supplierCategoriesTransformed; 
            request.Countries = countriesTransformed; 
        }
    }
}
