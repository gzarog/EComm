using EComm.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Api.Customers.Interfaces
{
     public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<CustomerModel> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, CustomerModel Customer, string ErrorMessage)> GetCustomerAsync( int id );
    }
}
