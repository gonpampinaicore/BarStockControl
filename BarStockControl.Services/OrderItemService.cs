using BarStockControl.Models;
using BarStockControl.Data;
using BarStockControl.Mappers;
using System.Xml.Linq;
using BarStockControl.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace BarStockControl.Services
{
    public class OrderItemService : BaseService<OrderItem>
    {
        public OrderItemService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "orderItems") { }

        protected override OrderItem MapFromXml(XElement element)
        {
            return OrderItemMapper.FromXml(element);
        }

        protected override XElement MapToXml(OrderItem item)
        {
            return OrderItemMapper.ToXml(item);
        }

        public List<string> ValidateOrderItem(OrderItemDto item, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (item.OrderId <= 0)
                errors.Add("Order ID is required.");

            if (item.DrinkId <= 0)
                errors.Add("Drink ID is required.");

            if (item.Quantity <= 0)
                errors.Add("Quantity must be greater than 0.");

            if (item.UnitPrice <= 0)
                errors.Add("Unit price must be greater than 0.");

            return errors;
        }

        public List<string> CreateOrderItem(OrderItemDto item)
        {
            var errors = ValidateOrderItem(item);
            if (errors.Any())
                return errors;

            var entity = OrderItemMapper.FromDto(item);
            entity.Id = GetNextId();
            Add(entity);
            return new List<string>();
        }

        public List<string> UpdateOrderItem(OrderItemDto item)
        {
            var errors = ValidateOrderItem(item, isUpdate: true);
            if (errors.Any())
                return errors;

            var entity = OrderItemMapper.FromDto(item);
            Update(entity.Id, entity);
            return new List<string>();
        }

        public void DeleteOrderItem(int id)
        {
            Delete(id);
        }

        public OrderItemDto GetOrderItemDtoById(int id)
        {
            var item = GetAll().FirstOrDefault(i => i.Id == id);
            return item != null ? OrderItemMapper.ToDto(item) : null;
        }

        public List<OrderItemDto> GetAllOrderItemDtos()
        {
            return GetAll().Select(OrderItemMapper.ToDto).ToList();
        }
    }
} 
