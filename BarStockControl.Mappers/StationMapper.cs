using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Models.Enums;

namespace BarStockControl.Mappers
{
    public static class StationMapper
    {
        public static StationDto ToDto(Station station)
        {
            if (station == null)
                return null;

            return new StationDto
            {
                Id = station.Id,
                Name = station.Name,
                Status = station.Status.ToString(),
                Active = station.Active,
                Comment = station.Comment,
                BarId = station.BarId
            };
        }

        public static Station ToEntity(StationDto dto)
        {
            if (dto == null)
                return null;

            return new Station
            {
                Id = dto.Id,
                Name = dto.Name,
                Status = Enum.TryParse(dto.Status, out StationStatus status) ? status : StationStatus.InPreparation,
                Active = dto.Active,
                Comment = dto.Comment,
                BarId = dto.BarId
            };
        }

        public static Station FromXml(XElement element)
        {
            return new Station
            {
                Id = int.Parse((string)element.Attribute("id")),
                Name = (string)element.Attribute("name"),
                Status = Enum.TryParse((string)element.Attribute("status"), out StationStatus status) ? status : StationStatus.InPreparation,
                Active = bool.Parse((string)element.Attribute("active") ?? "true"),
                Comment = (string)element.Attribute("comment"),
                BarId = int.Parse(element.Attribute("barId")?.Value ?? "0")

            };
        }

        public static XElement ToXml(Station station)
        {
            var element = new XElement("station",
                new XAttribute("id", station.Id),
                new XAttribute("name", station.Name),
                new XAttribute("status", station.Status.ToString()),
                new XAttribute("active", station.Active.ToString().ToLower()),
                new XAttribute("comment", station.Comment ?? ""),
                new XAttribute("barId", station.BarId)

            );

            return element;
        }
    }
}
