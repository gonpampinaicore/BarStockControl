using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Mappers;
using BarStockControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BarStockControl.Services
{
    public class PermissionService : BaseService<Permission>
    {
        public PermissionService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "permissions") { }

        protected override Permission MapFromXml(XElement element)
        {
            return PermissionMapper.FromXml(element);
        }

        protected override XElement MapToXml(Permission permission)
        {
            return PermissionMapper.ToXml(permission);
        }

        public PermissionDto ToDto(Permission permission)
        {
            return PermissionMapper.ToDto(permission);
        }

        public Permission ToEntity(PermissionDto dto)
        {
            return PermissionMapper.ToEntity(dto);
        }

        public List<string> ValidatePermission(Permission permission, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(permission.Name))
                errors.Add("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(permission.Description))
                errors.Add("La descripción es obligatoria.");

            var existing = GetAll().FirstOrDefault(p =>
                p.Name.Equals(permission.Name, StringComparison.OrdinalIgnoreCase));

            if (existing != null && (!isUpdate || existing.Id != permission.Id))
                errors.Add("Ya existe un permiso con ese nombre.");

            return errors;
        }

        public List<string> CreatePermission(PermissionDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El permiso no puede ser null.");

            var permission = ToEntity(dto);
            var errors = ValidatePermission(permission);
            if (errors.Any()) return errors;

            permission.Id = GetNextId();
            permission.IsActive = true;
            Add(permission);
            return new List<string>();
        }

        public List<string> UpdatePermission(PermissionDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El permiso no puede ser null.");

            var permission = ToEntity(dto);
            var errors = ValidatePermission(permission, isUpdate: true);
            if (errors.Any()) return errors;

            Update(permission.Id, permission);
            return new List<string>();
        }

        public void DeletePermission(int id)
        {
            var permission = GetById(id);
            if (permission == null)
                throw new InvalidOperationException($"Permiso con ID {id} no encontrado.");

            Delete(id);
        }

        public Permission GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public List<Permission> GetAllPermissions()
        {
            return GetAll();
        }

        public List<Permission> Search(Func<Permission, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return GetAll().Where(predicate).ToList();
        }

        public List<PermissionDto> GetAllPermissionDtos()
        {
            return GetAllPermissions().Select(ToDto).ToList();
        }

        // Si bien ahora usamos Composite, este método sirve para reportes rápidos
        public List<int> GetPermissionIdsByRoleIds(List<int> roleIds)
        {
            if (roleIds == null)
                throw new ArgumentNullException(nameof(roleIds));

            var xml = _xmlDataManager.LoadDocument();
            var permissionIds = new HashSet<int>();

            var roleElements = xml.Root.Element("roles")?.Elements("role")
                .Where(r => roleIds.Contains(int.Parse(r.Attribute("id")?.Value ?? "0")));

            if (roleElements != null)
            {
                foreach (var roleElement in roleElements)
                {
                    foreach (var permissionRef in roleElement.Elements("rolePermissionRef"))
                    {
                        if (int.TryParse(permissionRef.Attribute("ref")?.Value, out int pid))
                            permissionIds.Add(pid);
                    }
                }
            }

            return permissionIds.ToList();
        }

        public List<string> GetPermissionNamesByRoleIds(List<int> roleIds)
        {
            var ids = GetPermissionIdsByRoleIds(roleIds);
            return GetAll().Where(p => ids.Contains(p.Id)).Select(p => p.Name).ToList();
        }
    }
}
