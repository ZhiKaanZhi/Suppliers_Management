using WebApplication1.Entities;

namespace WebApplication1.Repositories.RepositoryInterfaces
{
    public interface ISupplierCategoryRepository
    {
        Task<SupplierCategory?> GetSupplierCategoryBySupplierCategoryID(Guid? id);
        Task<List<SupplierCategory>> GetAllSupplierCategories();
        Task<SupplierCategory> AddSupplierCategory(SupplierCategory? supplierCategory);
        Task<SupplierCategory> UpdateSupplierCategory(SupplierCategory? supplierCategory);
        Task<bool> DeleteSupplierCategory(Guid? id);
        Task<SupplierCategory?> GetSupplierCategoryBySupplierCategoryName(String? name);
    }
}
