using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Data;
using BarStockControl.Models;
using BarStockControl.Mappers;
using BarStockControl.DTOs;

namespace BarStockControl.Services
{
    public class ResourceRolePermissionService : BaseService<ResourceRolePermission>
    {
        public ResourceRolePermissionService(XmlDataManager dataManager)
            : base(dataManager, "resourceRolePermissions") { }

        protected override ResourceRolePermission MapFromXml(XElement element)
        {
            return new ResourceRolePermission
            {
                RoleId = int.Parse(element.Attribute("roleId").Value),
                ResourceType = element.Attribute("resourceType").Value
            };
        }

        protected override XElement MapToXml(ResourceRolePermission permission)
        {
            return new XElement("resourceRolePermission",
                new XAttribute("roleId", permission.RoleId),
                new XAttribute("resourceType", permission.ResourceType)
            );
        }

        public List<UserDto> GetUsersForResourceTypeDto(IEnumerable<UserDto> userDtos, string resourceType)
        {
            var permissions = GetAll();
            return userDtos
                .Select(UserMapper.ToEntity)
                .Where(u => u.RoleIds.Any(roleId => 
                    permissions.Any(p => p.RoleId == roleId && p.ResourceType == resourceType)))
                .Select(UserMapper.ToDto)
                .ToList();
        }
    }
} 
