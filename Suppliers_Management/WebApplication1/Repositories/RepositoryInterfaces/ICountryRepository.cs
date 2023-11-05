using WebApplication1.Entities;

namespace WebApplication1.Repositories.RepositoryInterfaces
{
    public interface ICountryRepository
    {
        Task<Country?> GetCountryByCountryID(Guid? id);
        Task<List<Country>> GetAllCountries();
        Task<Country> AddCountry(Country? country);
        Task<Country> UpdateCountry(Country? country);
        Task<bool> DeleteCountry(Guid? id);
        Task<Country?> GetCountryByCountryName(String? name);
    }
}
