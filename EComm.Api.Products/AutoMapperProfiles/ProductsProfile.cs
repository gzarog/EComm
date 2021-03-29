using AutoMapper;
using EComm.Api.Products.Data;
using EComm.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Api.Products.AutoMapperProfiles
{
    public class ProductsProfile :Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductModel> ();
        }
    }
}
