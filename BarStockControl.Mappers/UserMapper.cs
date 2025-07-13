using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Security;

namespace BarStockControl.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = PasswordEncryption.DecryptPassword(user.Password),
                Active = user.Active,
                RoleIds = user.RoleIds.ToList(),
                PermissionIds = user.PermissionIds.ToList()
            };
        }

        public static User ToEntity(UserDto dto)
        {
            if (dto == null) return null;

            return new User
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = PasswordEncryption.EncryptPassword(dto.Password),
                Active = dto.Active,
                RoleIds = dto.RoleIds.ToList(),
                PermissionIds = dto.PermissionIds.ToList()
            };
        }

        public static XElement ToXml(User user)
        {
            var element = new XElement("user",
                new XAttribute("id", user.Id),
                new XAttribute("name", user.FirstName),
                new XAttribute("lastname", user.LastName),
                new XAttribute("email", user.Email),
                new XAttribute("password", user.Password),
                new XAttribute("active", user.Active.ToString().ToLower())
            );

            foreach (var roleId in user.RoleIds)
                element.Add(new XElement("roleRef", new XAttribute("ref", roleId)));

            foreach (var permissionId in user.PermissionIds)
                element.Add(new XElement("permissionRef", new XAttribute("ref", permissionId)));

            return element;
        }

        public static User FromXml(XElement element)
        {
            return new User
            {
                Id = int.Parse((string)element.Attribute("id")),
                FirstName = (string)element.Attribute("name"),
                LastName = (string)element.Attribute("lastname"),
                Email = (string)element.Attribute("email"),
                Password = (string)element.Attribute("password"),
                Active = bool.Parse((string)element.Attribute("active") ?? "true"),
                RoleIds = element.Elements("roleRef")
                    .Select(e => int.Parse((string)e.Attribute("ref")))
                    .ToList(),
                PermissionIds = element.Elements("permissionRef")
                    .Select(e => int.Parse((string)e.Attribute("ref")))
                    .ToList()
            };
        }
    }
}
