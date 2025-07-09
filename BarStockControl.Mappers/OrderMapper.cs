using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;

namespace BarStockControl.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(Order order)
        {
            if (order == null) return null;
            return new OrderDto
            {
                Id = order.Id,
                EventId = order.EventId,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                PaymentMethod = order.PaymentMethod,
                Total = order.Total
            };
        }

        public static Order FromDto(OrderDto dto)
        {
            if (dto == null) return null;
            return new Order
            {
                Id = dto.Id,
                EventId = dto.EventId,
                UserId = dto.UserId,
                CreatedAt = dto.CreatedAt,
                Status = dto.Status,
                PaymentMethod = dto.PaymentMethod,
                Total = dto.Total
            };
        }

        public static Order FromXml(XElement element)
        {
            if (element == null) return null;
            return new Order
            {
                Id = (int)element.Attribute("id"),
                EventId = (int)element.Attribute("eventId"),
                UserId = (int)element.Attribute("userId"),
                CreatedAt = DateTime.Parse((string)element.Attribute("createdAt")),
                Status = (string)element.Attribute("status"),
                PaymentMethod = (string)element.Attribute("paymentMethod"),
                Total = (decimal)element.Attribute("total")
            };
        }

        public static XElement ToXml(Order order)
        {
            if (order == null) return null;
            return new XElement("order",
                new XAttribute("id", order.Id),
                new XAttribute("eventId", order.EventId),
                new XAttribute("userId", order.UserId),
                new XAttribute("createdAt", order.CreatedAt.ToString("o")),
                new XAttribute("status", order.Status),
                new XAttribute("paymentMethod", order.PaymentMethod),
                new XAttribute("total", order.Total)
            );
        }
    }
} 
