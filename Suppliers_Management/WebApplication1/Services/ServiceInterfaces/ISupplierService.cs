using WebApplication1.DTO.SupplierDTOs;
using WebApplication1.Entities;

namespace WebApplication1.Services.ServiceInterfaces
{
    public interface ISupplierService
    {
        /// <summary>
        /// Retrieves the details of a supplier by their id.
        /// </summary>
        /// <param name="id">The id for the supplier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the SupplierResponse object if found, otherwise null.</returns>
        Task<SupplierResponse?> GetSupplierBySupplierID(Guid? id);

        /// <summary>
        /// Retrieves a list of all suppliers.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of SupplierResponse objects.</returns>
        Task<List<SupplierResponse>> GetAllSuppliers();

        /// <summary>
        /// Adds a new supplier to the system.
        /// </summary>
        /// <param name="supplierAddRequest">The supplier object to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the SupplierResponse object for the newly added supplier.</returns>
        Task<SupplierResponse> AddSupplier(SupplierAddRequest? supplierAddRequest);

        /// <summary>
        /// Updates the details of an existing supplier.
        /// </summary>
        /// <param name="supplierUpdateRequest">The updated supplier information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated SupplierResponse object.</returns>
        Task<SupplierResponse> UpdateSupplier(SupplierUpdateRequest? supplierUpdateRequest);

        /// <summary>
        /// Deletes a supplier from the system based on their id.
        /// </summary>
        /// <param name="id">The id of the supplier to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteSupplier(Guid? id);
    }
}
