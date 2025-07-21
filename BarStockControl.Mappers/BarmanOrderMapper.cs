using System;
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
                BarmanId = entity.BarmanId,
                StationId = entity.StationId,
                BarId = entity.BarId,
                EventId = entity.EventId,
                DateTime = entity.DateTime
            };
        }

        public static BarmanOrder FromDto(BarmanOrderDto dto)
        {
            if (dto == null) return null;
            return new BarmanOrder
            {
                Id = dto.Id,
                OrderId = dto.OrderId,
                BarmanId = dto.BarmanId,
                StationId = dto.StationId,
                BarId = dto.BarId,
                EventId = dto.EventId,
                DateTime = dto.DateTime
            };
        }

        public static BarmanOrder FromXml(XElement element)
        {
            if (element == null) return null;
            return new BarmanOrder
            {
                Id = (int)element.Attribute("id"),
                OrderId = (int)element.Attribute("orderId"),
                BarmanId = (int)element.Attribute("barmanId"),
                StationId = (int)element.Attribute("stationId"),
                BarId = (int)element.Attribute("barId"),
                EventId = (int)element.Attribute("eventId"),
                DateTime = DateTime.Parse((string)element.Attribute("dateTime"))
            };
        }

        public static XElement ToXml(BarmanOrder entity)
        {
            if (entity == null) return null;
            return new XElement("barmanOrder",
                new XAttribute("id", entity.Id),
                new XAttribute("orderId", entity.OrderId),
                new XAttribute("barmanId", entity.BarmanId),
                new XAttribute("stationId", entity.StationId),
                new XAttribute("barId", entity.BarId),
                new XAttribute("eventId", entity.EventId),
                new XAttribute("dateTime", entity.DateTime.ToString("yyyy-MM-ddTHH:mm:ss"))
            );
        }
    }
} 
