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
            if (element == null)
                throw new ArgumentNullException(nameof(element), "El elemento XML de ResourceAssignment no puede ser null.");

            string[] requiredAttrs = { "id", "eventId", "resourceId", "resourceType", "userId" };
            foreach (var attr in requiredAttrs)
            {
                if (element.Attribute(attr) == null || string.IsNullOrWhiteSpace((string)element.Attribute(attr)))
                    throw new FormatException($"El atributo '{attr}' es obligatorio y no puede ser nulo o vacío en resourceAssignment.");
            }

            int id, eventId, resourceId, userId;
            if (!int.TryParse((string)element.Attribute("id"), out id))
                throw new FormatException("El atributo 'id' debe ser un número entero válido en resourceAssignment.");
            if (!int.TryParse((string)element.Attribute("eventId"), out eventId))
                throw new FormatException("El atributo 'eventId' debe ser un número entero válido en resourceAssignment.");
            if (!int.TryParse((string)element.Attribute("resourceId"), out resourceId))
                throw new FormatException("El atributo 'resourceId' debe ser un número entero válido en resourceAssignment.");
            if (!int.TryParse((string)element.Attribute("userId"), out userId))
                throw new FormatException("El atributo 'userId' debe ser un número entero válido en resourceAssignment.");

            string resourceType = (string)element.Attribute("resourceType");
            if (string.IsNullOrWhiteSpace(resourceType))
                throw new FormatException("El atributo 'resourceType' es obligatorio en resourceAssignment.");

            return new ResourceAssignment
            {
                Id = id,
                EventId = eventId,
                ResourceId = resourceId,
                ResourceType = resourceType,
                UserId = userId
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
