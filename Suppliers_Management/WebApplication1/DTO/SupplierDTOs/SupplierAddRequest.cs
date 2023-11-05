using System.ComponentModel.DataAnnotations;
using WebApplication1.CustomValidators;
using WebApplication1.Entities;

namespace WebApplication1.DTO.SupplierDTOs
{
    public class SupplierAddRequest
    {
        [Required]
        public Guid SupplierId { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string? SupplierName { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]
        public string? Tid { get; set; }

        [StringLength(10, MinimumLength = 5)]
        public string? Address { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? CountryName { get; set; }

        public bool IsActive { get; set; }

        public List<SupplierCategory> Categories { get; set; } = new List<SupplierCategory>();
        public List<Country> Countries { get; set; } = new List<Country>();

        public Supplier ToSupplier()
        {
            return new Supplier()
            {
                SupplierId = SupplierId,
                SupplierName = SupplierName,
                CategoryId = Guid.Empty,
                Tid = Tid,
                Address = Address,
                Phone = Phone,
                Email = Email,
                CountryId = Guid.Empty,
                IsActive = IsActive
            };
        }
    }
}
