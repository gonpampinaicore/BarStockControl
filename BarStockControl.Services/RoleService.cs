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
            return RoleMapper.FromXml(element);
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
            return RoleMapper.ToEntity(dto, allRoles, allPermissions);
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
    }
}
