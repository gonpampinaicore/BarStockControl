using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;

namespace BarStockControl.Mappers
{
    public static class OrderItemMapper
    {
        public static OrderItemDto ToDto(OrderItem item)
        {
            if (item == null) return null;
            return new OrderItemDto
            {
                Id = item.Id,
                OrderId = item.OrderId,
                DrinkId = item.DrinkId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount,
                Subtotal = item.Subtotal
            };
        }

        public static OrderItem FromDto(OrderItemDto dto)
        {
            if (dto == null) return null;
            return new OrderItem
            {
                Id = dto.Id,
                OrderId = dto.OrderId,
                DrinkId = dto.DrinkId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                Discount = dto.Discount,
                Subtotal = dto.Subtotal
            };
        }

        public static OrderItem FromXml(XElement element)
        {
            if (element == null) return null;
            return new OrderItem
            {
                Id = (int)element.Attribute("id"),
                OrderId = (int)element.Attribute("orderId"),
                DrinkId = (int)element.Attribute("drinkId"),
                Quantity = (int)element.Attribute("quantity"),
                UnitPrice = (decimal)element.Attribute("unitPrice"),
                Discount = (decimal)element.Attribute("discount"),
                Subtotal = (decimal)element.Attribute("subtotal")
            };
        }

        public static XElement ToXml(OrderItem item)
        {
            if (item == null) return null;
            return new XElement("orderItem",
                new XAttribute("id", item.Id),
                new XAttribute("orderId", item.OrderId),
                new XAttribute("drinkId", item.DrinkId),
                new XAttribute("quantity", item.Quantity),
                new XAttribute("unitPrice", item.UnitPrice),
                new XAttribute("discount", item.Discount),
                new XAttribute("subtotal", item.Subtotal)
            );
        }
    }
} 
