using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Entities;
using WebApplication1.RepositoryInterfaces;

namespace WebApplication1.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {

        private readonly DatabaseContext _context;

        public SupplierRepository(DatabaseContext context)
        {
            _context = context;
        }

        public SupplierResponse Add(SupplierAddRequest supplierAddRequest)
        {
            if (supplierAddRequest == null)
            {
                throw new ArgumentNullException(nameof(supplierAddRequest));
            }
            Supplier supplier = supplierAddRequest.ToSupplier();
            _context.Add(supplier);

            return supplier.ToPersonResponse();
        }

        public SupplierResponse? Get(int? id)
        {
            if (id == null)
                return null;
            
            Supplier? supplier = _context.Suppliers.Find(id);

            if (supplier == null)
                return null;

            return supplier.ToPersonResponse();
        }

        public SupplierResponse Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Supplier> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public SupplierResponse Update(SupplierUpdateRequest supplierUpdateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
