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
        private readonly XmlDataManager _xmlDataManager;
        private readonly ComponentService _componentService;

        public ResourceRolePermissionService(XmlDataManager dataManager)
            : base(dataManager, "resourceRolePermissions") 
        {
            _xmlDataManager = dataManager;
            _componentService = new ComponentService(_xmlDataManager);
        }

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
            var resourcePermissions = GetAll();
            
            var result = userDtos
                .Select(UserMapper.ToEntity)
                .Where(u => 
                {
                    // Construir la jerarquía de permisos del usuario
                    _componentService.BuildUserPermissions(u, u.RoleIds, u.PermissionIds);
                    
                    // Obtener todos los roles recursivamente (directos y heredados)
                    var allUserRoles = _componentService.GetAllUserRolesRecursive(u);
                    
                    // Verificar si el usuario tiene algún rol que tenga permiso para el tipo de recurso
                    var hasPermission = allUserRoles.Any(role => 
                        resourcePermissions.Any(rp => 
                            rp.RoleId == role.Id && 
                            rp.ResourceType == resourceType));
                    
                    return hasPermission;
                })
                .Select(UserMapper.ToDto)
                .ToList();
                
            return result;
        }
    }
} 
