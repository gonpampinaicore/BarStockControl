using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Models.Enums;

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
                CashRegisterId = order.CashRegisterId,
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
                CashRegisterId = dto.CashRegisterId,
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
                CashRegisterId = element.Attribute("cashRegisterId") != null ? (int?)element.Attribute("cashRegisterId") : null,
                CreatedAt = DateTime.Parse((string)element.Attribute("createdAt")),
                Status = Enum.TryParse((string)element.Attribute("status"), out OrderStatus status) ? status : OrderStatus.PendienteDePago,
                PaymentMethod = (string)element.Attribute("paymentMethod"),
                Total = (decimal)element.Attribute("total")
            };
        }

        public static XElement ToXml(Order order)
        {
            if (order == null) return null;
            var element = new XElement("order",
                new XAttribute("id", order.Id),
                new XAttribute("eventId", order.EventId),
                new XAttribute("userId", order.UserId),
                new XAttribute("createdAt", order.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss")),
                new XAttribute("status", order.Status.ToString()),
                new XAttribute("paymentMethod", order.PaymentMethod),
                new XAttribute("total", order.Total)
            );
            
            if (order.CashRegisterId.HasValue)
            {
                element.Add(new XAttribute("cashRegisterId", order.CashRegisterId.Value));
            }
            
            return element;
        }
    }
} 
