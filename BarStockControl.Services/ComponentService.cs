using System;
using System.Collections.Generic;
using System.Linq;
using BarStockControl.Models;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Models.Enums;

namespace BarStockControl.Services
{
    public class ComponentService
    {
        private readonly RoleService _roleService;
        private readonly PermissionService _permissionService;

        public ComponentService(XmlDataManager xmlDataManager)
        {
            _roleService = new RoleService(xmlDataManager);
            _permissionService = new PermissionService(xmlDataManager);
        }

        public void AddChildToComponent(Component parent, Component child)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent), "El componente padre no puede ser null.");

            if (child == null)
                throw new ArgumentNullException(nameof(child), "El componente hijo no puede ser null.");

            if (parent.Id == child.Id)
                throw new InvalidOperationException("Un componente no puede ser hijo de sí mismo.");

            if (child is Role role && role.Children.OfType<Role>().Any())
                throw new InvalidOperationException($"No se puede agregar '{child.Name}' como hijo porque ya es un rol padre. Un rol que tiene roles hijos no puede ser hijo de otro rol.");

            if (WouldCreateCircularReference(parent, child))
                throw new InvalidOperationException($"No se puede agregar '{child.Name}' como hijo de '{parent.Name}' porque crearía una referencia circular en la jerarquía.");

            parent.AddChild(child);
        }

        public void RemoveChildFromComponent(Component parent, Component child)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent), "El componente padre no puede ser null.");

            if (child == null)
                throw new ArgumentNullException(nameof(child), "El componente hijo no puede ser null.");

            parent.RemoveChild(child);
        }

        public void ClearChildren(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            component.ClearChildren();
        }

        public List<Component> GetChildren(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            return component.Children.ToList();
        }

        public List<Component> GetAllChildrenRecursive(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            var allChildren = new List<Component>();
            GetAllChildrenRecursiveInternal(component, allChildren);
            return allChildren;
        }

        private void GetAllChildrenRecursiveInternal(Component component, List<Component> allChildren)
        {
            foreach (var child in component.Children)
            {
                allChildren.Add(child);
                GetAllChildrenRecursiveInternal(child, allChildren);
            }
        }

        public List<Permission> GetAllPermissionsRecursive(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            var allPermissions = new List<Permission>();
            GetAllPermissionsRecursiveInternal(component, allPermissions);
            return allPermissions;
        }

        private void GetAllPermissionsRecursiveInternal(Component component, List<Permission> allPermissions)
        {
            if (component is Permission permission)
            {
                allPermissions.Add(permission);
            }
            else
            {
                foreach (var child in component.Children)
                {
                    GetAllPermissionsRecursiveInternal(child, allPermissions);
                }
            }
        }

        public List<Role> GetAllRolesRecursive(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            var allRoles = new List<Role>();
            GetAllRolesRecursiveInternal(component, allRoles);
            return allRoles;
        }

        private void GetAllRolesRecursiveInternal(Component component, List<Role> allRoles)
        {
            if (component is Role role)
            {
                allRoles.Add(role);
            }

            foreach (var child in component.Children)
            {
                GetAllRolesRecursiveInternal(child, allRoles);
            }
        }

        public bool HasPermission(Component component, string permissionName)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            if (string.IsNullOrWhiteSpace(permissionName))
                throw new ArgumentException("El nombre del permiso no puede estar vacío.", nameof(permissionName));

            var allPermissions = GetAllPermissionsRecursive(component);
            return allPermissions.Any(p => p.Name.Equals(permissionName, StringComparison.OrdinalIgnoreCase));
        }

        public bool HasPermission(Component component, PermissionType permissionType)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            var allPermissions = GetAllPermissionsRecursive(component);
            return allPermissions.Any(p => p.Permission == permissionType);
        }

        public List<string> GetPermissionNames(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            var allPermissions = GetAllPermissionsRecursive(component);
            return allPermissions.Select(p => p.Name).ToList();
        }

        public List<PermissionType> GetPermissionTypes(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            var allPermissions = GetAllPermissionsRecursive(component);
            return allPermissions.Select(p => p.Permission).ToList();
        }

        public Component FindComponentById(Component root, int id)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root), "El componente raíz no puede ser null.");

            if (root.Id == id)
                return root;

            foreach (var child in root.Children)
            {
                var found = FindComponentById(child, id);
                if (found != null)
                    return found;
            }

            return null;
        }

        public Component FindComponentByName(Component root, string name)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root), "El componente raíz no puede ser null.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(name));

            if (root.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                return root;

            foreach (var child in root.Children)
            {
                var found = FindComponentByName(child, name);
                if (found != null)
                    return found;
            }

            return null;
        }

        public int GetDepth(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            if (!component.Children.Any())
                return 0;

            return 1 + component.Children.Max(child => GetDepth(child));
        }

        public int GetTotalChildrenCount(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            return GetAllChildrenRecursive(component).Count;
        }

        private bool WouldCreateCircularReference(Component parent, Component child)
        {
            return parent.Id == child.Id;
        }

        public void ValidateComponentStructure(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            var visited = new HashSet<int>();
            ValidateComponentStructureInternal(component, visited);
        }

        private void ValidateComponentStructureInternal(Component component, HashSet<int> visited)
        {
            if (visited.Contains(component.Id))
                throw new InvalidOperationException($"Referencia circular detectada en el componente {component.Name} (ID: {component.Id})");

            visited.Add(component.Id);

            foreach (var child in component.Children)
            {
                ValidateComponentStructureInternal(child, visited);
            }

            visited.Remove(component.Id);
        }

        public void BuildComponentFromIds(Component component, List<int> roleIds, List<int> permissionIds)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            component.ClearChildren();

            if (roleIds != null)
            {
                foreach (var roleId in roleIds)
                {
                    var role = _roleService.GetById(roleId);
                    if (role != null)
                    {
                        component.AddChild(role);
                    }
                }
            }

            if (permissionIds != null)
            {
                foreach (var permissionId in permissionIds)
                {
                    var permission = _permissionService.GetById(permissionId);
                    if (permission != null)
                    {
                        component.AddChild(permission);
                    }
                }
            }
        }

        public (List<int> RoleIds, List<int> PermissionIds) GetIdsFromComponent(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            var roleIds = new List<int>();
            var permissionIds = new List<int>();

            foreach (var child in component.Children)
            {
                if (child is Role role)
                {
                    roleIds.Add(role.Id);
                }
                else if (child is Permission permission)
                {
                    permissionIds.Add(permission.Id);
                }
            }

            return (roleIds, permissionIds);
        }

        public List<string> ValidateComponentHierarchy(Component component)
        {
            var errors = new List<string>();

            try
            {
                ValidateComponentStructure(component);
            }
            catch (InvalidOperationException ex)
            {
                errors.Add(ex.Message);
            }

            if (component is Role role)
            {
                if (string.IsNullOrWhiteSpace(role.Name))
                    errors.Add("El nombre del rol es obligatorio.");

                if (string.IsNullOrWhiteSpace(role.Description))
                    errors.Add("La descripción del rol es obligatoria.");
            }
            else if (component is Permission permission)
            {
                if (string.IsNullOrWhiteSpace(permission.Name))
                    errors.Add("El nombre del permiso es obligatorio.");

                if (string.IsNullOrWhiteSpace(permission.Description))
                    errors.Add("La descripción del permiso es obligatoria.");
            }

            return errors;
        }

        public void SaveComponentHierarchy(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            ValidateComponentStructure(component);

            if (component is Role role)
            {
                _roleService.Update(role.Id, role);
            }
            else if (component is Permission permission)
            {
                _permissionService.Update(permission.Id, permission);
            }
        }

        public Component CloneComponent(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "El componente no puede ser null.");

            if (component is Role role)
            {
                var clonedRole = new Role
                {
                    Name = role.Name + " (Copia)",
                    Description = role.Description,
                    IsActive = role.IsActive
                };

                foreach (var child in role.Children)
                {
                    clonedRole.AddChild(child);
                }

                return clonedRole;
            }
            else if (component is Permission permission)
            {
                return new Permission
                {
                    Name = permission.Name + " (Copia)",
                    Description = permission.Description,
                    IsActive = permission.IsActive,
                    Permission = permission.Permission
                };
            }

            throw new InvalidOperationException("Tipo de componente no soportado para clonación.");
        }

        public void BuildUserPermissions(User user, List<int> roleIds, List<int> permissionIds)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser null.");

            user.Permissions.Clear();

            if (roleIds != null)
            {
                foreach (var roleId in roleIds)
                {
                    var role = _roleService.GetById(roleId);
                    if (role != null)
                    {
                        BuildRoleHierarchyRecursively(role);
                        user.Permissions.Add(role);
                    }
                }
            }

            if (permissionIds != null)
            {
                foreach (var permissionId in permissionIds)
                {
                    var permission = _permissionService.GetById(permissionId);
                    if (permission != null)
                    {
                        user.Permissions.Add(permission);
                    }
                }
            }
        }

        private void BuildRoleHierarchyRecursively(Role role)
        {
            if (role == null) return;

            var roleWithHierarchy = _roleService.GetByIdWithHierarchy(role.Id);
            if (roleWithHierarchy != null)
            {
                role.ClearChildren();
                foreach (var child in roleWithHierarchy.Children)
                {
                    role.AddChild(child);
                }
            }
        }

        public (List<int> RoleIds, List<int> PermissionIds) GetIdsFromUser(User user)
        {
            if (user == null)
                return (new List<int>(), new List<int>());

            var roleIds = new List<int>();
            var permissionIds = new List<int>();

            foreach (var component in user.Permissions)
            {
                if (component is Role role)
                {
                    roleIds.Add(role.Id);
                }
                else if (component is Permission permission)
                {
                    permissionIds.Add(permission.Id);
                }
            }

            return (roleIds, permissionIds);
        }

        public List<Permission> GetAllUserPermissionsRecursive(User user)
        {
            if (user == null)
                return new List<Permission>();

            var allPermissions = new List<Permission>();

            foreach (var component in user.Permissions)
            {
                if (component is Permission permission)
                {
                    allPermissions.Add(permission);
                }
                else if (component is Role role)
                {
                    var rolePermissions = GetAllPermissionsRecursive(role);
                    allPermissions.AddRange(rolePermissions);
                }
            }

            return allPermissions.Distinct().ToList();
        }

        public List<Role> GetAllUserRolesRecursive(User user)
        {
            if (user == null)
                return new List<Role>();

            var allRoles = new List<Role>();

            foreach (var component in user.Permissions)
            {
                if (component is Role role)
                {
                    allRoles.Add(role);
                    var subRoles = GetAllRolesRecursive(role);
                    allRoles.AddRange(subRoles);
                }
            }

            return allRoles.Distinct().ToList();
        }

        public bool UserHasPermission(User user, string permissionName)
        {
            if (user == null)
                return false;

            var allPermissions = GetAllUserPermissionsRecursive(user);
            return allPermissions.Any(p => p.Name.Equals(permissionName, StringComparison.OrdinalIgnoreCase));
        }

        public bool UserHasPermission(User user, PermissionType permissionType)
        {
            if (user == null)
                return false;

            var allPermissions = GetAllUserPermissionsRecursive(user);
            return allPermissions.Any(p => p.Permission == permissionType);
        }
    }
} 
 