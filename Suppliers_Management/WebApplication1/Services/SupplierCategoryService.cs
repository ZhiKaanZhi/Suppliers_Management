using WebApplication1.DTO.SupplierCategoryDTOs;
using WebApplication1.DTO.SupplierDTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories;
using WebApplication1.Repositories.RepositoryInterfaces;
using WebApplication1.Services.ServiceInterfaces;

namespace WebApplication1.Services
{
    public class SupplierCategoryService : ISupplierCategoryService
    {
        private readonly ISupplierCategoryRepository _supplierCategoryRepository;

        public SupplierCategoryService(ISupplierCategoryRepository supplierCategoryRepository)
        {
            _supplierCategoryRepository = supplierCategoryRepository;
        }

        public async Task<SupplierCategoryResponse> AddSupplierCategory(SupplierCategoryAddRequest? supplierCategoryAddRequest)
        {
            if (supplierCategoryAddRequest == null) throw new ArgumentNullException(nameof(supplierCategoryAddRequest));
            SupplierCategory? supplierCategory = supplierCategoryAddRequest.toSupplierCategory();

            SupplierCategory? supplierCategoryResponse = await _supplierCategoryRepository.AddSupplierCategory(supplierCategory);

            return supplierCategoryResponse.ToSupplierCategoryResponse();
        }

        public Task<bool> DeleteSupplierCategory(Guid? id)
        {
            if (!id.HasValue) throw new ArgumentNullException("id");
            return _supplierCategoryRepository.DeleteSupplierCategory(id);
        }

        public async Task<List<SupplierCategoryResponse>> GetAllSupplierCategories()
        {
            List<SupplierCategory> supplierCategoriesList = await _supplierCategoryRepository.GetAllSupplierCategories();

            return supplierCategoriesList.Select(temp => temp.ToSupplierCategoryResponse()).ToList();
        }

        public async Task<SupplierCategoryResponse?> GetSupplierCategoryBySupplierCategoryID(Guid? id)
        {
            if (!id.HasValue) throw new ArgumentNullException("id");

            SupplierCategory? supplierCategory = await _supplierCategoryRepository.GetSupplierCategoryBySupplierCategoryID(id);
            if (supplierCategory == null) return null;

            return supplierCategory.ToSupplierCategoryResponse();
        }

        public async Task<SupplierCategoryResponse?> GetSupplierCategoryBySupplierCategoryName(string? name)
        {
            if(name == null) throw new ArgumentNullException("name");

            SupplierCategory? supplierCategory = await _supplierCategoryRepository.GetSupplierCategoryBySupplierCategoryName(name);

            if (supplierCategory == null) return null;

            return supplierCategory.ToSupplierCategoryResponse();
        }

        public async Task<SupplierCategoryResponse> UpdateSupplierCategory(SupplierCategoryUpdateRequest? supplierCategoryUpdateRequest)
        {
            if (supplierCategoryUpdateRequest == null) throw new ArgumentNullException(nameof(supplierCategoryUpdateRequest));



            SupplierCategory supplierCategoryMatching = supplierCategoryUpdateRequest.toSupplierCategory();

            SupplierCategory supplierCategoryResponse = await _supplierCategoryRepository.UpdateSupplierCategory(supplierCategoryMatching);

            return supplierCategoryResponse.ToSupplierCategoryResponse();
        }
    }
}
