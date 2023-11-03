using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class SupplierCategory
    {
        public int CategoryCode { get; set; }  // primary key

        [Required]
        public string? Description { get; set; }

        // Navigation Property
        public virtual ICollection<Supplier>? Suppliers { get; set; }  // Link back to suppliers
    }
}
}
