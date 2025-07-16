using BarStockControl.Data;
using BarStockControl.Models;
using BarStockControl.Mappers;
using System.Xml.Linq;
using BarStockControl.DTOs;

namespace BarStockControl.Services
{
    public class RoleService : BaseService<Role>
    {
        private readonly PermissionService _permissionService;

        public RoleService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "roles")
        {
            _permissionService = new PermissionService(xmlDataManager);
        }

        protected override Role MapFromXml(XElement element)
        {
            var role = new Role
            {
                Id = int.Parse(element.Attribute("id")?.Value),
                Name = element.Attribute("name")?.Value,
                Description = element.Attribute("description")?.Value,
                IsActive = bool.Parse(element.Attribute("isActive")?.Value ?? "true")
            };

            return role;
        }

        protected override XElement MapToXml(Role role)
        {
            return RoleMapper.ToXml(role);
        }

        public RoleDto ToDto(Role role)
        {
            return RoleMapper.ToDto(role);
        }

        public Role ToEntity(RoleDto dto)
        {
            var allRoles = GetAll();
            var allPermissions = _permissionService.GetAll();
            var role = RoleMapper.ToEntity(dto, allRoles, allPermissions);
            
            var componentService = new ComponentService(_xmlDataManager);
            componentService.ValidateComponentStructure(role);
            
            return role;
        }

        public List<string> ValidateRole(Role role, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(role.Name))
                errors.Add("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(role.Description))
                errors.Add("La descripción es obligatoria.");

            var existing = GetAll().FirstOrDefault(r =>
                r.Name.Equals(role.Name, StringComparison.OrdinalIgnoreCase));

            if (existing != null && (!isUpdate || existing.Id != role.Id))
                errors.Add("Ya existe un rol con ese nombre.");

            return errors;
        }

        public List<string> CreateRole(RoleDto roleDto)
        {
            var role = ToEntity(roleDto);
            var errors = ValidateRole(role);
            if (errors.Any())
                return errors;

            role.Id = GetNextId();
            role.IsActive = true;
            Add(role);
            return new List<string>();
        }

        public List<string> UpdateRole(RoleDto roleDto)
        {
            var role = ToEntity(roleDto);
            var errors = ValidateRole(role, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(role.Id, role);
            return new List<string>();
        }

        public void DeleteRole(int id)
        {
            try
            {
                var role = GetById(id);
                if (role == null)
                    throw new InvalidOperationException($"Rol con ID {id} no encontrado.");
                if (role.Name == "AdminAdmin")
                    throw new InvalidOperationException("No se puede eliminar el rol AdminAdmin.");
                Delete(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar rol: {ex.Message}", ex);
            }
        }

        public Role GetById(int id)
        {
            return GetAll().FirstOrDefault(r => r.Id == id);
        }

        public List<RoleDto> GetAllRoles()
        {
            try
            {
                return GetAll().Select(ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener todos los roles: {ex.Message}", ex);
            }
        }

        public List<Role> Search(Func<Role, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public int? GetRoleIdByName(string roleName)
        {
            var role = GetAll().FirstOrDefault(r => r.Name == roleName);
            return role?.Id;
        }

        public (List<int> RoleIds, List<int> PermissionIds) GetComponentIds(Role role)
        {
            if (role == null)
                return (new List<int>(), new List<int>());

            var componentService = new ComponentService(_xmlDataManager);
            return componentService.GetIdsFromComponent(role);
        }

        public RoleDto ToDtoWithComponentService(Role role)
        {
            if (role == null) return null;

            var (roleIds, permissionIds) = GetComponentIds(role);

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                IsActive = role.IsActive,
                PermissionIds = permissionIds,
                RoleIds = roleIds
            };
        }

        public Role GetByIdWithHierarchy(int id)
        {
            var role = GetById(id);
            if (role == null) return null;

            var xml = _xmlDataManager.LoadDocument();
            var roleElement = xml.Root.Element("roles")?.Elements("role")
                .FirstOrDefault(r => int.Parse(r.Attribute("id")?.Value ?? "0") == id);

            if (roleElement != null)
            {
                role.ClearChildren();

                foreach (var permRef in roleElement.Elements("rolePermissionRef"))
                {
                    if (int.TryParse(permRef.Attribute("ref")?.Value, out int permId))
                    {
                        var permission = _permissionService.GetById(permId);
                        if (permission != null)
                        {
                            role.AddChild(permission);
                        }
                    }
                }

                foreach (var roleRef in roleElement.Elements("roleRef"))
                {
                    if (int.TryParse(roleRef.Attribute("ref")?.Value, out int subRoleId))
                    {
                        var subRole = GetByIdWithHierarchy(subRoleId);
                        if (subRole != null)
                        {
                            role.AddChild(subRole);
                        }
                    }
                }
            }

            return role;
        }
    }
}
