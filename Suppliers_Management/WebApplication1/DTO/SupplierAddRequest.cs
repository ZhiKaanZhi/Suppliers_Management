using System.ComponentModel.DataAnnotations;
using WebApplication1.CustomValidators;
using WebApplication1.Entities;

namespace WebApplication1.DTO
{
    public class SupplierAddRequest
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string? Name { get; set; }

        [Required]
        public int CategoryCode { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]
        [TidValidation]
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
        public int CountryCode { get; set; }
        public bool IsActive { get; set; }


        public Supplier ToSupplier()
        {
            return new Supplier()
            {
                Id = Id,
                Name = Name,
                CategoryCode = CategoryCode,
                Tid = Tid,
                Address = Address,
                Phone = Phone,
                Email = Email,
                CountryCode = CountryCode,
                IsActive = IsActive
            };
        }
    }
}
