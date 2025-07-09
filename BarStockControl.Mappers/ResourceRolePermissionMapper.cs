using BarStockControl.Models;
using BarStockControl.DTOs;

namespace BarStockControl.Mappers
{
    public static class ResourceRolePermissionMapper
    {
        public static ResourceRolePermissionDto ToDto(ResourceRolePermission entity)
        {
            return new ResourceRolePermissionDto
            {
                RoleId = entity.RoleId,
                ResourceType = entity.ResourceType
            };
        }

        public static ResourceRolePermission ToEntity(ResourceRolePermissionDto dto)
        {
            return new ResourceRolePermission
            {
                RoleId = dto.RoleId,
                ResourceType = dto.ResourceType
            };
        }
    }
} 
