using WebApplication1.Entities;

namespace WebApplication1.Repositories.RepositoryInterfaces
{
    public interface ICountryRepository
    {
        /// <summary>
        /// Retrieves the details of a country by its id.
        /// </summary>
        /// <param name="id">The id for the country.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Country object if found, otherwise null.</returns>
        Task<Country?> GetCountryByCountryID(Guid? id);

        /// <summary>
        /// Retrieves a list of all countries in the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all Country objects.</returns>
        Task<List<Country>> GetAllCountries();

        /// <summary>
        /// Adds a new country to the repository.
        /// </summary>
        /// <param name="country">The country object to add to the repository.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added Country object.</returns>
        Task<Country> AddCountry(Country? country);

        /// <summary>
        /// Updates an existing country in the repository.
        /// </summary>
        /// <param name="country">The country object with updated information to replace the existing one.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated Country object.</returns>
        Task<Country> UpdateCountry(Country? country);

        /// <summary>
        /// Deletes a country from the repository. If the country is deleted, all the suppliers associated with this country, will also be deleted.
        /// </summary>
        /// <param name="id">The id of the country to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteCountry(Guid? id);

        /// <summary>
        /// Retrieves the details of a country by its name.
        /// </summary>
        /// <param name="name">The name of the country to find.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Country object if found, otherwise null.</returns>
        Task<Country?> GetCountryByCountryName(String? name);
    }
}
