using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using WebApplication1.CustomValidators;
using WebApplication1.Entities;

namespace WebApplication1.DTO.SupplierDTOs
{
    public class SupplierResponse
    {
        public Guid SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public String? CategoryName { get; set; }
        public string? Tid { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public String? CountryName { get; set; }
        public bool IsActive { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(SupplierResponse)) return false;

            SupplierResponse supplier = (SupplierResponse)obj;
            return SupplierId == supplier.SupplierId && SupplierName == supplier.SupplierName && CategoryName == supplier.CategoryName && Tid == supplier.Tid && Address == supplier.Address && Phone == supplier.Phone && Email == supplier.Email && CountryName == supplier.CountryName && IsActive == supplier.IsActive;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"Supplier ID: {SupplierId}, Supplier SupplierName: {SupplierName}, Category: {CategoryName}, Taxpayer ID: {Tid}, Address: {Address}, Phone: {Phone}, Email: {Email}, Country: {CountryName}, Active: {IsActive}";
        }

        public SupplierUpdateRequest ToSupplierUpdateRequest()
        {
            return new SupplierUpdateRequest() { SupplierId = SupplierId, SupplierName = SupplierName, Tid = Tid, Address = Address, Phone = Phone, Email = Email, IsActive = IsActive };
        }
    }


    public static class SupplierExtensions
    {
        public static SupplierResponse ToSupplierResponse(this Supplier supplier)
        {

            return new SupplierResponse()
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                CategoryName = supplier.Category?.Description,
                Tid = supplier.Tid,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                CountryName = supplier.Country?.CountryName,
                IsActive = supplier.IsActive,
            };
        }
    }
}
