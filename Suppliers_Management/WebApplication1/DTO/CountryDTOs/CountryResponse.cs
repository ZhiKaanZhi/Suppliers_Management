using System.ComponentModel.DataAnnotations;
using WebApplication1.DTO.SupplierCategoryDTOs;
using WebApplication1.Entities;

namespace WebApplication1.DTO.CountryDTOs
{
    public class CountryResponse
    {
        [Required]
        public Guid CountryId { get; set; }  // primary key

        [Required]
        public string? CountryName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(CountryResponse)) return false;
            CountryResponse country = (CountryResponse)obj;
            return CountryId == country.CountryId && CountryName == country.CountryName;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override string ToString()
        {
            return $"Country ID: {CountryId}, Country Name: {CountryName}";
        }

        public CountryUpdateRequest ToCountryUpdateRequest()
        {
            return new CountryUpdateRequest() { CountryId = CountryId, CountryName = CountryName };
        }
    }
    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {

            return new CountryResponse()
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName
            };
        }
    }
}

