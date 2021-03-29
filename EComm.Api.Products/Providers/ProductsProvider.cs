using AutoMapper;
using EComm.Api.Products.Data;
using EComm.Api.Products.Interfaces;
using EComm.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext , ILogger<ProductsProvider> logger , IMapper  mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Product() { Id = 1, Name = "Keyboard", Price = 10, Inventory = 100 });
                dbContext.Products.Add(new Product() { Id = 2, Name = "Monitor", Price = 120, Inventory = 100 });
                dbContext.Products.Add(new Product() { Id = 3, Name = "Mouse", Price = 5, Inventory = 100 });
                dbContext.Products.Add(new Product() { Id = 4, Name = "Chair", Price = 50, Inventory = 100 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductModel> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var Products = await dbContext.Products.ToListAsync();
                if( Products !=null && Products.Any())
                {
                   var result = mapper.Map<IEnumerable<Product> , IEnumerable<ProductModel>> (Products);
                    return (true, result, null);
                }
                return (false, null, "No Products");
            }
            catch (Exception ex )
            {
                logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }   
        }

        public async Task<(bool IsSuccess, ProductModel Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var Product = await dbContext.Products.FirstOrDefaultAsync(f=>f.Id == id);
                if (Product != null )
                {
                    var result = mapper.Map<Product, ProductModel>(Product);
                    return (true, result, null);
                }
                return (false, null, "No Product");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
