using WebApplication1.Entities;
using WebApplication1.DTO;

namespace WebApplication1.RepositoryInterfaces
{
    public interface ISupplierRepository
    {
        SupplierResponse Get(int id);
        IEnumerable<Supplier> GetAll();
        SupplierResponse Add(SupplierAddRequest supplierAddRequest);
        SupplierResponse Update(SupplierUpdateRequest supplierUpdateRequest);
        bool Remove(int id);
    }
}
