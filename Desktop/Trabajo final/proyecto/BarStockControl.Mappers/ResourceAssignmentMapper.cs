using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Models.Enums;

namespace BarStockControl.Mappers
{
    public static class ResourceAssignmentMapper
    {
        public static ResourceAssignmentDto ToDto(ResourceAssignment entity)
        {
            return new ResourceAssignmentDto
            {
                Id = entity.Id,
                EventId = entity.EventId,
                ResourceId = entity.ResourceId,
                ResourceType = entity.ResourceType.ToLower(),
                UserId = entity.UserId
            };
        }

        public static ResourceAssignment ToEntity(ResourceAssignmentDto dto)
        {
            return new ResourceAssignment
            {
                Id = dto.Id,
                EventId = dto.EventId,
                ResourceId = dto.ResourceId,
                ResourceType = dto.ResourceType.ToLower(),
                UserId = dto.UserId
            };
        }

        public static ResourceAssignment FromXml(XElement element)
        {
            return new ResourceAssignment
            {
                Id = int.Parse((string)element.Attribute("id")),
                EventId = int.Parse((string)element.Attribute("eventId")),
                ResourceId = int.Parse((string)element.Attribute("resourceId")),
                ResourceType = (string)element.Attribute("resourceType"),
                UserId = int.Parse((string)element.Attribute("userId"))
            };
        }

        public static XElement ToXml(ResourceAssignment entity)
        {
            return new XElement("resourceAssignment",
                new XAttribute("id", entity.Id),
                new XAttribute("eventId", entity.EventId),
                new XAttribute("resourceId", entity.ResourceId),
                new XAttribute("resourceType", entity.ResourceType.ToLower()),
                new XAttribute("userId", entity.UserId)
            );
        }
    }
}
