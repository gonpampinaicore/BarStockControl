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

        public List<string> ValidateOrder(OrderDto order, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (order.EventId <= 0)
                errors.Add("Event ID is required.");

            if (order.UserId <= 0)
                errors.Add("User ID is required.");

            if (order.Total <= 0)
                errors.Add("Total must be greater than 0.");

            return errors;
        }

        public List<string> CreateOrder(OrderDto order)
        {
            var errors = ValidateOrder(order);
            if (errors.Any())
                return errors;

            var entity = OrderMapper.FromDto(order);
            entity.Id = GetNextId();
            Add(entity);
            return new List<string>();
        }

        public List<string> UpdateOrder(OrderDto order)
        {
            var errors = ValidateOrder(order, isUpdate: true);
            if (errors.Any())
                return errors;

            var entity = OrderMapper.FromDto(order);
            Update(entity.Id, entity);
            return new List<string>();
        }

        public OrderDto GetOrderDtoById(int id)
        {
            var order = GetAll().FirstOrDefault(o => o.Id == id);
            return order != null ? OrderMapper.ToDto(order) : null;
        }

        public List<OrderDto> GetAllOrderDtos()
        {
            return GetAll().Select(OrderMapper.ToDto).ToList();
        }
    }
}
