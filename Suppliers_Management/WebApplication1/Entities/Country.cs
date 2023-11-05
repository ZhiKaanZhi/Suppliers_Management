using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }  // primary key

        [Required]
        public string? CountryName { get; set; }

        // Navigation Property
        public virtual ICollection<Supplier>? Suppliers { get; set; }  // Link back to suppliers
    }
}
