using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.DTOs;

namespace BarStockControl.Forms
{
    public partial class UserForm : Form
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly PermissionService _permissionService;
        private UserDto _selectedUser;

        public UserForm()
        {
            InitializeComponent();
            _userService = new UserService(new XmlDataManager("Xml/data.xml"));
            _roleService = new RoleService(new XmlDataManager("Xml/data.xml"));
            _permissionService = new PermissionService(new XmlDataManager("Xml/data.xml"));

            LoadUsers();
            LoadRoles();
            LoadPermissions();
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

        private void LoadRoles()
        {
            clbRoles.Items.Clear();
            var roles = _roleService.GetAllRoles();
            foreach (var r in roles)
                clbRoles.Items.Add(r, false);
            clbRoles.DisplayMember = "Name";
        }

        private void LoadPermissions()
        {
            clbPermissions.Items.Clear();
            var permissions = _permissionService.GetAllPermissions().Select(_permissionService.ToDto).ToList();
            foreach (var p in permissions)
                clbPermissions.Items.Add(p, false);
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

                for (int i = 0; i < clbRoles.Items.Count; i++)
                {
                    if (clbRoles.Items[i] is RoleDto r)
                        clbRoles.SetItemChecked(i, _selectedUser.RoleIds.Contains(r.Id));
                }

                for (int i = 0; i < clbPermissions.Items.Count; i++)
                {
                    if (clbPermissions.Items[i] is PermissionDto p)
                        clbPermissions.SetItemChecked(i, _selectedUser.PermissionIds.Contains(p.Id));
                }
            }
        }

        private UserDto GetUserFromForm()
        {
            var selectedRoleIds = clbRoles.CheckedItems.Cast<RoleDto>().Select(r => r.Id).ToList();
            var selectedPermissionIds = clbPermissions.CheckedItems.Cast<PermissionDto>().Select(p => p.Id).ToList();

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
            _selectedUser = null;

            for (int i = 0; i < clbRoles.Items.Count; i++)
                clbRoles.SetItemChecked(i, false);

            for (int i = 0; i < clbPermissions.Items.Count; i++)
                clbPermissions.SetItemChecked(i, false);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadUsers();
        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e) => LoadUsers();
        private void btnClear_Click(object sender, EventArgs e) => ClearForm();
    }
}
