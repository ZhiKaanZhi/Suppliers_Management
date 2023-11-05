using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Repositories.RepositoryInterfaces;

namespace WebApplication1.Repositories
{
    public class SupplierCategoryRepository : ISupplierCategoryRepository
    {

        private readonly DatabaseContext _db;

        public SupplierCategoryRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<SupplierCategory> AddSupplierCategory(SupplierCategory? supplierCategory)
        {
            if (supplierCategory == null) throw new ArgumentNullException(nameof(supplierCategory));

            ValidationHelper.ModelValidation(supplierCategory);

            supplierCategory.CategoryId = Guid.NewGuid();
            _db.Add(supplierCategory);
            await _db.SaveChangesAsync();
            return supplierCategory;
        }

        public async Task<bool> DeleteSupplierCategory(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            SupplierCategory? supplierCategory = await _db.SupplierCategories.FirstOrDefaultAsync(temp => temp.CategoryId == id);
            if (supplierCategory == null) { return false; }

            _db.SupplierCategories.Remove(_db.SupplierCategories.First(temp => temp.CategoryId == id));
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<SupplierCategory>> GetAllSupplierCategories()
        {
            var supplierCategories = await _db.SupplierCategories.ToListAsync();
            return supplierCategories;
        }

        public async Task<SupplierCategory?> GetSupplierCategoryBySupplierCategoryID(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            SupplierCategory? supplierCategory = await _db.SupplierCategories.FirstOrDefaultAsync(temp => temp.CategoryId == id);

            if (supplierCategory == null)
            {
                return null;
            }

            return supplierCategory;
        }

        public async Task<SupplierCategory?> GetSupplierCategoryBySupplierCategoryName(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            SupplierCategory? supplierCategory = await _db.SupplierCategories.FirstOrDefaultAsync(temp => temp.Description == name || (temp.Description == null && name == null));

            return supplierCategory;
        }

        public async Task<SupplierCategory> UpdateSupplierCategory(SupplierCategory? supplierCategory)
        {
            if (supplierCategory == null) { throw new ArgumentNullException(nameof(supplierCategory)); }

            ValidationHelper.ModelValidation(supplierCategory);

            SupplierCategory? matchingSupplierCategory = await _db.SupplierCategories.FirstOrDefaultAsync(temp => temp.CategoryId == supplierCategory.CategoryId);

            if (matchingSupplierCategory == null)
            {
                throw new ArgumentException("Given supplier category id doesn't exist");
            }

            matchingSupplierCategory.CategoryId = supplierCategory.CategoryId;
            matchingSupplierCategory.Description = supplierCategory.Description;
            matchingSupplierCategory.Suppliers = supplierCategory.Suppliers;

            await _db.SaveChangesAsync();

            return matchingSupplierCategory;
        }
    }
}
