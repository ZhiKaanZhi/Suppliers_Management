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
        private readonly ISupplierCategoryService _supplierCategoryService;
        private readonly ICountryService _countryService;

        public SupplierService(ISupplierRepository supplierRepository, ISupplierCategoryService supplierCategoryService, ICountryService countryService)
        {
            _supplierRepository = supplierRepository;
            _supplierCategoryService = supplierCategoryService;
            _countryService = countryService;
        }

        public async Task<SupplierResponse> AddSupplier(SupplierAddRequest? supplierAddRequest)
        {
            
            if (supplierAddRequest == null) throw new ArgumentNullException(nameof(supplierAddRequest));

            Supplier? supplier = supplierAddRequest.ToSupplier();

            var category = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryName(supplierAddRequest.SupplierName);

            if(category != null)
            {
                supplier.CategoryId = category.CategoryId;
            }

            var country = await _countryService.GetCountryByCountryName(supplierAddRequest.CountryName);
            if(country != null)
            {
                supplier.CountryId = country.CountryId;
            }

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

            var category = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryName(supplierUpdateRequest.SupplierName);

            if (category != null)
            {
                supplierMatching.CategoryId = category.CategoryId;
            }

            var country = await _countryService.GetCountryByCountryName(supplierUpdateRequest.CountryName);
            if (country != null)
            {
                supplierMatching.CountryId = country.CountryId;
            }

            Supplier supplierResponse = await _supplierRepository.UpdateSupplier(supplierMatching);

            return supplierResponse.ToSupplierResponse();
        }
    }
}
