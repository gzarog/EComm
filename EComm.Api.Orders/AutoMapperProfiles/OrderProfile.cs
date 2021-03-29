using AutoMapper;
using EComm.Api.Orders.Data;
using EComm.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Api.Orders.AutoMapperProfiles
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderModel> ();
            
            CreateMap<OrderItem, OrderItemModel>();
        }
    }
}
