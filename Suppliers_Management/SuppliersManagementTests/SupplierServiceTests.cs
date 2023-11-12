using Azure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTO.CountryDTOs;
using WebApplication1.DTO.SupplierCategoryDTOs;
using WebApplication1.DTO.SupplierDTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories.RepositoryInterfaces;
using WebApplication1.Services;
using WebApplication1.Services.ServiceInterfaces;

namespace SuppliersManagementTests
{
    public class SupplierServiceTests
    {
        private readonly Mock<ISupplierRepository> mockSupplierRepository;
        private readonly Mock<ISupplierCategoryService> mockSupplierCategoryService;
        private readonly Mock<ICountryService> mockCountryService;
        private readonly Mock<INotificationService> mockNotificationService;
        private readonly ISupplierService _supplierService;

        public SupplierServiceTests()
        {
            mockSupplierRepository = new Mock<ISupplierRepository>();
            mockSupplierCategoryService = new Mock<ISupplierCategoryService>();
            mockCountryService = new Mock<ICountryService>();
            mockNotificationService = new Mock<INotificationService>();

            _supplierService = new SupplierService(mockSupplierRepository.Object, mockSupplierCategoryService.Object, mockCountryService.Object, mockNotificationService.Object);
        }

        #region AddSupplier
        [Fact]
        public async Task AddSupplier_NullSupplier()
        {
            //Arrange
            SupplierAddRequest? request = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _supplierService.AddSupplier(request);
            });
        }

        [Fact]
        public async Task AddSupplier_NullSupplierArgs()
        {
            //Arrange
            SupplierAddRequest? request = new SupplierAddRequest()
            {
                SupplierName = null,
                CategoryName = null,
                Tid = null,
                Address = null,
                Phone = null,
                Email = null,
                CountryName = null,
                IsActive = false,
            };

            mockSupplierRepository.Setup(repo => repo.AddSupplier(It.IsAny<Supplier>()))
                .ReturnsAsync((Supplier supplier) =>
                {
                    supplier.SupplierId = Guid.NewGuid();
                    return supplier;
                });

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _supplierService.AddSupplier(request);
            });
        }

        [Fact]
        public async Task AddSupplier_ProperSupplierDetailsAsync()
        {
            //Arrange
            SupplierAddRequest? request = new SupplierAddRequest()
            {
                SupplierName = "Supplier_1",
                CategoryName = "Category_1",
                Tid = "011111113",
                Address = "Address_1",
                Phone = "0123456789",
                Email = "supplier_1@suppliers.com",
                CountryName = "Country_1",
                IsActive = true,
            };

           

            var categoryId = Guid.NewGuid();
            mockSupplierCategoryService.Setup(serv => serv.GetSupplierCategoryBySupplierCategoryName("Category_1"))
                .ReturnsAsync(new SupplierCategoryResponse 
                { 
                    CategoryId = categoryId,
                    Description = "Category_1"
                });

            var countryId = Guid.NewGuid();
            mockCountryService.Setup(serv => serv.GetCountryByCountryName("Country_1"))
                .ReturnsAsync(new CountryResponse
                {
                    CountryId = countryId,
                    CountryName = "Country_1"
                });

            var expectedSupplierId = Guid.NewGuid();
            mockSupplierRepository.Setup(repo => repo.AddSupplier(It.IsAny<Supplier>()))
                .ReturnsAsync((Supplier supplier) =>
                {
                    supplier.SupplierId = expectedSupplierId;
                    supplier.CategoryId = categoryId;
                    supplier.CountryId = countryId;
                    supplier.Country = new Country()
                    {
                        CountryId = countryId,
                        CountryName = "Country_1"
                    };
                    supplier.Category = new SupplierCategory()
                    {
                        CategoryId = categoryId,
                        Description = "Category_1"
                    };
                    return supplier;
                });


            //Act
            var response = await _supplierService.AddSupplier(request);

            //Assert
            Assert.NotNull(response);
            Assert.True(response.SupplierId != Guid.Empty);
            Assert.Equal(expectedSupplierId, response.SupplierId);
            Assert.Equal(request.SupplierName, response.SupplierName);
            Assert.Equal(request.CountryName, response.CountryName);
            Assert.Equal(request.CategoryName, response.CategoryName);
            Assert.Equal(request.Tid, response.Tid);
            Assert.Equal(request.Address, response.Address);
            Assert.Equal(request.Phone, response.Phone);
            Assert.Equal(request.Email, response.Email);
            Assert.Equal(request.IsActive, response.IsActive);
        }

        #endregion

        #region GetAllSuppliers

        [Fact]
        public async Task GetAllSuppliers_ReturnsMockedSuppliers()
        {
            //Arrange
            var categoryId1 = Guid.NewGuid();
            var categoryId2 = Guid.NewGuid();
            var categoryId3 = Guid.NewGuid();
            var countryId1 = Guid.NewGuid();
            var countryId2 = Guid.NewGuid();
            var countryId3 = Guid.NewGuid();
            mockSupplierRepository.Setup(repo => repo.GetAllSuppliers())
                .ReturnsAsync(new List<Supplier>
                {
                    new Supplier
                    {
                        SupplierId = Guid.NewGuid(),
                        SupplierName = "Supplier_1",
                        CategoryId = categoryId1,
                        CountryId = countryId1,
                        Tid = "123456789",
                        Address = "Address_1",
                        Phone = "0123456789",
                        Email = "supplier1@suppliers.com",
                        IsActive = true
                    },

                    new Supplier
                    {
                        SupplierId = Guid.NewGuid(),
                        SupplierName = "Supplier_2",
                        CategoryId = categoryId2,
                        CountryId = countryId2,
                        Tid = "123456789",
                        Address = "Address_2",
                        Phone = "0123456789",
                        Email = "supplier2@suppliers.com",
                        IsActive = false
                    },

                    new Supplier
                    {
                        SupplierId = Guid.NewGuid(),
                        SupplierName = "Supplier_3",
                        CategoryId = categoryId3,
                        CountryId = countryId3,
                        Tid = "123456789",
                        Address = "Address_3",
                        Phone = "0123456789",
                        Email = "supplier3@suppliers.com",
                        IsActive = true
                    }
                });

            //Act
            List<SupplierResponse> response = await _supplierService.GetAllSuppliers();

            //Assert
            Assert.NotNull(response);
            Assert.Equal(3, response.Count);
            Assert.Contains(response, c => (c.SupplierName == "Supplier_1") && (c.Tid == "123456789") && (c.Address == "Address_1") && (c.Phone == "0123456789") && (c.Email == "supplier1@suppliers.com") && (c.IsActive == true));
            Assert.Contains(response, c => (c.SupplierName == "Supplier_2") && (c.Tid == "123456789") && (c.Address == "Address_2") && (c.Phone == "0123456789") && (c.Email == "supplier2@suppliers.com") && (c.IsActive == false));
            Assert.Contains(response, c => (c.SupplierName == "Supplier_3") && (c.Tid == "123456789") && (c.Address == "Address_3") && (c.Phone == "0123456789") && (c.Email == "supplier3@suppliers.com") && (c.IsActive == true));
        }
        #endregion

        #region GetSupplierBySupplierId
        [Fact]
        public async Task GetSupplierBySupplierID_NullSupplierId_ValidSupplier()
        {
            // Arrange
            SupplierAddRequest? request = new SupplierAddRequest()
            {
                SupplierName = "Supplier_1",
                CategoryName = "Category_1",
                Tid = "011111113",
                Address = "Address_1",
                Phone = "0123456789",
                Email = "supplier_1@suppliers.com",
                CountryName = "Country_1",
                IsActive = true
            };

            var categoryId = Guid.NewGuid();
            mockSupplierCategoryService.Setup(serv => serv.GetSupplierCategoryBySupplierCategoryName("Category_1"))
                .ReturnsAsync(new SupplierCategoryResponse
                {
                    CategoryId = categoryId,
                    Description = "Category_1"
                });

            var countryId = Guid.NewGuid();
            mockCountryService.Setup(serv => serv.GetCountryByCountryName("Country_1"))
                .ReturnsAsync(new CountryResponse
                {
                    CountryId = countryId,
                    CountryName = "Country_1"
                });

            var expectedSupplierId = Guid.NewGuid();
            mockSupplierRepository.Setup(repo => repo.AddSupplier(It.IsAny<Supplier>()))
                .ReturnsAsync((Supplier supplier) =>
                {
                    supplier.SupplierId = expectedSupplierId;
                    supplier.CategoryId = categoryId;
                    supplier.CountryId = countryId;
                    supplier.Country = new Country()
                    {
                        CountryId = countryId,
                        CountryName = "Country_1"
                    };
                    supplier.Category = new SupplierCategory()
                    {
                        CategoryId = categoryId,
                        Description = "Category_1"
                    };
                    return supplier;
                });

            mockSupplierRepository.Setup(repo => repo.GetSupplierBySupplierID(expectedSupplierId))
                .ReturnsAsync(new Supplier
                {
                    SupplierId = expectedSupplierId,
                    SupplierName = "Supplier_1",
                    CategoryId = categoryId,
                    Tid = "011111113",
                    Address = "Address_1",
                    Phone = "0123456789",
                    Email = "supplier_1@suppliers.com",
                    CountryId = countryId,
                    IsActive = true,
                    Country = new Country() { CountryId = countryId, CountryName = "Country_1" },
                    Category = new SupplierCategory() { CategoryId = categoryId, Description = "Category_1"}
                });

            // Act
            SupplierResponse supplierResponseFromAdd = await _supplierService.AddSupplier(request);
            SupplierResponse? supplierResponseFromGet = await _supplierService.GetSupplierBySupplierID(supplierResponseFromAdd.SupplierId);

            // Assert
            Assert.NotNull(supplierResponseFromGet);
            Assert.Equal(expectedSupplierId, supplierResponseFromGet.SupplierId);
            Assert.Equal("Supplier_1", supplierResponseFromGet.SupplierName);
            Assert.Equal("Country_1", supplierResponseFromGet.CountryName);
            Assert.Equal("Category_1", supplierResponseFromGet.CategoryName);
            Assert.Equal("011111113", supplierResponseFromGet.Tid);
            Assert.Equal("Address_1", supplierResponseFromGet.Address);
            Assert.Equal("0123456789", supplierResponseFromGet.Phone);
            Assert.Equal("supplier_1@suppliers.com", supplierResponseFromGet.Email);
            Assert.True(supplierResponseFromGet.IsActive);
        }


        [Fact]
        public async Task GetSupplierBySupplierID_NullSupplierId_ThrowsArgumentNullExceptionAsync()
        {
            //Arrange
            Guid? supplierId = null;

            //Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _supplierService.GetSupplierBySupplierID(supplierId);
            });
        }

        #endregion

        #region DeleteSupplier

        [Fact]
        public async Task DeleteSupplier_ExistingSupplierId()
        {
            //Arrange
            var supplierId = Guid.NewGuid();

            mockSupplierRepository.Setup(repo => repo.DeleteSupplier(supplierId))
                .ReturnsAsync(true);

            //Act
            var result = await _supplierService.DeleteSupplier(supplierId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSupplier_NonExistingSupplierId()
        {
            //Arrange
            var supplierId = Guid.NewGuid();

            mockSupplierRepository.Setup(repo => repo.DeleteSupplier(supplierId))
                .ReturnsAsync(false);

            //Act
            var result = await _supplierService.DeleteSupplier(supplierId);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteSupplier_NullSupplierId()
        {
            //Arrange
            Guid? supplierId = null;

            //Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _supplierService.DeleteSupplier(supplierId);
            });
        }

        #endregion
    }
}
