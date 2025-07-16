using BarStockControl.DTOs;
using BarStockControl.Models;
using BarStockControl;
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
                PermissionIds = entity.Children
                    .OfType<Permission>()
                    .Select(p => p.Id)
                    .ToList(),
                RoleIds = entity.Children
                    .OfType<Role>()
                    .Select(r => r.Id)
                    .ToList()
            };
        }

        public static Role ToEntity(RoleDto dto, List<Role> allRoles, List<Permission> allPermissions)
        {
            var role = new Role
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                IsActive = dto.IsActive
            };

            foreach (var pid in dto.PermissionIds)
            {
                var perm = allPermissions.FirstOrDefault(p => p.Id == pid);
                if (perm != null)
                    role.AddChild(perm);
            }

            foreach (var rid in dto.RoleIds)
            {
                var subRole = allRoles.FirstOrDefault(r => r.Id == rid);
                if (subRole != null)
                    role.AddChild(subRole);
            }

            return role;
        }

        public static Role FromXml(XElement element)
        {
            var role = new Role
            {
                Id = int.Parse(element.Attribute("id")?.Value),
                Name = element.Attribute("name")?.Value,
                Description = element.Attribute("description")?.Value,
                IsActive = bool.Parse(element.Attribute("isActive")?.Value ?? "true")
            };

            foreach (var permRef in element.Elements("rolePermissionRef"))
            {
                if (int.TryParse(permRef.Attribute("ref")?.Value, out int permId))
                    role.AddChild(new Permission { Id = permId });
            }

            foreach (var roleRef in element.Elements("roleRef"))
            {
                if (int.TryParse(roleRef.Attribute("ref")?.Value, out int roleId))
                    role.AddChild(new Role { Id = roleId });
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

            foreach (var child in role.Children)
            {
                if (child is Permission perm)
            {
                    element.Add(new XElement("rolePermissionRef", new XAttribute("ref", perm.Id)));
                }
                else if (child is Role subRole)
                {
                    element.Add(new XElement("roleRef", new XAttribute("ref", subRole.Id)));
                }
            }

            return element;
        }
    }
}
