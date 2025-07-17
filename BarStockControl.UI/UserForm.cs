using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Models;

namespace BarStockControl.UI
{
    public partial class UserForm : Form
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly PermissionService _permissionService;
        private readonly ComponentService _componentService;
        private UserDto _selectedUser = new UserDto();
        private List<PermissionDto> _allPermissions = new List<PermissionDto>();

        public UserForm()
        {
            InitializeComponent();
            _userService = new UserService(new XmlDataManager("Xml/data.xml"));
            _roleService = new RoleService(new XmlDataManager("Xml/data.xml"));
            _permissionService = new PermissionService(new XmlDataManager("Xml/data.xml"));
            _componentService = new ComponentService(new XmlDataManager("Xml/data.xml"));

            LoadUsers();
            LoadAllPermissions();
        }

        private void LoadUsers()
        {
            var users = _userService.GetAllUsers();

            if (chkOnlyActive.Checked)
                users = users.Where(u => u.Active).ToList();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                var filter = txtSearch.Text.ToLower();
                users = users.Where(u =>
                        u.FirstName.ToLower().Contains(filter) ||
                        u.LastName.ToLower().Contains(filter) ||
                        u.Email.ToLower().Contains(filter)).ToList();
            }

            dgvUsers.DataSource = users;
        }

        private void LoadAllPermissions()
        {
            _allPermissions = _permissionService.GetAllPermissions().Select(_permissionService.ToDto).ToList();
        }

        private void LoadUserHierarchy(UserDto user)
        {
            try
            {
                tvUserRoles.Nodes.Clear();
                
                if (user == null) return;

                foreach (var roleId in user.RoleIds)
                {
                    var role = _roleService.GetByIdWithHierarchy(roleId);
                    if (role != null)
                    {
                        var roleNode = CreateTreeNode(role);
                        tvUserRoles.Nodes.Add(roleNode);
                    }
                }

                foreach (var permissionId in user.PermissionIds)
                {
                    var permission = _permissionService.GetById(permissionId);
                    if (permission != null)
                    {
                        var permissionNode = CreateTreeNode(permission);
                        tvUserRoles.Nodes.Add(permissionNode);
                    }
                }

                tvUserRoles.ExpandAll();
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
                if (_selectedUser == null)
                {
                    MessageBox.Show("Seleccione un usuario para agregar roles.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var availableRoles = GetAvailableRolesForSelection();
                if (!availableRoles.Any())
                {
                    MessageBox.Show("No hay roles disponibles para agregar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var form = new RoleSelectionForm(availableRoles, "Seleccionar Rol"))
                {
                    if (form.ShowDialog() == DialogResult.OK && form.SelectedRole != null)
                    {
                        if (_selectedUser.RoleIds == null)
                            _selectedUser.RoleIds = new List<int>();

                        if (!_selectedUser.RoleIds.Contains(form.SelectedRole.Id))
                        {
                            _selectedUser.RoleIds.Add(form.SelectedRole.Id);
                            LoadUserHierarchy(_selectedUser);
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
                if (_selectedUser == null)
                {
                    MessageBox.Show("Seleccione un usuario para agregar permisos.", "Aviso", 
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
                        if (_selectedUser.PermissionIds == null)
                            _selectedUser.PermissionIds = new List<int>();

                        if (!_selectedUser.PermissionIds.Contains(form.SelectedPermission.Id))
                        {
                            _selectedUser.PermissionIds.Add(form.SelectedPermission.Id);
                            LoadUserHierarchy(_selectedUser);
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

        private void btnRemovePermission_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedUser == null)
                {
                    MessageBox.Show("Seleccione un usuario primero.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (tvUserRoles.SelectedNode == null)
                {
                    MessageBox.Show("Seleccione un elemento para quitar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var selectedComponent = tvUserRoles.SelectedNode.Tag as Component;
                if (selectedComponent == null) return;

                var confirm = MessageBox.Show($"¿Está seguro de quitar '{selectedComponent.Name}'?", 
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    if (selectedComponent is Role role)
                    {
                        if (_selectedUser.RoleIds.Contains(role.Id))
                        {
                            _selectedUser.RoleIds.Remove(role.Id);
                        }
                    }
                    else if (selectedComponent is Permission permission)
                    {
                        if (_selectedUser.PermissionIds.Contains(permission.Id))
                        {
                            _selectedUser.PermissionIds.Remove(permission.Id);
                        }
                    }

                    LoadUserHierarchy(_selectedUser);
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
            var currentRoleIds = _selectedUser.RoleIds ?? new List<int>();

            return allRoles.Where(r => !currentRoleIds.Contains(r.Id)).ToList();
        }

        private List<PermissionDto> GetAvailablePermissionsForSelection()
        {
            var userPermissions = GetUserPermissionsRecursive(_selectedUser);
            var currentPermissionIds = userPermissions.Select(p => p.Id).ToList();

            return _allPermissions.Where(p => !currentPermissionIds.Contains(p.Id)).ToList();
        }

        private List<PermissionDto> GetUserPermissionsRecursive(UserDto user)
        {
            var permissions = new List<PermissionDto>();

            foreach (var roleId in user.RoleIds)
            {
                var role = _roleService.GetById(roleId);
                if (role != null)
                {
                    var rolePermissions = _componentService.GetAllPermissionsRecursive(role);
                    permissions.AddRange(rolePermissions.Select(_permissionService.ToDto));
                }
            }

            foreach (var permissionId in user.PermissionIds)
            {
                var permission = _permissionService.GetById(permissionId);
                if (permission != null)
                {
                    permissions.Add(_permissionService.ToDto(permission));
                }
            }

            return permissions.Distinct().ToList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var userDto = GetUserFromForm();
            var errors = _userService.CreateUser(userDto);
            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClearForm();
            LoadUsers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null)
            {
                MessageBox.Show("Seleccioná un usuario para actualizar.");
                return;
            }

            var userDto = GetUserFromForm();
            userDto.Id = _selectedUser.Id;

            var errors = _userService.UpdateUser(userDto);
            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClearForm();
            LoadUsers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null)
            {
                MessageBox.Show("Seleccioná un usuario para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Estás seguro de eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                _userService.DeleteUser(_selectedUser.Id);
                ClearForm();
                LoadUsers();
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _selectedUser = (UserDto)dgvUsers.Rows[e.RowIndex].DataBoundItem;

                txtFirstName.Text = _selectedUser.FirstName;
                txtLastName.Text = _selectedUser.LastName;
                txtEmail.Text = _selectedUser.Email;
                txtPassword.Text = _selectedUser.Password;
                chkActive.Checked = _selectedUser.Active;

                LoadUserHierarchy(_selectedUser);
            }
        }

        private UserDto GetUserFromForm()
        {
            var selectedRoleIds = _selectedUser?.RoleIds ?? new List<int>();
            var selectedPermissionIds = _selectedUser?.PermissionIds ?? new List<int>();

            return new UserDto
            {
                Id = _selectedUser?.Id ?? 0,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                Active = chkActive.Checked,
                RoleIds = selectedRoleIds,
                PermissionIds = selectedPermissionIds
            };
        }

        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            chkActive.Checked = true;
            _selectedUser = new UserDto();

            tvUserRoles.Nodes.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadUsers();
        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e) => LoadUsers();
        private void btnClear_Click(object sender, EventArgs e) => ClearForm();
    }
}
