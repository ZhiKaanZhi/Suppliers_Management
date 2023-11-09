using WebApplication1.Entities;
using WebApplication1.DTO;

namespace WebApplication1.Repositories.RepositoryInterfaces
{
    public interface ISupplierRepository
    {
        /// <summary>
        /// Retrieves the details of a supplier by their id.
        /// </summary>
        /// <param name="id">The id for the supplier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Supplier object if found, otherwise null.</returns>
        Task<Supplier?> GetSupplierBySupplierID(Guid? id);

        /// <summary>
        /// Retrieves a list of all suppliers in the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of Supplier objects.</returns>
        Task<List<Supplier>> GetAllSuppliers();

        /// <summary>
        /// Adds a new supplier to the repository.
        /// </summary>
        /// <param name="supplier">The supplier object to add to the repository.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added Supplier object.</returns>
        Task<Supplier> AddSupplier(Supplier? supplier);

        /// <summary>
        /// Updates an existing supplier in the repository.
        /// </summary>
        /// <param name="supplier">The supplier object with updated information to replace the existing one.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated Supplier object.</returns>
        Task<Supplier> UpdateSupplier(Supplier? supplier);

        /// <summary>
        /// Deletes a supplier from the repository based on their id.
        /// </summary>
        /// <param name="id">The id of the supplier to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteSupplier(Guid? id);
    }
}
