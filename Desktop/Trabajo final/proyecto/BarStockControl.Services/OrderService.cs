using BarStockControl.Models;
using BarStockControl.Data;
using BarStockControl.Mappers;
using System.Xml.Linq;
using BarStockControl.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace BarStockControl.Services
{
    public class OrderService : BaseService<Order>
    {
        public OrderService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "orders")
        {
        }

        protected override Order MapFromXml(XElement element)
        {
            return OrderMapper.FromXml(element);
        }

        protected override XElement MapToXml(Order order)
        {
            return OrderMapper.ToXml(order);
        }

        public void CreateOrder(Order order)
        {
            Add(order);
        }

        public void UpdateOrder(int id, Order order)
        {
            Update(id, order);
        }

        public void DeleteOrder(int id)
        {
            Delete(id);
        }

        public Order GetOrderById(int id)
        {
            return GetById(id);
        }

        public List<Order> GetAllOrders()
        {
            return GetAll();
        }
    }
}
