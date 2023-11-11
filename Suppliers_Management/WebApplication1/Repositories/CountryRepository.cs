using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Repositories.RepositoryInterfaces;

namespace WebApplication1.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DatabaseContext _db;

        public CountryRepository(DatabaseContext db)
        {
            _db = db;
        }


        public async Task<Country> AddCountry(Country? country)
        {
            if (country == null || country.CountryName == null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            ValidationHelper.ModelValidation(country);

            Country? existingCountry = await GetCountryByCountryName(country.CountryName);

            if (existingCountry == null) 
            {
                country.CountryId = Guid.NewGuid();
                _db.Add(country);
                await _db.SaveChangesAsync();
                return country;
            }
            throw new ArgumentException("Given country already exists");

        }

        public async Task<bool> DeleteCountry(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Country? country = await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryId == id);
            if (country == null) { return false; }

            _db.Countries.Remove(_db.Countries.First(temp => temp.CountryId == id));
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            var countries = await _db.Countries.ToListAsync();
            return countries;
        }

        public async Task<Country?> GetCountryByCountryID(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            Country? country = await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryId == id);

            if (country == null)
            {
                return null;
            }

            return country;
        }

        public async Task<Country?> GetCountryByCountryName(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            Country? country = await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryName == name || (temp.CountryName == null && name == null));

            return country;
        }

        public async Task<Country> UpdateCountry(Country? country)
        {
            if (country == null) { throw new ArgumentNullException(nameof(country)); }

            ValidationHelper.ModelValidation(country);

            Country? matchingCountry = await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryId == country.CountryId);
            if (matchingCountry == null)
            {
                throw new ArgumentException("Given country id doesn't exist");
            }

            matchingCountry.CountryId = country.CountryId;
            matchingCountry.CountryName = country.CountryName;
            matchingCountry.Suppliers = country.Suppliers;

            await _db.SaveChangesAsync();

            return matchingCountry;
        }
    }
}
