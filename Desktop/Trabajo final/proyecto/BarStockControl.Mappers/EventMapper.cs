using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Models.Enums;

namespace BarStockControl.Mappers
{
    public static class EventMapper
    {
        public static EventDto ToDto(Event ev)
        {
            if (ev == null)
                return null;

            return new EventDto
            {
                Id = ev.Id,
                Name = ev.Name,
                Description = ev.Description,
                StartDate = ev.StartDate,
                EndDate = ev.EndDate,
                Status = ev.Status,
                IsActive = ev.IsActive
            };
        }

        public static Event ToEntity(EventDto dto)
        {
            if (dto == null)
                return null;

            return new Event
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                IsActive = dto.IsActive
            };
        }

        public static Event FromXml(XElement element)
        {
            return new Event
            {
                Id = int.Parse((string)element.Attribute("id")),
                Name = (string)element.Attribute("name"),
                Description = (string)element.Attribute("description"),
                StartDate = DateTime.Parse((string)element.Attribute("startDate")),
                EndDate = DateTime.TryParse((string)element.Attribute("endDate"), out var end) ? end : null,
                Status = Enum.TryParse((string)element.Attribute("status"), out EventStatus status) ? status : EventStatus.InPreparation,
                IsActive = bool.Parse((string)element.Attribute("active") ?? "true")
            };
        }

        public static XElement ToXml(Event ev)
        {
            var element = new XElement("event",
                new XAttribute("id", ev.Id),
                new XAttribute("name", ev.Name),
                new XAttribute("description", ev.Description ?? ""),
                new XAttribute("startDate", ev.StartDate.ToString("o")),
                new XAttribute("endDate", ev.EndDate?.ToString("o") ?? ""),
                new XAttribute("status", ev.Status.ToString()),
                new XAttribute("active", ev.IsActive.ToString().ToLower())
            );

            return element;
        }
    }
}
