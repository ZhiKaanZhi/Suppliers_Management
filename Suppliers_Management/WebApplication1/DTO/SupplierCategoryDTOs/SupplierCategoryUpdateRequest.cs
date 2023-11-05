using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities;

namespace WebApplication1.DTO.SupplierCategoryDTOs
{
    public class SupplierCategoryUpdateRequest
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public string? Description { get; set; }


        public SupplierCategory toSupplierCategory()
        {
            return new SupplierCategory 
            { 
                CategoryId = CategoryId, 
                Description = Description 
            };
        }
    }
}
