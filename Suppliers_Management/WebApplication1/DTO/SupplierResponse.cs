using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using WebApplication1.CustomValidators;
using WebApplication1.Entities;

namespace WebApplication1.DTO
{
    public class SupplierResponse
    {
        public int Id { get; set; }  
        public string? Name { get; set; }    
        public int CategoryCode { get; set; }  
        public string? Tid { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int CountryCode { get; set; }  
        public bool IsActive { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(SupplierResponse)) return false;

            SupplierResponse supplier = (SupplierResponse)obj;
            return Id == supplier.Id && Name == supplier.Name && CategoryCode == supplier.CategoryCode && Tid == supplier.Tid && Address == supplier.Address && Phone == supplier.Phone && Email == supplier.Email && CountryCode == supplier.CountryCode && IsActive == supplier.IsActive;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"Supplier ID: {Id}, Supplier Name: {Name}, Category: {CategoryCode}, Taxpayer ID: {Tid}, Address: {Address}, Phone: {Phone}, Email: {Email}, Country: {CountryCode}, Active: {IsActive}";
        }

        public SupplierUpdateRequest ToSupplierUpdateRequest()
        {
            return new SupplierUpdateRequest() { Id = Id, Name = Name, CategoryCode = CategoryCode, Tid = Tid, Address = Address, Phone = Phone, Email = Email, CountryCode = CountryCode, IsActive = IsActive };
        }
    }


    public static class SupplierExtensions
    {
        public static SupplierResponse ToPersonResponse(this Supplier supplier)
        {
            //person => convert => PersonResponse
            return new SupplierResponse()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                CategoryCode = supplier.CategoryCode,
                Tid = supplier.Tid,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                CountryCode = supplier.CountryCode,
                IsActive = supplier.IsActive,
            };
        }
    }



}
