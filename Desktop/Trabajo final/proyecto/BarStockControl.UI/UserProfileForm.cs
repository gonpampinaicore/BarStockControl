using BarStockControl.Core;
using BarStockControl.Models;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Forms;

namespace BarStockControl
{
    public partial class UserProfileForm : Form
    {
        private readonly UserService _userService;
        private User _currentUser;

        public UserProfileForm()
        {
            InitializeComponent();
            _userService = new UserService(new XmlDataManager("Xml/data.xml"));
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                _currentUser = SessionContext.Instance.LoggedUser;
                if (_currentUser == null)
                {
                    MessageBox.Show("No hay usuario logueado.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                txtFirstName.Text = _currentUser.FirstName;
                txtLastName.Text = _currentUser.LastName;
                txtEmail.Text = _currentUser.Email;
                txtPassword.Text = _userService.DecryptPassword(_currentUser.Password);
                chkActive.Checked = _currentUser.Active;

                txtEmail.ReadOnly = false;
                txtEmail.BackColor = System.Drawing.Color.White;
                chkActive.Enabled = true;
                txtPassword.UseSystemPasswordChar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del usuario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentUser == null)
                {
                    MessageBox.Show("No hay usuario cargado.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    MessageBox.Show("El nombre es obligatorio.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFirstName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("El apellido es obligatorio.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLastName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("El email es obligatorio.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                if (!txtEmail.Text.Contains("@"))
                {
                    MessageBox.Show("El email ingresado no es válido.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("La contraseña es obligatoria.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                bool emailChanged = !_currentUser.Email.Equals(txtEmail.Text.Trim(), StringComparison.OrdinalIgnoreCase);
                bool passwordChanged = !_userService.DecryptPassword(_currentUser.Password).Equals(txtPassword.Text);

                var userDto = new DTOs.UserDto
                {
                    Id = _currentUser.Id,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text,
                    Active = chkActive.Checked,
                    RoleIds = new List<int>(_currentUser.RoleIds)
                };

                var errors = _userService.UpdateUser(userDto);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (emailChanged || passwordChanged)
                {
                    var result = MessageBox.Show(
                        "Has modificado tu email o contraseña. Para que los cambios surtan efecto, " +
                        "necesitas volver a iniciar sesión. ¿Deseas ir al login ahora?", 
                        "Cambios en credenciales", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        SessionContext.Instance.Clear();
                        
                        this.Close();
                        var mainMenu = Application.OpenForms.OfType<MainMenuForm>().FirstOrDefault();
                        if (mainMenu != null)
                        {
                            mainMenu.CloseProgrammatically();
                        }
                        
                        var loginForm = new LoginForm();
                        loginForm.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    var updatedUser = _userService.GetById(_currentUser.Id);
                    SessionContext.Instance.SetUser(updatedUser);
                    
                    MessageBox.Show("Datos actualizados correctamente.", "Éxito", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar datos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
    }
} 
