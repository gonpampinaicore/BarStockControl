using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Forms.Roles;
using BarStockControl.Forms.Permissions;

namespace BarStockControl.Forms.Users
{
    public partial class UserForm : Form
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private UserDto _selectedUserDto;
        private List<RoleDto> _allRoles;

        public UserForm()
        {
            InitializeComponent();
            _userService = new UserService(new XmlDataManager("Xml/data.xml"));
            _roleService = new RoleService(new XmlDataManager("Xml/data.xml"));
            LoadUsers();
            LoadRoles();
        }

        private void LoadUsers()
        {
            try
            {
                var userDtos = _userService.GetAllUsers();

                if (chkOnlyActive.Checked)
                    userDtos = userDtos.Where(u => u.Active).ToList();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string filter = txtSearch.Text.ToLower();
                    userDtos = userDtos.Where(u =>
                        u.FirstName.ToLower().Contains(filter) ||
                        u.LastName.ToLower().Contains(filter) ||
                        u.Email.ToLower().Contains(filter)).ToList();
                }

                dgvUsers.DataSource = userDtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoles()
        {
            try
            {
                _allRoles = _roleService.GetAllRoles();
                FilterRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterRoles()
        {
            try
            {
                var filter = txtRoleSearch.Text.ToLower();
                var filtered = string.IsNullOrWhiteSpace(filter)
                    ? _allRoles
                    : _allRoles.Where(r => r.Name.ToLower().Contains(filter)).ToList();

                var selectedRoleIds = _selectedUserDto?.RoleIds ?? new List<int>();

                dgvRoles.Rows.Clear();

                foreach (var role in filtered)
                {
                    dgvRoles.Rows.Add(
                        selectedRoleIds.Contains(role.Id),
                        role.Id,
                        role.Name
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar roles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private UserDto GetUserFromForm()
        {
            var dto = new UserDto
            {
                Id = _selectedUserDto?.Id ?? 0,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Active = chkActive.Checked,
                Password = txtPassword.Text,
                RoleIds = new List<int>()
            };

            foreach (DataGridViewRow row in dgvRoles.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Selected"].Value))
                {
                    dto.RoleIds.Add(Convert.ToInt32(row.Cells["Id"].Value));
                }
            }

            return dto;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedUserDto == null)
                {
                    MessageBox.Show("Seleccioná un usuario para actualizar.");
                    return;
                }

                var userDto = GetUserFromForm();
                userDto.Id = _selectedUserDto.Id;
                var errors = _userService.UpdateUser(userDto);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error técnico: {ex.Message}\n\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedUserDto == null)
                {
                    MessageBox.Show("Seleccioná un usuario para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Estás seguro de eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _userService.DeleteUser(_selectedUserDto.Id);
                    ClearForm();
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            dgvUsers.ClearSelection();
            _selectedUserDto = null;
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var selectedRow = (UserDto)dgvUsers.Rows[e.RowIndex].DataBoundItem;

                    _selectedUserDto = _userService.GetUserDtoById(selectedRow.Id);

                    txtFirstName.Text = _selectedUserDto.FirstName;
                    txtLastName.Text = _selectedUserDto.LastName;
                    txtEmail.Text = _selectedUserDto.Email;
                    chkActive.Checked = _selectedUserDto.Active;
                    txtPassword.Text = _selectedUserDto.Password;

                    FilterRoles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            chkActive.Checked = true;
            _selectedUserDto = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void txtRoleSearch_TextChanged(object sender, EventArgs e)
        {
            FilterRoles();
        }

        private void btnGoToRoles_Click(object sender, EventArgs e)
        {
            try
            {
                var roleForm = new RoleForm();
                roleForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Roles: {ex.Message}", "Error", 
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
