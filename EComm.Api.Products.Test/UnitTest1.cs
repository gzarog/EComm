using EComm.Api.Products.Data;
using EComm.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using EComm.Api.Products.AutoMapperProfiles;
using AutoMapper;

namespace EComm.Api.Products.Test
{
    public class UnitTest1
    {
        [Fact]
        public async void GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts)).Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductsProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext,null,mapper);
            var result = await productsProvider.GetProductsAsync();
            Assert.True(result.IsSuccess);
            Assert.True(result.Products !=null);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public async void GetProductsReturnProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnProductUsingValidId)).Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductsProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var result = await productsProvider.GetProductAsync(10);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Product);
            Assert.True(result.Product.Id ==10);
            Assert.Null(result.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 0; i < 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i+10,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)

                });
         
            }
            dbContext.SaveChanges();

        }
    }
}
