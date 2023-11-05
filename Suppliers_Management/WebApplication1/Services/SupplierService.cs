using System.Runtime.CompilerServices;
using WebApplication1.DTO.SupplierDTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories.RepositoryInterfaces;
using WebApplication1.Services.ServiceInterfaces;

namespace WebApplication1.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<SupplierResponse> AddSupplier(SupplierAddRequest? supplierAddRequest)
        {
            
            if (supplierAddRequest == null) throw new ArgumentNullException(nameof(supplierAddRequest));
            Supplier? supplier = supplierAddRequest.ToSupplier();

            Supplier? supplierResponse = await _supplierRepository.AddSupplier(supplier);

            return supplierResponse.ToSupplierResponse();
        }

        public Task<bool> DeleteSupplier(Guid? id)
        {
            if (!id.HasValue) throw new ArgumentNullException("id");
            return _supplierRepository.DeleteSupplier(id);
        }

        public async Task<List<SupplierResponse>> GetAllSuppliers()
        {
            List<Supplier> suppliersList = await _supplierRepository.GetAllSuppliers();

            return suppliersList.Select(temp => temp.ToSupplierResponse()).ToList();
        }

        public async Task<SupplierResponse?> GetSupplierBySupplierID(Guid? id)
        {
            if (!id.HasValue) throw new ArgumentNullException("id");

            Supplier? supplier = await _supplierRepository.GetSupplierBySupplierID(id);
            if (supplier == null) return null;

            return supplier.ToSupplierResponse();
        }

        public async Task<SupplierResponse> UpdateSupplier(SupplierUpdateRequest? supplierUpdateRequest)
        {
            if (supplierUpdateRequest == null) throw new ArgumentNullException(nameof(supplierUpdateRequest));



            Supplier supplierMatching = supplierUpdateRequest.ToSupplier();

            Supplier supplierResponse = await _supplierRepository.UpdateSupplier(supplierMatching);

            return supplierResponse.ToSupplierResponse();
        }
    }
}
