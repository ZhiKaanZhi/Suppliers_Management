using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities;

namespace WebApplication1.DTO.CountryDTOs
{
    public class CountryAddRequest
    {
        [Required]
        public Guid CountryId { get; set; }  // primary key

        [Required]
        public string? CountryName { get; set; }

        public Country toCountry() 
        { 
            return new Country 
            { 
                CountryId = CountryId,
                CountryName = CountryName 
            };
        }
    }
}
