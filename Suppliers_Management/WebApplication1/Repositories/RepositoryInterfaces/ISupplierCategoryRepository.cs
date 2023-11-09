using WebApplication1.Entities;

namespace WebApplication1.Repositories.RepositoryInterfaces
{
    public interface ISupplierCategoryRepository
    {
        /// <summary>
        /// Retrieves the details of a supplier category by its id.
        /// </summary>
        /// <param name="id">The id for the supplier category.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the SupplierCategory object if found, otherwise null.</returns>
        Task<SupplierCategory?> GetSupplierCategoryBySupplierCategoryID(Guid? id);

        /// <summary>
        /// Retrieves a list of all supplier categories.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of SupplierCategory objects.</returns>
        Task<List<SupplierCategory>> GetAllSupplierCategories();

        /// <summary>
        /// Adds a new supplier category to the repository.
        /// </summary>
        /// <param name="supplierCategory">The supplier category object to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added SupplierCategory object.</returns>
        Task<SupplierCategory> AddSupplierCategory(SupplierCategory? supplierCategory);

        /// <summary>
        /// Updates an existing supplier category in the repository.
        /// </summary>
        /// <param name="supplierCategory">The supplier category object with updated information to replace the existing one.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated SupplierCategory object.</returns>
        Task<SupplierCategory> UpdateSupplierCategory(SupplierCategory? supplierCategory);

        /// <summary>
        /// Deletes a supplier category from the repository based on its id. If the category is deleted, all the suppliers associated with this category, will also be deleted.
        /// </summary>
        /// <param name="id">The id of the supplier category to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteSupplierCategory(Guid? id);

        /// <summary>
        /// Retrieves the details of a supplier category by its name.
        /// </summary>
        /// <param name="name">The name of the supplier category.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the SupplierCategory object if found, otherwise null.</returns>
        Task<SupplierCategory?> GetSupplierCategoryBySupplierCategoryName(String? name);
    }
}
