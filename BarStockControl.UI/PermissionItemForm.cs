using BarStockControl.Models;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Mappers;
using BarStockControl.DTOs;
using BarStockControl.Forms.Users;
using BarStockControl.Forms.Roles;

namespace BarStockControl.Forms.Permissions
{
    public partial class PermissionItemForm : Form
    {
        private readonly PermissionItemService _permissionItemService;
        private PermissionItemDto _selectedItemDto;

        public PermissionItemForm()
        {
            InitializeComponent();
            _permissionItemService = new PermissionItemService(new XmlDataManager("Xml/data.xml"));
            LoadItems();
        }

        private void LoadItems()
        {
            try
            {
                var items = _permissionItemService.GetAllItemDtos();

                if (chkOnlyActive.Checked)
                    items = items.Where(i => i.IsActive).ToList();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var filter = txtSearch.Text.ToLower();
                    items = items.Where(i =>
                        i.Name.ToLower().Contains(filter) ||
                        i.Description.ToLower().Contains(filter)).ToList();
                }

                dgvPermissionItems.DataSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar elementos de permiso: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var itemDto = GetItemFromForm();
                var errors = _permissionItemService.CreatePermissionItem(itemDto);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadItems();
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
                if (_selectedItemDto == null)
                {
                    MessageBox.Show("Seleccioná un permiso para actualizar.");
                    return;
                }

                var itemDto = GetItemFromForm();
                itemDto.Id = _selectedItemDto.Id;

                var errors = _permissionItemService.UpdatePermissionItem(itemDto);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadItems();
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
                if (_selectedItemDto == null)
                {
                    MessageBox.Show("Seleccioná un permiso para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Estás seguro de eliminar este permiso?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _permissionItemService.DeletePermissionItem(_selectedItemDto.Id);
                    ClearForm();
                    LoadItems();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPermissionItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    _selectedItemDto = (PermissionItemDto)dgvPermissionItems.Rows[e.RowIndex].DataBoundItem;

                    txtName.Text = _selectedItemDto.Name;
                    txtDescription.Text = _selectedItemDto.Description;
                    chkActive.Checked = _selectedItemDto.IsActive;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PermissionItemDto GetItemFromForm()
        {
            return new PermissionItemDto
            {
                Id = _selectedItemDto?.Id ?? 0,
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
            _selectedItemDto = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void btnGoToUsers_Click(object sender, EventArgs e)
        {
            try
            {
                var userForm = new UserForm();
                userForm.Show();
                this.Close();
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
            
            // Si no hay otros formularios abiertos, abrir el menú principal
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] == this)
            {
                var mainMenuForm = new MainMenuForm();
                mainMenuForm.Show();
            }
        }
    }
}
