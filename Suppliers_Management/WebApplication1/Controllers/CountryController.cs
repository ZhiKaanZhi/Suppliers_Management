using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DTO.CountryDTOs;
using WebApplication1.Services.ServiceInterfaces;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class CountryController : Controller
    {
        // Service for managing countries
        private readonly ICountryService _countryService;

        // Constructor to inject the country service
        public CountryController(ICountryService countryService)
        {
            this._countryService = countryService;
        }


        // Action to display the list of countries
        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            // Fetches all countries to display on the index page
            List<CountryResponse> countries = await _countryService.GetAllCountries();
            return View(countries);
        }

        // GET action to display the AddCountry view
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> AddCountry()
        {
            // Fetches countries for dropdown list preparation
            List<CountryResponse> countries = await _countryService.GetAllCountries();

            ViewBag.Countries = countries.Select(temp =>
                new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() }
            );

            
            return View();
        }


        // POST action to add a new country
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddCountry(CountryAddRequest countryAddRequest)
        {
            // Validates and adds a new country, then redirects to index
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            CountryResponse countryResponse = await _countryService.AddCountry(countryAddRequest);
            if(countryResponse == null)
            {
                TempData["msg"] = "Could not add Country";
                return View();
            }

            TempData["msg"] = "Added Succesfully";
            return RedirectToAction("Index", "Country");
        }

        // GET action to find and display a single country by ID
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> FindCountry(Guid id)
        {
            // Fetches and displays a single country
            CountryResponse? countryResponse = await _countryService.GetCountryByCountryID(id);
            if (countryResponse == null)
            {
                return RedirectToAction("Index");
            }

            return View(countryResponse);
        }

        // GET action to display the EditCountry view with existing country data
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> EditCountry(Guid id)
        {
            // Fetches country details for editing
            CountryResponse? countryResponse = await _countryService.GetCountryByCountryID(id);
            if (countryResponse == null)
            {
                return RedirectToAction("Index");
            }

            CountryUpdateRequest countryUpdateRequest = countryResponse.ToCountryUpdateRequest();

           
            return View(countryUpdateRequest);
        }

        // POST action to update an existing country
        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> EditCountry(CountryUpdateRequest countryUpdateRequest)
        {
            // Processes the update of a country after validation
            CountryResponse? countryResponse = await _countryService.GetCountryByCountryID(countryUpdateRequest.CountryId);

            if (countryResponse == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                CountryResponse updatedCountry = await _countryService.UpdateCountry(countryUpdateRequest);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(countryResponse.ToCountryUpdateRequest());
            }
        }

        // POST action to delete a country
        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            // Deletes a country and redirects to the index
            CountryResponse? countryResponse = await _countryService.GetCountryByCountryID(id);
            if (countryResponse == null)
            {
                return RedirectToAction("Index");
            }

            if (await _countryService.DeleteCountry(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }     
    }
}
