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
    }
} 
 