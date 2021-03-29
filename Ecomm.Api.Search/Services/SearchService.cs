using Ecomm.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customeId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customeId);
            var customersResult = await customersService.GetCustomersAsync(customeId);
            var productsResult = await productsService.GetProductsAsync();
            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ?
                            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            "Product not Available ";
                    }
                }

                var result = new
                {
                    Orders = ordersResult.Orders,
                    Customers = customersResult.IsSuccess ?
                                customersResult.Customer :
                                new { Name = "Customer not Available " }

                };
                return (true, result);
            }
            return (false, null);

        }
    }
}
