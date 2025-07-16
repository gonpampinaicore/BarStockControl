
using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Services;
using BarStockControl.Models;
using BarStockControl.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace BarStockControl
{
    public partial class RoleForm : Form
    {
        private readonly RoleService _roleService;
        private readonly PermissionService _permissionService;
        private readonly ComponentService _componentService;
        private RoleDto _selectedRole;
        private Role _currentRoleEntity;

        public RoleForm()
        {
            InitializeComponent();
            _roleService = new RoleService(new XmlDataManager("Xml/data.xml"));
            _permissionService = new PermissionService(new XmlDataManager("Xml/data.xml"));
            _componentService = new ComponentService(new XmlDataManager("Xml/data.xml"));
            
            SetupEventHandlers();
            LoadRoles();
        }

        private void SetupEventHandlers()
        {
            btnAddRole.Click += btnAddRole_Click;
            btnAddPermission.Click += btnAddPermission_Click;
            btnRemoveItem.Click += btnRemoveItem_Click;
            tvRoleHierarchy.AfterSelect += tvRoleHierarchy_AfterSelect;
        }

        private void LoadRoles()
        {
            try
            {
                var roles = _roleService.GetAllRoles();

                if (chkOnlyActive.Checked)
                    roles = roles.Where(r => r.IsActive).ToList();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var filter = txtSearch.Text.ToLower();
                    roles = roles.Where(r =>
                        r.Name.ToLower().Contains(filter) ||
                        r.Description.ToLower().Contains(filter)).ToList();
                }

                dgvRoles.DataSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoleHierarchy(Role role)
        {
            try
            {
                tvRoleHierarchy.Nodes.Clear();
                
                if (role == null) return;

                var rootNode = new TreeNode(role.Name) { Tag = role };
                tvRoleHierarchy.Nodes.Add(rootNode);

                foreach (var child in role.Children)
                {
                    var childNode = CreateTreeNode(child);
                    rootNode.Nodes.Add(childNode);
                }

                rootNode.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar jerarquía: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TreeNode CreateTreeNode(Component component)
        {
            var node = new TreeNode(component.Name) { Tag = component };

            if (component is Role role)
            {
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                
                foreach (var child in role.Children)
                {
                    var childNode = CreateTreeNode(child);
                    node.Nodes.Add(childNode);
                }
            }
            else if (component is Permission permission)
            {
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
            }

            return node;
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentRoleEntity == null)
                {
                    MessageBox.Show("Seleccione un rol para agregar sub-roles.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var availableRoles = GetAvailableRolesForSelection();
                if (!availableRoles.Any())
                {
                    MessageBox.Show("No hay roles disponibles para agregar. Los roles restantes comparten permisos con este rol o sus roles hijos.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var form = new RoleSelectionForm(availableRoles, "Seleccionar Rol"))
                {
                    if (form.ShowDialog() == DialogResult.OK && form.SelectedRole != null)
                    {
                        var selectedRole = _roleService.GetByIdWithHierarchy(form.SelectedRole.Id);
                        if (selectedRole != null)
                        {
                            _componentService.AddChildToComponent(_currentRoleEntity, selectedRole);
                            LoadRoleHierarchy(_currentRoleEntity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar rol: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPermission_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentRoleEntity == null)
                {
                    MessageBox.Show("Seleccione un rol para agregar permisos.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var availablePermissions = GetAvailablePermissionsForSelection();
                if (!availablePermissions.Any())
                {
                    MessageBox.Show("No hay permisos disponibles para agregar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var form = new PermissionSelectionForm(availablePermissions, "Seleccionar Permiso"))
                {
                    if (form.ShowDialog() == DialogResult.OK && form.SelectedPermission != null)
                    {
                        var selectedPermission = _permissionService.GetById(form.SelectedPermission.Id);
                        if (selectedPermission != null)
                        {
                            _componentService.AddChildToComponent(_currentRoleEntity, selectedPermission);
                            LoadRoleHierarchy(_currentRoleEntity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar permiso: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvRoleHierarchy.SelectedNode == null)
                {
                    MessageBox.Show("Seleccione un elemento para quitar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var selectedComponent = tvRoleHierarchy.SelectedNode.Tag as Component;
                if (selectedComponent == null) return;

                if (selectedComponent == _currentRoleEntity)
                {
                    MessageBox.Show("No puede quitar el rol principal.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var confirm = MessageBox.Show($"¿Está seguro de quitar '{selectedComponent.Name}'?", 
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    _componentService.RemoveChildFromComponent(_currentRoleEntity, selectedComponent);
                    LoadRoleHierarchy(_currentRoleEntity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al quitar elemento: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<RoleDto> GetAvailableRolesForSelection()
        {
            var allRoles = _roleService.GetAllRoles();
            var currentRoleIds = _componentService.GetAllRolesRecursive(_currentRoleEntity)
                .Select(r => r.Id)
                .ToList();

            var currentPermissionIds = _componentService.GetAllPermissionsRecursive(_currentRoleEntity)
                .Select(p => p.Id)
                .ToList();

            var availableRoles = new List<RoleDto>();

            foreach (var role in allRoles)
            {
                if (currentRoleIds.Contains(role.Id) || role.Id == _currentRoleEntity.Id)
                    continue;

                var roleWithHierarchy = _roleService.GetByIdWithHierarchy(role.Id);
                if (roleWithHierarchy == null) continue;

                var rolePermissionIds = _componentService.GetAllPermissionsRecursive(roleWithHierarchy)
                    .Select(p => p.Id)
                    .ToList();

                var hasDuplicatePermissions = rolePermissionIds.Any(permId => currentPermissionIds.Contains(permId));

                if (!hasDuplicatePermissions)
                {
                    availableRoles.Add(role);
                }
            }

            return availableRoles;
        }

        private List<PermissionDto> GetAvailablePermissionsForSelection()
        {
            var allPermissions = _permissionService.GetAllPermissionDtos();
            var currentPermissionIds = _componentService.GetAllPermissionsRecursive(_currentRoleEntity)
                .Select(p => p.Id)
                .ToList();

            return allPermissions.Where(p => !currentPermissionIds.Contains(p.Id)).ToList();
        }

        private void tvRoleHierarchy_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Component component)
            {
                var tooltip = component is Role role ? 
                    $"Rol: {role.Description}" : 
                    $"Permiso: {((Permission)component).Description}";
                
                toolTip1.SetToolTip(tvRoleHierarchy, tooltip);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var roleDto = GetRoleFromForm();
                var errors = _roleService.CreateRole(roleDto);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear rol: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedRole == null)
                {
                    MessageBox.Show("Seleccione un rol para actualizar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var roleDto = GetRoleFromForm();
                roleDto.Id = _selectedRole.Id;

                var errors = _roleService.UpdateRole(roleDto);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar rol: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedRole == null)
                {
                    MessageBox.Show("Seleccione un rol para eliminar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var confirm = MessageBox.Show("¿Está seguro de eliminar este rol?", "Confirmar", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (confirm == DialogResult.Yes)
                {
                    _roleService.DeleteRole(_selectedRole.Id);
                    ClearForm();
                    LoadRoles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar rol: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    _selectedRole = (RoleDto)dgvRoles.Rows[e.RowIndex].DataBoundItem;

                    txtName.Text = _selectedRole.Name;
                    txtDescription.Text = _selectedRole.Description;
                    chkActive.Checked = _selectedRole.IsActive;

                    _currentRoleEntity = _roleService.GetByIdWithHierarchy(_selectedRole.Id);
                    LoadRoleHierarchy(_currentRoleEntity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al seleccionar rol: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private RoleDto GetRoleFromForm()
        {
            if (_currentRoleEntity == null)
            {
                return new RoleDto
                {
                    Id = _selectedRole?.Id ?? 0,
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    IsActive = chkActive.Checked,
                    PermissionIds = new List<int>(),
                    RoleIds = new List<int>()
                };
            }

            var (roleIds, permissionIds) = _roleService.GetComponentIds(_currentRoleEntity);

            return new RoleDto
            {
                Id = _selectedRole?.Id ?? 0,
                Name = txtName.Text,
                Description = txtDescription.Text,
                IsActive = chkActive.Checked,
                PermissionIds = permissionIds,
                RoleIds = roleIds
            };
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtDescription.Clear();
            chkActive.Checked = true;
            _selectedRole = null;
            _currentRoleEntity = null;
            tvRoleHierarchy.Nodes.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadRoles();
        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e) => LoadRoles();
    }
}
