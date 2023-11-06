using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using System;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Entities;
using WebApplication1.Repositories.RepositoryInterfaces;

namespace WebApplication1.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly DatabaseContext _db;

        public SupplierRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<Supplier> AddSupplier(Supplier? supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier));
            }

            ValidationHelper.ModelValidation(supplier);

            supplier.SupplierId = Guid.NewGuid();
            _db.Add(supplier);
            await _db.SaveChangesAsync();
            return supplier;
        }

        public async Task<bool> DeleteSupplier(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Supplier? supplier = await _db.Suppliers.FirstOrDefaultAsync(temp => temp.SupplierId == id);
            if (supplier == null) { return false; }

            _db.Suppliers.Remove(_db.Suppliers.First(temp => temp.SupplierId == id));
            await _db.SaveChangesAsync();
            return true;
        }


        public async Task<List<Supplier>> GetAllSuppliers()
        {
            var suppliers = await _db.Suppliers
                                     .Include(s => s.Country)
                                     .Include(s => s.Category)
                                     .ToListAsync();
            return suppliers;
        }

        public async Task<Supplier?> GetSupplierBySupplierID(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            Supplier? supplier = await _db.Suppliers
                                          .Include(s => s.Country)
                                          .Include(s => s.Category)
                                          .FirstOrDefaultAsync(temp => temp.SupplierId == id);

            return supplier;
        }

        public async Task<Supplier> UpdateSupplier(Supplier? supplier)
        {
            if (supplier == null) { throw new ArgumentNullException(nameof(supplier)); }

            ValidationHelper.ModelValidation(supplier);

            Supplier? matchingSupplier = await _db.Suppliers
                .Include(s => s.Country)
                .Include(s => s.Category)
                .FirstOrDefaultAsync(temp => temp.SupplierId == supplier.SupplierId);

            if (matchingSupplier == null)
            {
                throw new ArgumentException("Given supplier id doesn't exist");
            }

            matchingSupplier.Address = supplier.Address;
            matchingSupplier.Email = supplier.Email;
            matchingSupplier.Phone = supplier.Phone;
            matchingSupplier.CategoryId = supplier.CategoryId;
            matchingSupplier.CountryId = supplier.CountryId;
            matchingSupplier.IsActive = supplier.IsActive;

            if (matchingSupplier.IsActive)
            {
                matchingSupplier.SupplierName = supplier.SupplierName;
                matchingSupplier.Tid = supplier.Tid;
            }

            await _db.SaveChangesAsync();

            return matchingSupplier;
        }
    }
}
