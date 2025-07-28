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
            
            try
            {
                return new BarmanOrder
                {
                    Id = int.Parse(element.Attribute("id")?.Value ?? "0"),
                    OrderId = int.Parse(element.Attribute("orderId")?.Value ?? "0"),
                    BarmanId = int.Parse(element.Attribute("barmanId")?.Value ?? "0"),
                    StationId = int.Parse(element.Attribute("stationId")?.Value ?? "0"),
                    BarId = int.Parse(element.Attribute("barId")?.Value ?? "0"),
                    EventId = int.Parse(element.Attribute("eventId")?.Value ?? "0"),
                    DateTime = DateTime.Parse(element.Attribute("dateTime")?.Value ?? DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"))
                };
            }
            catch (Exception ex)
            {
                throw new FormatException($"Error al parsear elemento XML de BarmanOrder: {ex.Message}", ex);
            }
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
