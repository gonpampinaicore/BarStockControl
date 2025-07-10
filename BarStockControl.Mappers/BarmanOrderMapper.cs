using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;

namespace BarStockControl.Mappers
{
    public static class BarmanOrderMapper
    {
        public static BarmanOrderDto ToDto(BarmanOrder entity)
        {
            if (entity == null) return null;
            return new BarmanOrderDto
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                BarmanId = entity.BarmanId
            };
        }

        public static BarmanOrder FromDto(BarmanOrderDto dto)
        {
            if (dto == null) return null;
            return new BarmanOrder
            {
                Id = dto.Id,
                OrderId = dto.OrderId,
                BarmanId = dto.BarmanId
            };
        }

        public static BarmanOrder FromXml(XElement element)
        {
            if (element == null) return null;
            return new BarmanOrder
            {
                Id = (int)element.Attribute("id"),
                OrderId = (int)element.Attribute("orderId"),
                BarmanId = (int)element.Attribute("barmanId")
            };
        }

        public static XElement ToXml(BarmanOrder entity)
        {
            if (entity == null) return null;
            return new XElement("barmanOrder",
                new XAttribute("id", entity.Id),
                new XAttribute("orderId", entity.OrderId),
                new XAttribute("barmanId", entity.BarmanId)
            );
        }
    }
} 
