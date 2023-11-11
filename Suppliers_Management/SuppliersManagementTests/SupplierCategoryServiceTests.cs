using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTO.SupplierCategoryDTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories.RepositoryInterfaces;
using WebApplication1.Services.ServiceInterfaces;
using WebApplication1.Services;

namespace SuppliersManagementTests
{
    public class SupplierCategoryServiceTests
    {
        private readonly Mock<ISupplierCategoryRepository> mockRepo;
        private readonly ISupplierCategoryService _supplierCategoryService;

        private static readonly Guid electronicsGuid = Guid.NewGuid();

        public SupplierCategoryServiceTests()
        {
            mockRepo = new Mock<ISupplierCategoryRepository>();
            _supplierCategoryService = new SupplierCategoryService(mockRepo.Object);

            // Mock setup for AddSupplierCategory
            mockRepo.Setup(repo => repo.AddSupplierCategory(It.IsAny<SupplierCategory>()))
                .ReturnsAsync((SupplierCategory supplierCategory) => {
                    if (supplierCategory.Description == "Electronics")
                    {
                        supplierCategory.CategoryId = electronicsGuid;
                    }
                    else
                    {
                        supplierCategory.CategoryId = Guid.NewGuid();
                    }
                    return supplierCategory;
                });

            // Mock setup for GetSupplierCategoryBySupplierCategoryID (specific for "Electronics")
            mockRepo.Setup(repo => repo.GetSupplierCategoryBySupplierCategoryID(electronicsGuid))
                .ReturnsAsync(new SupplierCategory { CategoryId = electronicsGuid, Description = "Electronics" });

            // Mock setup for GetCountryByCountryID (specific for "China")
            mockRepo.Setup(repo => repo.GetSupplierCategoryBySupplierCategoryID(electronicsGuid))
                .ReturnsAsync(new SupplierCategory { CategoryId = electronicsGuid, Description = "Electronics" });

            mockRepo.Setup(repo => repo.GetAllSupplierCategories())
                    .ReturnsAsync(new List<SupplierCategory>
                    {
                        new SupplierCategory
                        {
                            CategoryId = Guid.NewGuid(),
                            Description = "Category1"
                        },
                        new SupplierCategory
                        {
                            CategoryId = Guid.NewGuid(),
                            Description = "Category2"
                        },
                        new SupplierCategory
                        {
                            CategoryId = Guid.NewGuid(),
                            Description = "Category3"
                        },
                    });


        }

        #region AddSupplierCategory

        [Fact]
        public async Task AddSupplierCategory_NullSupplierCategory()
        {
            // Arrange
            SupplierCategoryAddRequest? request = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _supplierCategoryService.AddSupplierCategory(request);
            });
        }


        [Fact]
        public async Task AddSupplierCategory_ProperDetailsAsync()
        {
            // Arrange
            var request = new SupplierCategoryAddRequest { Description = "Electronics" };

            // Act
            var response = await _supplierCategoryService.AddSupplierCategory(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(electronicsGuid, response.CategoryId);
            Assert.Equal("Electronics", response.Description);
        }


        [Fact]
        public async Task AddSupplierCategory_SupplierCategoryDescriptionIsNullAsync()
        {
            //Arrange
            SupplierCategoryAddRequest? request = new SupplierCategoryAddRequest() { Description = null };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _supplierCategoryService.AddSupplierCategory(request);
            });
        }
        #endregion

        #region GetAllSupplierCategories

        [Fact]
        public async Task GetAllSupplierCategories_ReturnsMockedSupplierCategories()
        {
            // Arrange - Setup is already done in mockRepo to return specific supplierCategories

            // Act
            List<SupplierCategoryResponse> actualSupplierCategoryResponseList = await _supplierCategoryService.GetAllSupplierCategories();

            // Assert
            Assert.NotNull(actualSupplierCategoryResponseList);
            Assert.Equal(3, actualSupplierCategoryResponseList.Count);
            Assert.Contains(actualSupplierCategoryResponseList, c => c.Description == "Category1");
            Assert.Contains(actualSupplierCategoryResponseList, c => c.Description == "Category2");
            Assert.Contains(actualSupplierCategoryResponseList, c => c.Description == "Category3");
        }
        #endregion


        #region GetSupplierCategoryBySupplierCategoryId

        [Fact]
        public async Task GetSupplierCategoryBySupplierCategoryID_NullSupplierCategoryId_ThrowsArgumentNullExceptionAsync()
        {
            // Arrange
            Guid? SupplierCategoryID = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryID(SupplierCategoryID);
            });
        }


        [Fact]
        public async Task GetSupplierCategoryBySupplierCategoryID_ValidSupplierCategoryID()
        {
            // Arrange
            SupplierCategoryAddRequest SupplierCategory_add_request = new SupplierCategoryAddRequest { Description = "Electronics" };

            // Act
            SupplierCategoryResponse SupplierCategory_response_from_add = await _supplierCategoryService.AddSupplierCategory(SupplierCategory_add_request);
            SupplierCategoryResponse? SupplierCategory_response_from_get = await _supplierCategoryService.GetSupplierCategoryBySupplierCategoryID(SupplierCategory_response_from_add.CategoryId);

            // Assert
            Assert.NotNull(SupplierCategory_response_from_get);
            Assert.Equal(electronicsGuid, SupplierCategory_response_from_get.CategoryId);
            Assert.Equal("Electronics", SupplierCategory_response_from_get.Description);
        }
        #endregion

        #region DeleteSupplierCategory

        [Fact]
        public async Task DeleteSupplierCategory_ExistingCategoryId()
        {
            // Arrange
            var categoryId = Guid.NewGuid();

            // Setup the repository mock
            mockRepo.Setup(repo => repo.DeleteSupplierCategory(categoryId))
                    .ReturnsAsync(true);

            // Act
            var result = await _supplierCategoryService.DeleteSupplierCategory(categoryId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSupplierCategory_NonExistingCategoryId()
        {
            // Arrange
            var categoryId = Guid.NewGuid();

            // Setup the repository mock
            mockRepo.Setup(repo => repo.DeleteSupplierCategory(categoryId))
                    .ReturnsAsync(false);

            // Act
            var result = await _supplierCategoryService.DeleteSupplierCategory(categoryId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteSupplierCategory_NullCategoryId()
        {
            // Arrange
            Guid? categoryId = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _supplierCategoryService.DeleteSupplierCategory(categoryId);
            });
        }

        #endregion

    }
}
