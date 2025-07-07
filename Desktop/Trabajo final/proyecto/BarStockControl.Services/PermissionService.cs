using BarStockControl.Data;
using BarStockControl.Models;
using BarStockControl.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BarStockControl.Services
{
    public class PermissionService : BaseService<Permission>
    {
        public PermissionService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "permissionDefs") { }

        protected override Permission MapFromXml(XElement element)
        {
            return PermissionMapper.FromXml(element);
        }

        protected override XElement MapToXml(Permission permission)
        {
            return PermissionMapper.ToXml(permission);
        }

        public List<string> ValidatePermission(Permission permission, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(permission.Name))
                errors.Add("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(permission.Description))
                errors.Add("La descripción es obligatoria.");

            var existing = GetAll().FirstOrDefault(p => p.Name.Equals(permission.Name, StringComparison.OrdinalIgnoreCase));
            if (existing != null && (!isUpdate || existing.Id != permission.Id))
                errors.Add("Ya existe un permiso con ese nombre.");

            if (permission.PermissionItemIds == null || !permission.PermissionItemIds.Any())
                errors.Add("Debe contener al menos un permiso atómico (PermissionItem).");

            return errors;
        }

        public List<string> CreatePermission(Permission permission)
        {
            try
            {
                if (permission == null)
                    throw new ArgumentNullException(nameof(permission), "El permiso no puede ser null.");

                var errors = ValidatePermission(permission);
                if (errors.Any())
                    return errors;

                permission.Id = GetNextId();
                permission.IsActive = true;
                Add(permission);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear permiso: {ex.Message}", ex);
            }
        }

        public List<string> UpdatePermission(Permission permission)
        {
            try
            {
                if (permission == null)
                    throw new ArgumentNullException(nameof(permission), "El permiso no puede ser null.");

                var errors = ValidatePermission(permission, isUpdate: true);
                if (errors.Any())
                    return errors;

                Update(permission.Id, permission);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al actualizar permiso: {ex.Message}", ex);
            }
        }

        public void DeletePermission(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID del permiso debe ser mayor a 0.", nameof(id));

                var permission = GetById(id);
                if (permission == null)
                    throw new InvalidOperationException($"Permiso con ID {id} no encontrado.");

                Delete(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar permiso: {ex.Message}", ex);
            }
        }

        public Permission GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return null;

                return GetAll().FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener permiso por ID: {ex.Message}", ex);
            }
        }

        public List<Permission> GetAllPermissions()
        {
            try
            {
                return GetAll();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener todos los permisos: {ex.Message}", ex);
            }
        }

        public List<Permission> Search(Func<Permission, bool> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate), "El predicado de búsqueda no puede ser null.");

                return GetAll().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al buscar permisos: {ex.Message}", ex);
            }
        }

        public List<int> GetPermissionIdsByRoleIds(List<int> roleIds)
        {
            try
            {
                if (roleIds == null)
                    throw new ArgumentNullException(nameof(roleIds), "La lista de IDs de roles no puede ser null.");

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
                            {
                                permissionIds.Add(pid);
                            }
                        }
                    }
                }

                return permissionIds.ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener IDs de permisos por roles: {ex.Message}", ex);
            }
        }

        public List<string> GetPermissionNamesByRoleIds(List<int> roleIds)
        {
            try
            {
                if (roleIds == null)
                    throw new ArgumentNullException(nameof(roleIds), "La lista de IDs de roles no puede ser null.");

                var permissionIds = GetPermissionIdsByRoleIds(roleIds);

                return GetAll()
                    .Where(p => permissionIds.Contains(p.Id))
                    .Select(p => p.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener nombres de permisos por roles: {ex.Message}", ex);
            }
        }

    }
}
