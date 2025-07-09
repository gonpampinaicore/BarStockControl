using BarStockControl.DTOs;
using BarStockControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarStockControl.Mappers
{
    public static class PermissionItemMapper
    {
        public static PermissionItemDto ToDto(PermissionItem entity)
        {
            return new PermissionItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static PermissionItem ToEntity(PermissionItemDto dto)
        {
            return new PermissionItem
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                IsActive = dto.IsActive
            };
        }

        public static PermissionItem FromXml(XElement element)
        {
            return new PermissionItem
            {
                Id = int.Parse((string)element.Attribute("id")),
                Name = (string)element.Attribute("name"),
                Description = (string)element.Attribute("description"),
                IsActive = bool.Parse((string)element.Attribute("isActive") ?? "true")
            };
        }

        public static XElement ToXml(PermissionItem item)
        {
            return new XElement("permissionItem",
                new XAttribute("id", item.Id),
                new XAttribute("name", item.Name),
                new XAttribute("description", item.Description),
                new XAttribute("isActive", item.IsActive.ToString().ToLower())
            );
        }
    }

}
