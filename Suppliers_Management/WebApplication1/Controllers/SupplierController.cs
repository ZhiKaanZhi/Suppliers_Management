﻿using Microsoft.AspNetCore.Mvc;
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

            if (ModelState.IsValid)
            {
                SupplierResponse updatedSupplier = await _supplierService.UpdateSupplier(supplierUpdateRequest);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(response.ToSupplierUpdateRequest());
            }
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
    }
}
