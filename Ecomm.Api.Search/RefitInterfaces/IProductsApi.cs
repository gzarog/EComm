
using Ecomm.Api.Search.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm.Api.Search.RefitInterfaces
{
    public interface IProductsApi
    {
        [Get(path: "/api/products")]
        Task<ApiResponse<IEnumerable<ProductModel>>> GetProductsAsync();
        [Get(path: "/api/products/{id}")]
        Task<ApiResponse<ProductModel>> GetProductAsync(int id);
    }
}
