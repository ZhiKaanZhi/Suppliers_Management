using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DTO.CountryDTOs;
using WebApplication1.Services.ServiceInterfaces;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            this._countryService = countryService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            List<CountryResponse> countries = await _countryService.GetAllCountries();
            return View(countries);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> AddCountry()
        {
            List<CountryResponse> countries = await _countryService.GetAllCountries();

            ViewBag.Countries = countries.Select(temp =>
                new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() }
            );

            
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddCountry(CountryAddRequest countryAddRequest)
        {
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


        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> FindCountry(Guid id)
        {
            CountryResponse? countryResponse = await _countryService.GetCountryByCountryID(id);
            if (countryResponse == null)
            {
                return RedirectToAction("Index");
            }

            return View(countryResponse);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> EditCountry(Guid id)
        {
            CountryResponse? countryResponse = await _countryService.GetCountryByCountryID(id);
            if (countryResponse == null)
            {
                return RedirectToAction("Index");
            }

            CountryUpdateRequest countryUpdateRequest = countryResponse.ToCountryUpdateRequest();

           
            return View(countryUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> EditCountry(CountryUpdateRequest countryUpdateRequest)
        {
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


        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
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
