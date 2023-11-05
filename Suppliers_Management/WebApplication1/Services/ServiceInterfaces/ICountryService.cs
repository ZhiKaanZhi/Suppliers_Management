using WebApplication1.DTO.CountryDTOs;
using WebApplication1.Entities;

namespace WebApplication1.Services.ServiceInterfaces
{
    public interface ICountryService
    {
        Task<CountryResponse?> GetCountryByCountryID(Guid? id);
        Task<List<CountryResponse>> GetAllCountries();
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);
        Task<CountryResponse> UpdateCountry(CountryUpdateRequest? countryUpdateRequest);
        Task<bool> DeleteCountry(Guid? id);
        Task<CountryResponse?> GetCountryByCountryName(String? name);
    }
}
