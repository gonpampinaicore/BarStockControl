using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;

namespace BarStockControl.Mappers
{
    public static class PermissionMapper
    {
        public static PermissionDto ToDto(Permission entity)
        {
            return new PermissionDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public static Permission ToEntity(PermissionDto dto)
        {
            return new Permission
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                IsActive = dto.IsActive
            };
        }

        public static Permission FromXml(XElement element)
        {
            return new Permission
            {
                Id = int.Parse((string)element.Attribute("id")),
                Name = (string)element.Attribute("name"),
                Description = (string)element.Attribute("description"),
                IsActive = bool.Parse((string)element.Attribute("isActive") ?? "true")
            };
        }

        public static XElement ToXml(Permission permission)
        {
            return new XElement("permission",
                new XAttribute("id", permission.Id),
                new XAttribute("name", permission.Name),
                new XAttribute("description", permission.Description),
                new XAttribute("isActive", permission.IsActive.ToString().ToLower())
            );
        }
    }

}
