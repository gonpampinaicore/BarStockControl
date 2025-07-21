using System;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;

namespace BarStockControl.Mappers
{
    public static class StationProductConsumptionMapper
    {
        public static StationProductConsumptionDto ToDto(StationProductConsumption entity)
        {
            return new StationProductConsumptionDto
            {
                Id = entity.Id,
                StationId = entity.StationId,
                ProductId = entity.ProductId,
                OrderItemId = entity.OrderItemId,
                DateTime = entity.DateTime,
                EventId = entity.EventId,
                UserId = entity.UserId
            };
        }

        public static StationProductConsumption FromDto(StationProductConsumptionDto dto)
        {
            return new StationProductConsumption
            {
                Id = dto.Id,
                StationId = dto.StationId,
                ProductId = dto.ProductId,
                OrderItemId = dto.OrderItemId,
                DateTime = dto.DateTime,
                EventId = dto.EventId,
                UserId = dto.UserId
            };
        }

        public static StationProductConsumption FromXml(XElement element)
        {
            return new StationProductConsumption
            {
                Id = (int?)element.Attribute("id") ?? 0,
                StationId = (int?)element.Attribute("stationId") ?? 0,
                ProductId = (int?)element.Attribute("productId") ?? 0,
                OrderItemId = (int?)element.Attribute("orderItemId") ?? 0,
                DateTime = DateTime.TryParse((string)element.Attribute("dateTime"), out var dateTime) ? dateTime : DateTime.Now,
                EventId = (int?)element.Attribute("eventId") ?? 0,
                UserId = (int?)element.Attribute("userId")
            };
        }

        public static XElement ToXml(StationProductConsumption entity)
        {
            return new XElement("stationProductConsumption",
                new XAttribute("id", entity.Id),
                new XAttribute("stationId", entity.StationId),
                new XAttribute("productId", entity.ProductId),
                new XAttribute("orderItemId", entity.OrderItemId),
                new XAttribute("dateTime", entity.DateTime.ToString("o")),
                new XAttribute("eventId", entity.EventId),
                new XAttribute("userId", entity.UserId.HasValue ? entity.UserId.Value.ToString() : "")
            );
        }
    }
} 
