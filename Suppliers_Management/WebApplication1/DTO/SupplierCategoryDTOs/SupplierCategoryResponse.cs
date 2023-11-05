using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using WebApplication1.DTO.SupplierDTOs;
using WebApplication1.Entities;

namespace WebApplication1.DTO.SupplierCategoryDTOs
{
    public class SupplierCategoryResponse
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public string? Description { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(SupplierCategoryResponse)) return false;
            SupplierCategoryResponse supplierCategory = (SupplierCategoryResponse)obj;
            return CategoryId == supplierCategory.CategoryId && Description == supplierCategory.Description;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override string ToString()
        {
            return $"Supplier Category ID: {CategoryId}, Supplier Category Description: {Description}";
        }

        public SupplierCategoryUpdateRequest ToSupplierCategoryUpdateRequest()
        {
            return new SupplierCategoryUpdateRequest() { CategoryId = CategoryId, Description = Description};
        }

        public SupplierCategory ToSupplierCategory()
        {
            return new SupplierCategory() { CategoryId = CategoryId, Description = Description };
        }
    }
    public static class SupplierCategoryExtensions
    {
        public static SupplierCategoryResponse ToSupplierCategoryResponse(this SupplierCategory supplierCategory)
        {

            return new SupplierCategoryResponse()
            {
                CategoryId = supplierCategory.CategoryId,
                Description = supplierCategory.Description
            };
        }
    }
}

