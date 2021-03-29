using AutoMapper;
using EComm.Api.Customers.Data;
using EComm.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Api.Customers.AutoMapperProfiles
{
    public class CustomersProfile :Profile
    {
        public CustomersProfile()
        {
            CreateMap<Customer, CustomerModel> ();
        }
    }
}
