using BarStockControl.DTOs;
using BarStockControl.Models;
using BarStockControl.Models.Enums;
using System;
using System.Xml.Linq;

namespace BarStockControl.Mappers
{
    public static class StockMovementMapper
    {
        public static StockMovementDto ToDto(StockMovement movement)
        {
            return new StockMovementDto
            {
                Id = movement.Id,
                ProductId = movement.ProductId,
                FromDepositId = movement.FromDepositId,
                FromStationId = movement.FromStationId,
                ToDepositId = movement.ToDepositId,
                ToStationId = movement.ToStationId,
                Quantity = movement.Quantity,
                EventId = movement.EventId,
                UserId = movement.UserId,
                Timestamp = movement.Timestamp,
                Status = movement.Status,
                Comment = movement.Comment
            };
        }

        public static StockMovement ToEntity(StockMovementDto dto)
        {
            return new StockMovement
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                FromDepositId = dto.FromDepositId,
                FromStationId = dto.FromStationId,
                ToDepositId = dto.ToDepositId,
                ToStationId = dto.ToStationId,
                Quantity = dto.Quantity,
                EventId = dto.EventId,
                UserId = dto.UserId,
                Timestamp = dto.Timestamp,
                Status = dto.Status,
                Comment = dto.Comment
            };
        }

        public static XElement ToXml(StockMovement movement)
        {
            var element = new XElement("stockMovement",
                new XAttribute("id", movement.Id),
                new XAttribute("productId", movement.ProductId),
                new XAttribute("quantity", movement.Quantity),
                new XAttribute("eventId", movement.EventId),
                new XAttribute("userId", movement.UserId),
                new XAttribute("timestamp", movement.Timestamp.ToString("o")), // ISO 8601
                new XAttribute("status", movement.Status.ToString())
            );



            if (movement.FromDepositId.HasValue)
                element.Add(new XAttribute("fromDepositId", movement.FromDepositId));

            if (movement.FromStationId.HasValue)
                element.Add(new XAttribute("fromStationId", movement.FromStationId));

            if (movement.ToDepositId.HasValue)
                element.Add(new XAttribute("toDepositId", movement.ToDepositId));

            if (movement.ToStationId.HasValue)
                element.Add(new XAttribute("toStationId", movement.ToStationId));

            if (!string.IsNullOrWhiteSpace(movement.Comment))
                element.Add(new XAttribute("comment", movement.Comment));

            return element;
        }

        public static StockMovement FromXml(XElement element)
        {
            return new StockMovement
            {
                Id = int.Parse(element.Attribute("id")?.Value),
                ProductId = int.Parse(element.Attribute("productId")?.Value),
                Quantity = double.Parse(element.Attribute("quantity")?.Value),
                EventId = int.Parse(element.Attribute("eventId")?.Value),
                UserId = int.Parse(element.Attribute("userId")?.Value),

                FromDepositId = element.Attribute("fromDepositId") != null ? int.Parse(element.Attribute("fromDepositId")?.Value) : (int?)null,
                FromStationId = element.Attribute("fromStationId") != null ? int.Parse(element.Attribute("fromStationId")?.Value) : (int?)null,
                ToDepositId = element.Attribute("toDepositId") != null ? int.Parse(element.Attribute("toDepositId")?.Value) : (int?)null,
                ToStationId = element.Attribute("toStationId") != null ? int.Parse(element.Attribute("toStationId")?.Value) : (int?)null,
                Timestamp = DateTime.Parse(element.Attribute("timestamp")?.Value),
                Status = Enum.TryParse(element.Attribute("status")?.Value, out StockMovementStatus status) ? status : StockMovementStatus.Created,
                Comment = element.Attribute("comment")?.Value
            };
        }
    }
}
