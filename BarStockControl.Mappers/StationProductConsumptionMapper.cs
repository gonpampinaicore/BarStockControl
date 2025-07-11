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
                FechaHora = entity.FechaHora,
                EventoId = entity.EventoId,
                UsuarioId = entity.UsuarioId
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
                FechaHora = dto.FechaHora,
                EventoId = dto.EventoId,
                UsuarioId = dto.UsuarioId
            };
        }

        public static StationProductConsumption FromXml(XElement element)
        {
            return new StationProductConsumption
            {
                Id = (int)element.Attribute("id"),
                StationId = (int)element.Attribute("stationId"),
                ProductId = (int)element.Attribute("productId"),
                OrderItemId = (int)element.Attribute("orderItemId"),
                FechaHora = DateTime.Parse((string)element.Attribute("fechaHora")),
                EventoId = (int)element.Attribute("eventoId"),
                UsuarioId = (int?)element.Attribute("usuarioId")
            };
        }

        public static XElement ToXml(StationProductConsumption entity)
        {
            return new XElement("stationProductConsumption",
                new XAttribute("id", entity.Id),
                new XAttribute("stationId", entity.StationId),
                new XAttribute("productId", entity.ProductId),
                new XAttribute("orderItemId", entity.OrderItemId),
                new XAttribute("fechaHora", entity.FechaHora.ToString("o")),
                new XAttribute("eventoId", entity.EventoId),
                new XAttribute("usuarioId", entity.UsuarioId.HasValue ? entity.UsuarioId.Value.ToString() : "")
            );
        }
    }
} 
