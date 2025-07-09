using BarStockControl.Models;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Mappers;
using BarStockControl.DTOs;
using BarStockControl.Forms.Users;
using BarStockControl.Forms.Roles;

namespace BarStockControl.Forms.Permissions
{
    public partial class PermissionForm : Form
    {
        private readonly PermissionService _permissionService;
        private readonly PermissionItemService _permissionItemService;
        private PermissionDto _selectedPermission;

        public PermissionForm()
        {
            InitializeComponent();
            _permissionService = new PermissionService(new XmlDataManager("Xml/data.xml"));
            _permissionItemService = new PermissionItemService(new XmlDataManager("Xml/data.xml"));
            LoadPermissionItems();
            LoadPermissions();
        }

        private void LoadPermissions()
        {
            try
            {
                var permissions = _permissionService.GetAllPermissionDtos();

                if (chkOnlyActive.Checked)
                    permissions = permissions.Where(p => p.IsActive).ToList();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var filter = txtSearch.Text.ToLower();
                    permissions = permissions.Where(p =>
                        p.Name.ToLower().Contains(filter) ||
                        p.Description.ToLower().Contains(filter)).ToList();
                }

                dgvPermissions.DataSource = permissions;
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
                var permissionDto = GetPermissionFromForm();
                var errors = _permissionService.CreatePermission(permissionDto);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadPermissions();
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
                if (_selectedPermission == null)
                {
                    MessageBox.Show("Seleccioná un permiso para actualizar.");
                    return;
                }

                var permissionDto = GetPermissionFromForm();
                permissionDto.Id = _selectedPermission.Id;

                var errors = _permissionService.UpdatePermission(permissionDto);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadPermissions();
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
                if (_selectedPermission == null)
                {
                    MessageBox.Show("Seleccioná un permiso para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Estás seguro de eliminar este permiso?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _permissionService.DeletePermission(_selectedPermission.Id);
                    ClearForm();
                    LoadPermissions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPermissions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    _selectedPermission = (PermissionDto)dgvPermissions.Rows[e.RowIndex].DataBoundItem;

                    txtName.Text = _selectedPermission.Name;
                    txtDescription.Text = _selectedPermission.Description;
                    chkActive.Checked = _selectedPermission.IsActive;

                    for (int i = 0; i < clbPermissionItems.Items.Count; i++)
                    {
                        if (clbPermissionItems.Items[i] is PermissionItem item)
                        {
                            bool isSelected = _selectedPermission.PermissionItemIds.Contains(item.Id);
                            clbPermissionItems.SetItemChecked(i, isSelected);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private PermissionDto GetPermissionFromForm()
        {
            var selectedIds = new List<int>();

            foreach (var checkedItem in clbPermissionItems.CheckedItems)
            {
                if (checkedItem is PermissionItem item)
                {
                    selectedIds.Add(item.Id);
                }
            }

            return new PermissionDto
            {
                Id = _selectedPermission?.Id ?? 0,
                Name = txtName.Text,
                Description = txtDescription.Text,
                IsActive = chkActive.Checked,
                PermissionItemIds = selectedIds
            };
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtDescription.Clear();
            chkActive.Checked = true;
            _selectedPermission = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadPermissions();
        }

        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadPermissions();
        }

        private void LoadPermissionItems()
        {
            try
            {
                clbPermissionItems.Items.Clear();
                var items = _permissionItemService.GetAllItems();
                foreach (var item in items)
                {
                    clbPermissionItems.Items.Add(item, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar elementos de permiso: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPermissionItemSearch_TextChanged(object sender, EventArgs e)
        {
            LoadPermissionItems();
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

        private void btnGoToRoles_Click(object sender, EventArgs e)
        {
            try
            {
                var roleForm = new RoleForm();
                roleForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Roles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoToPermissionItems_Click(object sender, EventArgs e)
        {
            try
            {
                var permissionItemForm = new PermissionItemForm();
                permissionItemForm.Show();
                this.Close();
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
