using AutoMapper;
using EComm.Api.Orders.Data;
using EComm.Api.Orders.Interfaces;
using EComm.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.OrderItems.Any())
            {
                dbContext.OrderItems.Add(new OrderItem() { 
                    Id=1,
                    OrderId=1,
                    ProductId =1,
                    Quantity =10,
                    UnitPrice=10,
               
                });
                dbContext.OrderItems.Add(new OrderItem()
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = 2,
                    Quantity = 10,
                    UnitPrice = 10,

                });

                dbContext.OrderItems.Add(new OrderItem()
                {
                    Id = 3,
                    OrderId = 2,
                    ProductId = 1,
                    Quantity = 10,
                    UnitPrice = 10,

                });
                dbContext.OrderItems.Add(new OrderItem()
                {
                    Id = 4,
                    OrderId = 2,
                    ProductId = 2,
                    Quantity = 10,
                    UnitPrice = 10,

                });

            }
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Total = 100,
                    Items = new List<OrderItem>()
                });

                dbContext.Orders.Add(new Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Total = 100,
                    Items = new List<OrderItem>()
                });


                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<OrderModel> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var Orders = await dbContext.Orders.Where(w => w.CustomerId == customerId).ToListAsync();
                if (Orders != null && Orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(Orders);
                    return (true, result, null);
                }
                return (false, null, "No Orders");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }


    }
}
