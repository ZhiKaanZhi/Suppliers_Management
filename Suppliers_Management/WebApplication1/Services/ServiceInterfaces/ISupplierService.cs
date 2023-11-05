using WebApplication1.DTO.SupplierDTOs;
using WebApplication1.Entities;

namespace WebApplication1.Services.ServiceInterfaces
{
    public interface ISupplierService
    {
        Task<SupplierResponse?> GetSupplierBySupplierID(Guid? id);
        Task<List<SupplierResponse>> GetAllSuppliers();
        Task<SupplierResponse> AddSupplier(SupplierAddRequest? supplierAddRequest);
        Task<SupplierResponse> UpdateSupplier(SupplierUpdateRequest? supplierUpdateRequest);
        Task<bool> DeleteSupplier(Guid? id);
    }
}
