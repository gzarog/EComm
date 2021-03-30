using Ecomm.Api.Search.Interfaces;
using Ecomm.Api.Search.Models;
using Ecomm.Api.Search.RefitInterfaces;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecomm.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProductsService> logger;

        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<ProductsService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<ProductModel> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var productsApi = RestService.For<IProductsApi>(httpClientFactory.CreateClient("ProductsService"));
                var response = await productsApi.GetProductsAsync();
                if (response.IsSuccessStatusCode)
                {
                   
                    return (true, response.Content, null);

                }
                return (false, response.Content, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }

       
    }
}
