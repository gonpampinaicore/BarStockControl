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

        public bool UserCanManageResourceType(User user, string resourceType)
        {
            var permissions = GetAll();
            return user.RoleIds.Any(roleId => permissions.Any(p => p.RoleId == roleId && p.ResourceType == resourceType));
        }

        public List<User> GetUsersForResourceType(IEnumerable<User> users, string resourceType)
        {
            var permissions = GetAll();
            return users.Where(u => UserCanManageResourceType(u, resourceType)).ToList();
        }

        public List<ResourceRolePermissionDto> GetAllDto()
        {
            return GetAll().Select(ResourceRolePermissionMapper.ToDto).ToList();
        }

        public List<UserDto> GetUsersForResourceTypeDto(IEnumerable<UserDto> userDtos, string resourceType)
        {
            var users = userDtos.Select(UserMapper.ToEntity).ToList();
            var filtered = GetUsersForResourceType(users, resourceType);
            return filtered.Select(UserMapper.ToDto).ToList();
        }
    }
} 
