using BarStockControl.Models;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Mappers;
using BarStockControl.DTOs;

namespace BarStockControl.UI
{
    public partial class PermissionForm : Form
    {
        private readonly PermissionService _permissionService;
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private PermissionDto _selectedPermission = new PermissionDto();

        public PermissionForm()
        {
            InitializeComponent();
            _permissionService = new PermissionService(new XmlDataManager("Xml/data.xml"));
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
            catch (Exception)
            {
                MessageBox.Show("Error al cargar permisos.", "Error", 
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PermissionDto GetPermissionFromForm()
        {
            return new PermissionDto
            {
                Id = _selectedPermission?.Id ?? 0,
                Name = txtName.Text,
                Description = txtDescription.Text,
                IsActive = chkActive.Checked
            };
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtDescription.Clear();
            chkActive.Checked = true;
            _selectedPermission = new PermissionDto();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadPermissions();
        }

        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadPermissions();
        }

        private void btnGoToUsers_Click(object sender, EventArgs e)
        {
            try
            {
                var userForm = new UserForm();
                userForm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al abrir formulario de usuarios.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoToRoles_Click(object sender, EventArgs e)
        {
            try
            {
                var roleForm = new RoleForm();
                roleForm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al abrir formulario de roles.", "Error", 
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
            catch (Exception)
            {
                MessageBox.Show("Error al abrir menú principal.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            try
            {
                var mainMenuForm = new MainMenuForm();
                mainMenuForm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al abrir menú principal.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
