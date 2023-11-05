using WebApplication1.DTO.SupplierCategoryDTOs;
using WebApplication1.Entities;

namespace WebApplication1.Services.ServiceInterfaces
{
    public interface ISupplierCategoryService
    {
        Task<SupplierCategoryResponse?> GetSupplierCategoryBySupplierCategoryID(Guid? id);
        Task<List<SupplierCategoryResponse>> GetAllSupplierCategories();
        Task<SupplierCategoryResponse> AddSupplierCategory(SupplierCategoryAddRequest? supplierCategoryAddRequest);
        Task<SupplierCategoryResponse> UpdateSupplierCategory(SupplierCategoryUpdateRequest? supplierCategoryUpdateRequest);
        Task<bool> DeleteSupplierCategory(Guid? id);
        Task<SupplierCategoryResponse?> GetSupplierCategoryBySupplierCategoryName(String? name);
    }
}
