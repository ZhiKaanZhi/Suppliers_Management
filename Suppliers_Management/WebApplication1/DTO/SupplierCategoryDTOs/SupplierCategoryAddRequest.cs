using System.ComponentModel.DataAnnotations;
using WebApplication1.DTO.SupplierDTOs;

namespace WebApplication1.DTO.SupplierCategoryDTOs
{
    public class SupplierCategoryAddRequest
    {
        [Required]
        public Guid CategoryId { get; set; }  

        [Required]
        public string? Description { get; set; }

        public SupplierCategoryAddRequest toSupplierCategoryAddRequest()
        {
            return new SupplierCategoryAddRequest()
            {
                CategoryId = CategoryId,
                Description = Description
            };
        }
    }
}
