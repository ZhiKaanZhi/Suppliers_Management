using WebApplication1.DTO.SupplierCategoryDTOs;
using WebApplication1.Entities;

namespace WebApplication1.Services.ServiceInterfaces
{
    public interface ISupplierCategoryService
    {
        /// <summary>
        /// Retrieves the details of a supplier category by its id.
        /// </summary>
        /// <param name="id">The id for the supplier category.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the SupplierCategoryResponse object if found, otherwise null.</returns>
        Task<SupplierCategoryResponse?> GetSupplierCategoryBySupplierCategoryID(Guid? id);

        /// <summary>
        /// Retrieves a list of all supplier categories.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of SupplierCategoryResponse objects.</returns>
        Task<List<SupplierCategoryResponse>> GetAllSupplierCategories();

        /// <summary>
        /// Adds a new supplier category to the system.
        /// </summary>
        /// <param name="supplierCategoryAddRequest">The supplier category object to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the SupplierCategoryResponse object for the newly added supplier category.</returns>
        Task<SupplierCategoryResponse> AddSupplierCategory(SupplierCategoryAddRequest? supplierCategoryAddRequest);

        /// <summary>
        /// Updates the details of an existing supplier category.
        /// </summary>
        /// <param name="supplierCategoryUpdateRequest">The updated supplier category information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated SupplierCategoryResponse object.</returns>
        Task<SupplierCategoryResponse> UpdateSupplierCategory(SupplierCategoryUpdateRequest? supplierCategoryUpdateRequest);

        /// <summary>
        /// Deletes a supplier category from the system based on its id. If the category is deleted, all the suppliers associated with this category, will also be deleted.
        /// </summary>
        /// <param name="id">The id of the supplier category to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteSupplierCategory(Guid? id);

        /// <summary>
        /// Retrieves the details of a supplier category by its name.
        /// </summary>
        /// <param name="name">The name of the supplier category.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the SupplierCategoryResponse object if found, otherwise null.</returns>
        Task<SupplierCategoryResponse?> GetSupplierCategoryBySupplierCategoryName(String? name);
    }
}
