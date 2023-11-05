using WebApplication1.DTO.CountryDTOs;
using WebApplication1.DTO.SupplierDTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories;
using WebApplication1.Repositories.RepositoryInterfaces;
using WebApplication1.Services.ServiceInterfaces;

namespace WebApplication1.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            if (countryAddRequest == null) throw new ArgumentNullException(nameof(countryAddRequest));
            Country? country = countryAddRequest.toCountry();

            Country? countryResponse = await _countryRepository.AddCountry(country);

            return countryResponse.ToCountryResponse();
        }

        public Task<bool> DeleteCountry(Guid? id)
        {
            if (!id.HasValue) throw new ArgumentNullException("id");
            return _countryRepository.DeleteCountry(id);
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            List<Country> countriesList = await _countryRepository.GetAllCountries();

            return countriesList.Select(temp => temp.ToCountryResponse()).ToList();
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? id)
        {
            if (!id.HasValue) throw new ArgumentNullException("id");

            Country? country = await _countryRepository.GetCountryByCountryID(id);
            if (country == null) return null;

            return country.ToCountryResponse();
        }

        public async Task<CountryResponse?> GetCountryByCountryName(string? name)
        {
            if (name == null) throw new ArgumentNullException("name");
            Country? country = await _countryRepository.GetCountryByCountryName(name);
            if (country == null) return null;

            return country.ToCountryResponse();
        }

        public async Task<CountryResponse> UpdateCountry(CountryUpdateRequest? countryUpdateRequest)
        {
            if (countryUpdateRequest == null) throw new ArgumentNullException(nameof(countryUpdateRequest));



            Country countryMatching = countryUpdateRequest.toCountry();

            Country countryResponse = await _countryRepository.UpdateCountry(countryMatching);

            return countryResponse.ToCountryResponse();
        }
    }
}
