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
        public int CategoryId { get; set; }
        public string? Tid { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(SupplierResponse)) return false;

            SupplierResponse supplier = (SupplierResponse)obj;
            return SupplierId == supplier.SupplierId && SupplierName == supplier.SupplierName && CategoryId == supplier.CategoryId && Tid == supplier.Tid && Address == supplier.Address && Phone == supplier.Phone && Email == supplier.Email && CountryId == supplier.CountryId && IsActive == supplier.IsActive;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"Supplier ID: {SupplierId}, Supplier SupplierName: {SupplierName}, Category: {CategoryId}, Taxpayer ID: {Tid}, Address: {Address}, Phone: {Phone}, Email: {Email}, Country: {CountryId}, Active: {IsActive}";
        }

        public SupplierUpdateRequest ToSupplierUpdateRequest()
        {
            return new SupplierUpdateRequest() { SupplierId = SupplierId, SupplierName = SupplierName, CategoryId = CategoryId, Tid = Tid, Address = Address, Phone = Phone, Email = Email, CountryId = CountryId, IsActive = IsActive };
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
                CategoryId = supplier.CategoryId,
                Tid = supplier.Tid,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                CountryId = supplier.CountryId,
                IsActive = supplier.IsActive,
            };
        }
    }



}
