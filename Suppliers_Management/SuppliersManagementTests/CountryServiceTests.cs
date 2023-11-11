using Moq;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTO.CountryDTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories.RepositoryInterfaces;
using WebApplication1.Services;
using WebApplication1.Services.ServiceInterfaces;

namespace SuppliersManagementTests
{
    public class CountryServiceTests
    {
        private readonly Mock<ICountryRepository> mockRepo;
        private readonly ICountryService _countryService;

        private static readonly Guid chinaGuid = Guid.NewGuid();

        public CountryServiceTests()
        {
            mockRepo = new Mock<ICountryRepository>();
            _countryService = new CountryService(mockRepo.Object);


            // Mock setup for AddCountry
            mockRepo.Setup(repo => repo.AddCountry(It.IsAny<Country>()))
                .ReturnsAsync((Country country) => {
                    // Assign chinaGuid if the country being added is "China"
                    if (country.CountryName == "China")
                    {
                        country.CountryId = chinaGuid;
                    }
                    else
                    {
                        country.CountryId = Guid.NewGuid();
                    }
                    return country;
                });

            // Mock setup for GetCountryByCountryID (specific for "China")
            mockRepo.Setup(repo => repo.GetCountryByCountryID(chinaGuid))
                .ReturnsAsync(new Country { CountryId = chinaGuid, CountryName = "China" });

            mockRepo.Setup(repo => repo.GetAllCountries())
                    .ReturnsAsync(new List<Country>
                    {
                        new Country
                        {
                            CountryId = Guid.NewGuid(),
                            CountryName = "Country1"
                        },
                        new Country
                        {
                            CountryId = Guid.NewGuid(),
                            CountryName = "Country2"
                        },
                         new Country
                        {
                            CountryId = Guid.NewGuid(),
                            CountryName = "Country3"
                        }
                    });

        }

        #region AddCountry

        [Fact]
        public async Task AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _countryService.AddCountry(request);
            });
        }

        [Fact]
        public async Task AddCountry_CountryNameIsNullAsync()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = null };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _countryService.AddCountry(request);
            });
        }

        /*[Fact]
        public async Task AddCountry_DuplicateCountryNameAsync()
        {
            //Arrange
            CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "USA" };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _countryService.AddCountry(request1);
                await _countryService.AddCountry(request2);
            });
        }*/

        [Fact]
        public async Task AddCountry_ProperCountryDetailsAsync()
        {
            // Arrange
            CountryAddRequest request = new CountryAddRequest { CountryName = "Japan" };

            // Act
            CountryResponse response = await _countryService.AddCountry(request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.CountryId != Guid.Empty);
            Assert.Equal("Japan", response.CountryName);
        }

        #endregion

        #region GetAllCountries

        [Fact]
        public async Task GetAllCountries_ReturnsMockedCountries()
        {
            // Arrange - Setup is already done in mockRepo to return specific countries

            // Act
            List<CountryResponse> actualCountryResponseList = await _countryService.GetAllCountries();

            // Assert
            Assert.NotNull(actualCountryResponseList);
            Assert.Equal(3, actualCountryResponseList.Count);
            Assert.Contains(actualCountryResponseList, c => c.CountryName == "Country1");
            Assert.Contains(actualCountryResponseList, c => c.CountryName == "Country2");
            Assert.Contains(actualCountryResponseList, c => c.CountryName == "Country3");
        }
        #endregion


        #region GetCountryByCountryId

        [Fact]
        public async Task GetCountryByCountryID_NullCountryId_ThrowsArgumentNullExceptionAsync()
        {
            // Arrange
            Guid? countryID = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _countryService.GetCountryByCountryID(countryID);
            });
        }


        [Fact]
        public async Task GetCountryByCountryID_ValidCountryID()
        {
            // Arrange
            CountryAddRequest country_add_request = new CountryAddRequest { CountryName = "China" };

            // Act
            CountryResponse country_response_from_add = await _countryService.AddCountry(country_add_request);
            CountryResponse? country_response_from_get = await _countryService.GetCountryByCountryID(country_response_from_add.CountryId);

            // Assert
            Assert.NotNull(country_response_from_get);
            Assert.Equal(chinaGuid, country_response_from_get.CountryId);
            Assert.Equal("China", country_response_from_get.CountryName);
        }
        #endregion

        #region DeleteCountry

        [Fact]
        public async Task DeleteCountry_ExistingCategoryId()
        {
            // Arrange
            var categoryId = Guid.NewGuid();

            // Setup the repository mock
            mockRepo.Setup(repo => repo.DeleteCountry(categoryId))
                    .ReturnsAsync(true);

            // Act
            var result = await _countryService.DeleteCountry(categoryId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCountry_NonExistingCategoryId()
        {
            // Arrange
            var categoryId = Guid.NewGuid();

            // Setup the repository mock
            mockRepo.Setup(repo => repo.DeleteCountry(categoryId))
                    .ReturnsAsync(false);

            // Act
            var result = await _countryService.DeleteCountry(categoryId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteCountry_NullCategoryId()
        {
            // Arrange
            Guid? categoryId = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _countryService.DeleteCountry(categoryId);
            });
        }

        #endregion
    }

}
