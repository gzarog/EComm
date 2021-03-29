using AutoMapper;
using EComm.Api.Customers.Data;
using EComm.Api.Customers.Interfaces;
using EComm.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext , ILogger<CustomersProvider> logger , IMapper  mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Customer() { Id = 1, Name = "zaro", Address = "aaaaaaaaaaaaaa" });
                dbContext.Customers.Add(new Customer() { Id = 2, Name = "maria", Address = "bbbbbbbbbb" });
                dbContext.Customers.Add(new Customer() { Id = 3, Name = "sofia", Address = "cccccccccc" });
                dbContext.Customers.Add(new Customer() { Id = 4, Name = "giorgos", Address = "dddddddddddddd" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerModel> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if(customers != null && customers.Any())
                {
                   var result = mapper.Map<IEnumerable<Customer> , IEnumerable<CustomerModel>> (customers);
                    return (true, result, null);
                }
                return (false, null, "No customers");
            }
            catch (Exception ex )
            {
                logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }   
        }

        public async Task<(bool IsSuccess, CustomerModel Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(f=>f.Id == id);
                if (customer != null )
                {
                    var result = mapper.Map<Customer, CustomerModel>(customer);
                    return (true, result, null);
                }
                return (false, null, "No Customer");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
