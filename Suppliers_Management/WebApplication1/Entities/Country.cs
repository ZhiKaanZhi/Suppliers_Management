using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class Country
    {
        public int CountryCode { get; set; }  // primary key

        [Required]
        public string? CountryName { get; set; }

        // Navigation Property
        public virtual ICollection<Supplier>? Suppliers { get; set; }  // Link back to suppliers
    }
}
}
