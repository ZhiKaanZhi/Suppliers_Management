using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    [Table("SupplierCategories")]
    public class SupplierCategory
    {
        [Key]
        public int CategoryId { get; set; }  // primary key

        [Required]
        public string? Description { get; set; }

        // Navigation Property
        public virtual ICollection<Supplier>? Suppliers { get; set; }  // Link back to suppliers
    }
}
