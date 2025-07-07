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
    public static class RoleMapper
    {
        public static RoleDto ToDto(Role entity)
        {
            return new RoleDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
                PermissionIds = new List<int>(entity.PermissionIds)
            };
        }

        public static Role ToEntity(RoleDto dto)
        {
            return new Role
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                IsActive = dto.IsActive,
                PermissionIds = new List<int>(dto.PermissionIds)
            };
        }

        public static Role FromXml(XElement element)
        {
            var role = new Role
            {
                Id = int.Parse(element.Attribute("id").Value),
                Name = element.Attribute("name")?.Value,
                Description = element.Attribute("description")?.Value,
                IsActive = bool.Parse(element.Attribute("isActive")?.Value ?? "true"),
                PermissionIds = new List<int>()
            };

            foreach (var permRef in element.Elements("rolePermissionRef"))
            {
                var refId = permRef.Attribute("ref")?.Value;
                if (int.TryParse(refId, out int permId))
                    role.PermissionIds.Add(permId);
            }

            return role;
        }

        public static XElement ToXml(Role role)
        {
            var element = new XElement("role",
                new XAttribute("id", role.Id),
                new XAttribute("name", role.Name),
                new XAttribute("description", role.Description ?? string.Empty),
                new XAttribute("isActive", role.IsActive.ToString().ToLower())
            );

            foreach (var permId in role.PermissionIds)
            {
                element.Add(new XElement("rolePermissionRef", new XAttribute("ref", permId)));
            }

            return element;
        }
    }
}
