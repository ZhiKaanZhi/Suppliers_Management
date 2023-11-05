using WebApplication1.Entities;
using WebApplication1.DTO;

namespace WebApplication1.Repositories.RepositoryInterfaces
{
    public interface ISupplierRepository
    {
        Task<Supplier?> GetSupplierBySupplierID(Guid? id);
        Task<List<Supplier>> GetAllSuppliers();
        Task<Supplier> AddSupplier(Supplier? supplier);
        Task<Supplier> UpdateSupplier(Supplier? supplier);
        Task<bool> DeleteSupplier(Guid? id);
    }
}
