using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Mappers;
using BarStockControl.DTOs;
using BarStockControl.Forms.Users;
using BarStockControl.Forms.Permissions;

namespace BarStockControl.Forms.Roles
{
    public partial class RoleForm : Form
    {
        private readonly RoleService _roleService;
        private readonly PermissionService _permissionService;
        private RoleDto _selectedRole;

        public RoleForm()
        {
            InitializeComponent();
            _roleService = new RoleService(new XmlDataManager("Xml/data.xml"));
            _permissionService = new PermissionService(new XmlDataManager("Xml/data.xml"));
            LoadPermissions();
            LoadRoles();
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

        private void LoadPermissions()
        {
            try
            {
                clbPermissions.Items.Clear();
                var permissions = _permissionService.GetAllPermissions().Select(_permissionService.ToDto).ToList();
                foreach (var p in permissions)
                {
                    clbPermissions.Items.Add(p, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar permisos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedRole == null)
                {
                    MessageBox.Show("Seleccioná un rol para actualizar.");
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
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedRole == null)
                {
                    MessageBox.Show("Seleccioná un rol para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Estás seguro de eliminar este rol?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _roleService.DeleteRole(_selectedRole.Id);
                    ClearForm();
                    LoadRoles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                    for (int i = 0; i < clbPermissions.Items.Count; i++)
                    {
                        if (clbPermissions.Items[i] is PermissionDto p)
                        {
                            bool isSelected = _selectedRole.PermissionIds.Contains(p.Id);
                            clbPermissions.SetItemChecked(i, isSelected);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private RoleDto GetRoleFromForm()
        {
            var selectedPermissionIds = new List<int>();

            foreach (var checkedItem in clbPermissions.CheckedItems)
            {
                if (checkedItem is PermissionDto p)
                    selectedPermissionIds.Add(p.Id);
            }

            return new RoleDto
            {
                Id = _selectedRole?.Id ?? 0,
                Name = txtName.Text,
                Description = txtDescription.Text,
                IsActive = chkActive.Checked,
                PermissionIds = selectedPermissionIds
            };
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtDescription.Clear();
            chkActive.Checked = true;
            _selectedRole = null;

            for (int i = 0; i < clbPermissions.Items.Count; i++)
                clbPermissions.SetItemChecked(i, false);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRoles();
        }

        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadRoles();
        }

        private void txtPermissionSearch_TextChanged(object sender, EventArgs e)
        {
            LoadPermissions();
        }

        private void btnGoToUsers_Click(object sender, EventArgs e)
        {
            try
            {
                var userForm = new UserForm();
                userForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Usuarios: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoToPermissions_Click(object sender, EventArgs e)
        {
            try
            {
                var permissionForm = new PermissionForm();
                permissionForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Permisos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoToPermissionItems_Click(object sender, EventArgs e)
        {
            try
            {
                var permissionItemForm = new PermissionItemForm();
                permissionItemForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Elementos de Permisos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoToMainMenu_Click(object sender, EventArgs e)
        {
            try
            {
                var mainMenuForm = new MainMenuForm();
                mainMenuForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Menú Principal: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] == this)
            {
                var mainMenuForm = new MainMenuForm();
                mainMenuForm.Show();
            }
        }
    }
}
