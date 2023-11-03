using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using WebApplication1.CustomValidators;

namespace WebApplication1.Entities
{
    public class Supplier
    {
        public int Id { get; set; }  // primary key

        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string? Name { get; set; }

        [Required]
        public int CategoryCode { get; set; }  // foreign key to Category lookup table

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
        public int CountryCode { get; set; }  // foreign key to Country lookup table

        public bool IsActive { get; set; }  // true for Active, false for Inactive

        
        public virtual SupplierCategory? Category { get; set; }  // Link to the Category lookup table ManyToOne relationship
        public virtual Country? Country { get; set; }   // Link to the Country lookup table
    }
}
