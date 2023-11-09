using WebApplication1.DTO.CountryDTOs;
using WebApplication1.Entities;

namespace WebApplication1.Services.ServiceInterfaces
{
    public interface ICountryService
    {
        /// <summary>
        /// Retrieves the details of a country by its id.
        /// </summary>
        /// <param name="id">The id for the country.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the CountryResponse object if found, otherwise null.</returns>
        Task<CountryResponse?> GetCountryByCountryID(Guid? id);

        /// <summary>
        /// Retrieves a list of all countries.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of CountryResponse objects.</returns>
        Task<List<CountryResponse>> GetAllCountries();

        /// <summary>
        /// Adds a new country to the system.
        /// </summary>
        /// <param name="countryAddRequest">The country object to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the CountryResponse object after adding it, including the newly generated country ID.</returns>
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Updates the details of an existing country.
        /// </summary>
        /// <param name="countryUpdateRequest">The updated country information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated CountryResponse object.</returns>
        Task<CountryResponse> UpdateCountry(CountryUpdateRequest? countryUpdateRequest);

        /// <summary>
        /// Deletes a country from the system. If the country is deleted, all the suppliers associated with this country, will also be deleted.
        /// </summary>
        /// <param name="id">The id of the country to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteCountry(Guid? id);

        /// <summary>
        /// Retrieves the details of a country by its name.
        /// </summary>
        /// <param name="name">The name of the country.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the CountryResponse object if found, otherwise null.</returns>
        Task<CountryResponse?> GetCountryByCountryName(String? name);
    }
}
